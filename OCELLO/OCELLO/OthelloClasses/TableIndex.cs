using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class TableIndex
    {
        public int rowIndex{get; set;} = 0;
        public int colIndex { get; set; } = 0;
        public TableIndex(int rowIndex,int colIndex)
        {
            this.rowIndex = rowIndex;
            this.colIndex = colIndex;
        }
    }
}
