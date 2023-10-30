using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Othello.OthelloClasses;
using Othello.OthelloClasses.Player;
using Utilitys;
using System.Net;
using System.IO;
namespace Othello
{
    public class OthelloClass
    {
        public static Dictionary<PlayerColor, CellState> playerColorConverter
            = new Dictionary<PlayerColor, CellState>() {
                                { PlayerColor.white,CellState.white }
                                , { PlayerColor.black,CellState.black} };
        public IPlayer[] players { get; }
        public bool gameStarted { get; set; }
        private int nowIndex = 0;
        private const int NO_SELECT = 0x70000000;
        public IPlayer nowPlayer {
            get
            {                
                return players[nowIndex];
            }
        }
        private Dictionary<CellState,int> cellStatesCount = new Dictionary<CellState, int>()
        {{CellState.black,0 },{ CellState.empty,0},{CellState.nextClickPlace,0},{ CellState.white,0}};

        private CellState nowPlayerCellState() => playerColorConverter[nowPlayer.myColor];
        private P2PLIB.UDP netWork;
        public OthelloClass(int cellCount)
        {
            netWork = new P2PLIB.UDP(IPAddress.Parse(Properties.Settings.Default.myIp)
                , Properties.Settings.Default.myport
                , IPAddress.Parse(Properties.Settings.Default.guestIP)
                , Properties.Settings.Default.guestport);
            Connection();
            File.WriteAllText("log.text", "Connection");
            // 順序決め 
            (PlayerColor hostPlayer, PlayerColor guestPlayer) order = DecideOrder();

            players = new IPlayer[2];
            players[0] = new Player(order.hostPlayer);
            players[0].identityVal = "おぬし";
            players[0].TurnStart = () => {
                players[0].myTurn = true;
                SamaryCellState();
                
                if (cellStatesCount[CellState.nextClickPlace] == 0)
                {
                    nowPlayer.selectCell.rowIndex = 0x7000;
                    nowPlayer.selectCell.colIndex = 0;
                    MarkNextClickPlace(playerColorConverter[players[1].myColor]);
                    NextTurn();
                }

                if(cellStatesCount[CellState.empty] == 0)
                {
                    //MessageBox.Show(cellStatesCount[playerColorConverter[players[0].myColor]].ToString());

                }
            };  
            players[0].TurnEnd = () => {
                netWork.Send(NumberUtility.ConvertTableIndexToLong(
                    nowPlayer.selectCell.rowIndex, nowPlayer.selectCell.colIndex));
                players[0].myTurn = false;
            };
            players[1] = new NetworkPlayer(order.guestPlayer);
            players[1].identityVal = "相手";
            players[1].TurnStart = () => {
                Task.Factory.StartNew(() =>
                {
                    var res = netWork.Recieve<int>();                        
                    Console.WriteLine(res);
                    var rowIndex = res >> 16;
                    var colIndex = res & 0xFF;
                    if(res != NO_SELECT)
                    {
                        StartReverse(rowIndex, colIndex);
                    }
                    else
                    {                        
                        MarkNextClickPlace(playerColorConverter[players[0].myColor]);
                        NextTurn();
                    }
                });
                players[1].myTurn = true;
            };
            players[1].TurnEnd = () => {
                players[1].myTurn = false;
            };

            this.CellNum = cellCount;
            table = new Cell[this.CellNum, this.CellNum];
            for (int rowIndex = 0; rowIndex < this.CellNum; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.CellNum; colIndex++)
                {
                    table[rowIndex, colIndex] = new Cell(this, colIndex, rowIndex);
                }
            }
        }
        
        // 勝敗表示

        /// <summary>
        /// 順序決め
        /// </summary>
        /// <returns></returns>
        private (PlayerColor, PlayerColor) DecideOrder()
        {
            var rand = new Random();
            int sendVal = 0;
            int reciveVal = 0;
            while (sendVal == reciveVal)
            {
                sendVal = rand.Next(DateTime.Now.Millisecond);
                netWork.Send(sendVal);
                reciveVal = netWork.Recieve<int>();
                nowIndex = (sendVal < reciveVal) ? 1 : 0;
                File.AppendAllText("log.text", $"sendVal:{sendVal} reciveVal:{reciveVal}");
            }
            var host = (nowIndex == 0) ? PlayerColor.black : PlayerColor.white;
            var guest = (nowIndex == 0) ? PlayerColor.white : PlayerColor.black;
            return (host, guest);
        }       
        private void Connection()
        {
            //Cursor.Current = Cursors.WaitCursor;
            const string conRequest = "Connection";
            bool connect = false;
            var sendTask = Task.Run(() =>
            {
                while (!connect)
                {
                    System.Threading.Thread.Sleep(1000);
                    
                    if(!connect)netWork.Send(conRequest);
                }

            });

            var response = netWork.Recieve<string>();
            connect = true;
            if (response == conRequest)
            {
                netWork.Send("Next");
            }
            //Cursor.Current = Cursors.Default;
        }

        private const int MIN_CELL_NUM = 6;
        private const int MAX_CELL_NUM = 20;
        private int _cellNum = MIN_CELL_NUM;
        public int CellNum
        {
            get
            {
                return this._cellNum;
            }
            set
            {
                if (value < MIN_CELL_NUM)
                {
                    this._cellNum = MIN_CELL_NUM;
                }
                else if (MAX_CELL_NUM < value)
                {
                    this._cellNum = MAX_CELL_NUM;
                }
                this._cellNum = value;
            }
        }
        public Cell[,] table { get; set; }
        private int NextIndex() => (this.nowIndex + 1) % 2;

