using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.Properties;
namespace Othello.OthelloClasses.Player
{
    public interface IPlayer
    {
        /// <summary>
        /// プレイヤーの色
        /// </summary>
        PlayerColor myColor { get; set; }
        Action TurnStart { get; set; }
      
        // セレクト管理
        bool lockSelect { get; set; }
        TableIndex selectCell { get; set; }
        Action TurnEnd { get; set; }

        // 表示値
        string identityVal { get; set; }
        int score { get; set; }
        bool myTurn { get; set; }
    }
}
