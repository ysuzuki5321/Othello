using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.OthelloClasses;

namespace OthelloWindowsForm
{
    public class OthelloWindowsForm : Othello.OthelloClass
    {
        public OthelloWindowsForm(int cellCount)
            : base(cellCount)
        {
            
        }

        internal void CellSelect(object sender, EventArgs e)
        {
            if (nowPlayer.lockSelect) return;
            nowPlayer.selectCell.rowIndex = ((ICellControl)sender).rowIndex;
            nowPlayer.selectCell.colIndex = ((ICellControl)sender).colIndex;
            StartReverse(nowPlayer.selectCell.rowIndex, nowPlayer.selectCell.colIndex);
        }

        internal void Initialize()
        {

        }
    }
}
