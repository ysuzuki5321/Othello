using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilitys
{
    public static class NumberUtility
    {
        #region "引数の値を使用し、数値の操作を行う関数を戻す"
        public static Func<int> IncNum(this int num)
        {
            return () => ++num;
        }
        public static Func<int> DecNum(this int num)
        {
            return () => --num;
        }
        public static Func<int> KeepNum(this int num)
        {
            return () => num;
        }
        #endregion
        public static bool CheckNegativeNum(params int[] values)
        {
            return (values.Aggregate((x,y) => x | y) & int.MinValue) == int.MinValue;
        }
        public static int ConvertTableIndexToLong(int rowIndex,int colIndex)
        {
            return (rowIndex << 16) | colIndex;
        }
    }
}
