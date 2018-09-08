using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.OthelloClasses.Player;
using System.ComponentModel;
using Utilitys;

namespace Othello
{
    internal class Player : NotifyPropChanged, IPlayer
    {
        public PlayerColor myColor { get; set; }

        public bool lockSelect { get; set; }
        public TableIndex selectCell { get; set; } = new TableIndex(0, 0);
        internal Player(PlayerColor myColor)
        {
            this.myColor = myColor;
        }
        public Action TurnStart { get; set; }

        public Action TurnEnd { get; set; }
        public string identityVal { get ; set ; }
        public int score { get ; set ; }
        private bool _myTurn;
        public bool myTurn {
            get
            {
                return _myTurn;
            }
            set {
                _myTurn = value;
                OnPropertyChanged("myTurn");
            }
        }


    }
}
