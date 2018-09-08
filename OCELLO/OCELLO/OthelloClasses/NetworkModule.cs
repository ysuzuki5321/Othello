using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
namespace Othello.OthelloClasses
{
    /// <summary>
    /// ネットワーク
    /// </summary>
    internal class NetworkModule
    {
        // P2P接続形式であるため、UDP接続を使用する
        // 一手に関してはuLongで送受信を行う
        // 64bitを32bitで分け、High32bitにRowIndex、Low32bitにColIndexを配置する
        //  →コンバータが必要
        // プレイヤーデータのクラスが必要
        //  →ID、名前、成績
        // CPUのAIに関してもハッシュテーブルを使用する
        // 

    }
}
