using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Othello
{
    /// <summary>
    /// Cell Class
    /// </summary>
    public class Cell : INotifyPropertyChanged
    {

        public TableIndex tableIndex ;
        public event PropertyChangedEventHandler PropertyChanged;
        private OthelloClass body;
        private CellState _state = CellState.empty;
                
        public CellState state
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
                if (PropertyChanged != null )
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("state"));
                }
            }
        }

        public Cell(OthelloClass OthelloBody, int rowIndex, int columnIndex)
        {
            this.body = OthelloBody;
            this.tableIndex = new TableIndex(rowIndex, columnIndex);
        }

        public void ChangeState(CellState state)
        {
            this.state = state;
        }

    }
}
