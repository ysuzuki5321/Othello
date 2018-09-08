using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitys;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace Hash
{

    unsafe public class HashBuilder
    {
        private const byte HASH_BLOCK_BYTE_LENGH = 64;
        private const byte PADDING_HEADER = 0x80;
        private const char PADDING_VALUE = (char)0x00;
        private const byte PADDING_FOOTERLENGTH = 9;

        private static byte[] PaddingFooterExpression(int byteCount)
        {
            var convetTarget =BitConverter.GetBytes(0L | (byteCount << 3));
            if( BitConverter.IsLittleEndian)
            {
                Array.Reverse(convetTarget);
            }
            return convetTarget;
        }

        private static uint[] baseBlock = new uint[]
            {
                0x428a2f98 ,0x71374491 ,0xb5c0fbcf ,0xe9b5dba5 ,0x3956c25b ,0x59f111f1 ,0x923f82a4 ,0xab1c5ed5
                ,0xd807aa98 ,0x12835b01 ,0x243185be ,0x550c7dc3 ,0x72be5d74 ,0x80deb1fe ,0x9bdc06a7 ,0xc19bf174
                ,0xe49b69c1 ,0xefbe4786 ,0x0fc19dc6 ,0x240ca1cc ,0x2de92c6f ,0x4a7484aa ,0x5cb0a9dc ,0x76f988da
                ,0x983e5152 ,0xa831c66d ,0xb00327c8 ,0xbf597fc7 ,0xc6e00bf3 ,0xd5a79147 ,0x06ca6351 ,0x14292967
                ,0x27b70a85 ,0x2e1b2138 ,0x4d2c6dfc ,0x53380d13 ,0x650a7354 ,0x766a0abb ,0x81c2c92e ,0x92722c85
                ,0xa2bfe8a1 ,0xa81a664b ,0xc24b8b70 ,0xc76c51a3 ,0xd192e819 ,0xd6990624 ,0xf40e3585 ,0x106aa070
                ,0x19a4c116 ,0x1e376c08 ,0x2748774c ,0x34b0bcb5 ,0x391c0cb3 ,0x4ed8aa4a ,0x5b9cca4f ,0x682e6ff3
                ,0x748f82ee ,0x78a5636f ,0x84c87814 ,0x8cc70208 ,0x90befffa ,0xa4506ceb ,0xbef9a3f7 ,0xc67178f2
            };

        public static string Build( string value)
        {
            byte[] byteArray = BytesPadding(value);

            uint[] defaultHash = new uint[]
            {
                0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A
                ,0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19
            };

            uint[] workHash = new uint[defaultHash.Length];
            foreach(uint[] block in SplitByte(byteArray))
            {
                // ハッシュのコピーと一時変数の宣言
                defaultHash.CopyTo(workHash, 0);
                uint tempX = 0;
                uint tempY = 0;
                for (int blockIndex = 0; blockIndex < HASH_BLOCK_BYTE_LENGH; blockIndex++)
                {
                    tempX = SafeAdd(workHash[7],SigmaA1(workHash[4]),Ch(workHash[4],workHash[5],workHash[6])
                        , baseBlock[blockIndex],block[blockIndex]);
                    tempY = SafeAdd(SigmaA0(workHash[0]),Maj(workHash[0],workHash[1],workHash[2]));

                    workHash[7] = workHash[6];
                    workHash[6] = workHash[5];
                    workHash[5] = workHash[4];
                    workHash[4] = SafeAdd( workHash[3], tempX);
                    workHash[3] = workHash[2];
                    workHash[2] = workHash[1];
                    workHash[1] = workHash[0];
                    workHash[0] = SafeAdd( tempX, tempY);
                }
                // Block毎にdefaultHashの計算を行う
                for (int hashIndex = 0; hashIndex < defaultHash.Length; hashIndex++)
                {
                    defaultHash[hashIndex] = SafeAdd(defaultHash[hashIndex],workHash[hashIndex]);
                }
            }
            byte[] retBytes = new byte[32];
            for (int i = 0; i < 8; i++)
            {
                Array.Copy(BitConverter.GetBytes(defaultHash[i]), 0, retBytes, i << 2, 4);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(retBytes, i << 2, 4);
                }
            }
           
            return string.Join("", retBytes.Select(x => Convert.ToString(x,16)));            
        }
        
        private static int ExtensionBlockNum(int count) => ((count < 56) ? 1 : 2);
        private static byte[] BytesPadding(string value)
        {            
            var byteCount = CommonUtility.sjisEndording.GetByteCount(value);
            var remainderCount = (byteCount % HASH_BLOCK_BYTE_LENGH);
            var blockSize = byteCount / HASH_BLOCK_BYTE_LENGH + ExtensionBlockNum(remainderCount);
            var retBytes = CommonUtility.sjisEndording.GetBytes(value);
            Array.Resize(ref retBytes, blockSize * HASH_BLOCK_BYTE_LENGH);
            retBytes[byteCount] = PADDING_HEADER;
            Array.Copy(PaddingFooterExpression(byteCount), 0, retBytes, retBytes.Length - 8, 8);
            return retBytes;
        }

        private static IEnumerable<uint[]> SplitByte(byte[] bytes)
        {
            const int EXTENSION_BASE_LENGTH = 16;
            int loopNum = bytes.Length / HASH_BLOCK_BYTE_LENGH;
            const int byteLength = 2;
            for (int loopIndex = 0; loopIndex < loopNum; loopIndex++)
            {
                uint[] numArray = new uint[HASH_BLOCK_BYTE_LENGH];
                for (int numArrayIndex = 0; numArrayIndex < HASH_BLOCK_BYTE_LENGH; numArrayIndex++)
                {
                    if(numArrayIndex < EXTENSION_BASE_LENGTH)
                    {
                        uint startIndex = ((uint)numArrayIndex << byteLength) 
                            + (uint)(loopIndex * HASH_BLOCK_BYTE_LENGH);

                        if (BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(bytes, (int)startIndex, 4);
                        }

                        numArray[numArrayIndex] = BitUtility.GetLeftBitShiftUInt(bytes[startIndex], 0)
                            | BitUtility.GetLeftBitShiftUInt(bytes[startIndex + 1], 8)
                            | BitUtility.GetLeftBitShiftUInt(bytes[startIndex + 2], 16)
                            | BitUtility.GetLeftBitShiftUInt(bytes[startIndex + 3], 24)
                            ;
                    }
                    else
                    {
                        // i - 2 ,i - 7 , i - 15 , i - 16のインデックスの値を加算する
                        numArray[numArrayIndex] = SafeAdd(SigmaB1(numArray[numArrayIndex - 2])
                                                    ,numArray[numArrayIndex - 7]
                                                    , SigmaB0(numArray[numArrayIndex - 15])
                                                    ,numArray[numArrayIndex - 16]);
                    }
                    
                }

                yield return numArray;
            }
        }

        private static uint SafeAdd(params uint[] values)
        {
            uint returnValue = values[0];
            for (int valuesIndex = 1; valuesIndex < values.Length ; valuesIndex++)
            {
                returnValue = ((returnValue & 0x7FFFFFFF) + (values[valuesIndex] & 0x7FFFFFFF))
                    ^ (returnValue & 0x80000000) ^ (values[valuesIndex] & 0x80000000) ;
            }
            return returnValue;
        }
        private static uint Ch(uint x,uint y,uint z)
        {
            return (x & y) ^ ((~x) & z);
        }
        private static uint Maj(uint x, uint y, uint z)
        {
            //return (x & y) ^ (y & z) ^ (z & x);
            return (x & y) ^ (x & z) ^ (y & z);
        }
        private static uint sigmaABase(uint value,int first,int second,int third) =>
            BitUtility.GetRightBitRotateUInt(value, first) ^ BitUtility.GetRightBitRotateUInt(value, second) ^ BitUtility.GetRightBitRotateUInt(value, third);
        private static uint sigmaBBase(uint value, int first, int second, int third) =>
            BitUtility.GetRightBitRotateUInt(value, first) ^ BitUtility.GetRightBitRotateUInt(value, second) ^ BitUtility.GetRightBitShiftUInt(value, third);
        private static uint SigmaA0(uint x)
        {
            return sigmaABase(x ,2,13,22);
        }
        private static uint SigmaA1(uint x)
        {
            return sigmaABase(x, 6, 11, 25);
        }
        private static uint SigmaB0(uint x)
        {
            return sigmaBBase(x, 7, 18, 3);
        }
        private static uint SigmaB1(uint x)
        {
            return sigmaBBase(x, 17, 19, 10);
        }

    }

}
