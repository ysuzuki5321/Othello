using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public enum CellState
    {
        empty = 0b0100
        ,nextClickPlace = 0b1000
        ,white = 0b0001
        ,black = 0b0010
    }

    public enum PlayerColor
    {
        black
        ,white
    }

    public enum PlayType
    {
        CPU
        ,Network
        ,SingleForm
    }
}
