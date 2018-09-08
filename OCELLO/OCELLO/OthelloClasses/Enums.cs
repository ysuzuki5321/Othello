using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public enum CellState
    {
        empty = 0x10
        ,nextClickPlace = 0x20
        ,white = 0x01
        ,black = 0x02
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
