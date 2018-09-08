using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Othello.OthelloClasses.Player.PlayerComponent
{
    public class PlayerAddress
    {
        public IPAddress ipAddress { get; set; }
        public int port { get; set; }

    }
}
