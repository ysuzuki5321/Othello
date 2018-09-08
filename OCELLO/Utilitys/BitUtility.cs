using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitys
{
    public class BitUtility
    {
        public static uint GetRightBitShiftUInt(uint val, int shift)
        {
            return val >> shift;
        }
        public static uint GetLeftBitShiftUInt(uint val, int shift)
        {
            return val << shift;
        }
        public static uint GetRightBitRotateUInt(uint val, int shift)
        {
            return (val >> shift) | (val << (32 - shift));
        }
        public static uint GetLeftBitRotateUInt(uint val, int shift)
        {
            return (val << shift) | (val >> (32 - shift));
        }
    }
}
