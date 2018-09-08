using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitys;
namespace Othello.OthelloClasses.Player.PlayerComponent
{
    public class PlayerName
    {
        private string id;
        private string _name;
        public string name { get; set; }
        public override int GetHashCode()
        {

            return base.GetHashCode();
        }
    }
}
