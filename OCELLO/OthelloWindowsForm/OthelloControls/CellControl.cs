using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Othello;
using Othello.OthelloClasses;
namespace OthelloWindowsForm
{
    public partial class CellControl : PictureBox,ICellControl
    {
        private CellState _state = CellState.empty;
        public CellState state {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
                this.BeginInvoke(new Action(() => this.Image = CommonObject.cellImage[this._state]));                    
            }
        }

        public int rowIndex { get; }
        public int colIndex { get; }
 
        public CellControl(int rowIndex,int colIndex) 
            :base()
        {
            this.rowIndex = rowIndex;
            this.colIndex = colIndex;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