        protected void StartReverse(int rowIndex,int colIndex)
        {
            if (CheckSelectCell(rowIndex, colIndex))
            {
                NextTurn();
            }
        }

        protected void NextTurn()
        {
            this.nowPlayer.TurnEnd();
            // プレイヤーChange 
            this.nowIndex = NextIndex();
            this.nowPlayer.TurnStart();

        }

        private void SamaryCellState()
        {
            foreach(CellState cState in Enum.GetValues(typeof(CellState))){
                cellStatesCount[cState] = 0;
            }
            for (int rowIndex = 0; rowIndex < this.CellNum; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.CellNum; colIndex++)
                {
                    cellStatesCount[table[rowIndex, colIndex].state]++;
                }
            }
        }

        private bool CheckSelectCell(int rowIndex, int colIndex)
        {
            if (!this.gameStarted) return false;
            if (table[rowIndex, colIndex].state != CellState.nextClickPlace) return false;
            bool stateChange = ReverseCheck(rowIndex, colIndex, true, this.nowPlayerCellState());
            if (stateChange)
            {
                table[rowIndex, colIndex].ChangeState(playerColorConverter[this.nowPlayer.myColor]);
                CellState nextColor = playerColorConverter[players[NextIndex()].myColor];
                MarkNextClickPlace(nextColor);
            }
            return stateChange;
        }

        public void MarkNextClickPlace(CellState nextColor)
        {
            for (int rowIndex = 0; rowIndex < this.CellNum; rowIndex++)
            {
                for (int colIndex = 0; colIndex < this.CellNum; colIndex++)
                {
                    if (((table[rowIndex, colIndex].state & (CellState.empty | CellState.nextClickPlace)) > 0))
                        table[rowIndex, colIndex].state = ReverseCheck(rowIndex, colIndex, false, nextColor) 
                            ? CellState.nextClickPlace : CellState.empty;
                }
            }
        }

        private bool ReverseCheck(int rowIndex, int colIndex, bool isReverse, CellState checkCellState)
        {

            return ReverseCellState(rowIndex.IncNum(), colIndex.IncNum(), checkCellState, checkCellState, isReverse)
                | ReverseCellState(rowIndex.IncNum(), colIndex.KeepNum(), checkCellState, checkCellState, isReverse)
                | ReverseCellState(rowIndex.KeepNum(), colIndex.IncNum(), checkCellState, checkCellState, isReverse)
                | ReverseCellState(rowIndex.IncNum(), colIndex.DecNum(), checkCellState, checkCellState, isReverse)
                | ReverseCellState(rowIndex.DecNum(), colIndex.IncNum(), checkCellState, checkCellState, isReverse)
                | ReverseCellState(rowIndex.DecNum(), colIndex.DecNum(), checkCellState, checkCellState, isReverse)
                | ReverseCellState(rowIndex.DecNum(), colIndex.KeepNum(), checkCellState, checkCellState, isReverse)
                | ReverseCellState(rowIndex.KeepNum(), colIndex.DecNum(), checkCellState, checkCellState, isReverse);
        }

        private bool ReverseCellState(Func<int> getRowIndex, Func<int> getColIndex, CellState beforeState, CellState clickCellState,bool isReverse)
        {

            int nowRowIndex = getRowIndex();
            int nowColIndex = getColIndex();
            if(NumberUtility.CheckNegativeNum(nowRowIndex,nowColIndex) || this.CellNum <= nowRowIndex || this.CellNum <= nowColIndex)
            {
                return false;
            }
            Cell nowCell = table[nowRowIndex, nowColIndex];

            // 複数条件をbit演算で処理する方法
            // var a  = (empty | nextClickPlace)  = 0b1100
            // empty            & a == 0b0100 = 4 > 0
            // nextClickPlace   & a == 0b1000 = 8 > 0
            // white            & a == 0b0000 = 0 
            // block            & a == 0b0000 = 0 
            // 通常のif文
            //if(nowCell.state == empty || nowCell.state == nextClickPlace)
            if ((nowCell.state & (CellState.empty | CellState.nextClickPlace)) > 0)
            {
                return false;
            }
            else if(nowCell.state == clickCellState && nowCell.state != beforeState )
            {
                return true;
            }
            else if(nowCell.state == clickCellState)
            {
                return false;
            }
            else
            {
                bool nextState = ReverseCellState(getRowIndex, getColIndex, nowCell.state, clickCellState, isReverse);
                if (nextState && isReverse)
                {
                    nowCell.state = clickCellState;
                }
                return nextState ;
            }
        }

        /// <summary>
        /// 初期セル設定
        /// </summary>
        /// <param name="halfCellNum"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public CellState GetInitialCellState(int halfCellNum, int rowIndex, int colIndex)
        {
            if ((rowIndex == halfCellNum - 1 && colIndex == halfCellNum - 1)
                || (rowIndex == halfCellNum && colIndex == halfCellNum)) return CellState.black;
            if ((rowIndex == halfCellNum - 1 && colIndex == halfCellNum)
                || (rowIndex == halfCellNum && colIndex == halfCellNum - 1)) return CellState.white;
            return CellState.empty;
        }

    }
}
