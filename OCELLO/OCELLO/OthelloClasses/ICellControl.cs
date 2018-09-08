using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.OthelloClasses
{
    public interface ICellControl
    {
        int rowIndex { get; }
        int colIndex { get; }
    }
}
