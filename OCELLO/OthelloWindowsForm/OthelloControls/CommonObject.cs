using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello;
namespace OthelloWindowsForm
{
    internal class CommonObject
    {
        // セルイメージマップ
        internal static Dictionary<CellState, Image> cellImage
            = new Dictionary<CellState, Image>()
            { {CellState.empty,Properties.Resources.empty }
                ,{CellState.white,Properties.Resources.white}
                ,{CellState.black,Properties.Resources.black }
                , {CellState.nextClickPlace,Properties.Resources.nextClickPlace }
            };

        internal static Dictionary<bool, Image> indicatorImage
            = new Dictionary<bool, Image>()
            {{true,Properties.Resources.Indicator}
                ,{false,Properties.Resources.IndicatorNull}
            };
    }
}
