using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello.OthelloClasses.Player.PlayerComponent
{
    /// <summary>
    /// 成績クラス
    /// </summary>
    public class Record
    {

        public Record(int year,int month,int day,bool win)
        {
            this.year = year;
            this.month = month;
            this.day = day;
            this.win = win;
        }
        public int year { get; }
        public int month { get; }
        public int day { get; }
        public bool win { get; }
    }
}
