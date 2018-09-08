using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hash;
using Othello.OthelloClasses.Player.PlayerComponent;

namespace Othello.OthelloClasses.Player
{
    [Serializable]
    public class PlayerData
    {
        public string id { get; set; } // プレイヤーID
        public string pass { get; set; } // ログインパスワード
        public List<Record> record { get; set; } // レコード

        // SHA256でハッシュ化
        public new string GetHashCode()
        {            
            return HashBuilder.Build(id);
        }

        public PlayerAddress address { get; set; } // アクセス先
    }
}
