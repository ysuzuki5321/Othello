using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P2PLIB;
using System.ComponentModel;
using Utilitys;

namespace Othello.OthelloClasses.Player
{
    internal class NetworkPlayer : NotifyPropChanged, IPlayer
    {
        private UDP netWork;
        public NetworkPlayer()
        {
            
        }
        public bool lockSelect { get; set; } = true;
        public TableIndex selectCell { get; set; } = new TableIndex(0, 0);

        public PlayerColor myColor { get; set; }

        internal NetworkPlayer(PlayerColor myColor)
        {
            this.myColor = myColor;
        }
        public Action TurnEnd { get; set; }

        public Action TurnStart { get; set; }
        public string identityVal { get; set; }
        public int score { get; set; }
        private bool _myTurn;
        public bool myTurn
        {
            get
            {
                return _myTurn;
            }
            set
            {
                _myTurn = value;
                OnPropertyChanged("myTurn");
            }
        }
    }
}
