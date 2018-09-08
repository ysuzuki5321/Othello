using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OthelloWindowsForm.OthelloControls;
namespace OthelloWindowsForm
{
    public partial class Form1 : Form
    {
        [Browsable(true)]
        public int CellNum { get; set; } = 8;
        private OthelloWindowsForm OthelloObj ;
        public Form1()
        {
            InitializeComponent();
            this.OthelloObj = new OthelloWindowsForm(this.CellNum);
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetOthelloControl();
            int width = this.Width;
            var cellSize = new Size(width / OthelloObj.CellNum, width / OthelloObj.CellNum);
            Width += 30;
            Height += 50;
            for (int rowIndex = 0; rowIndex < OthelloObj.CellNum; rowIndex++)
            {
                for (int colIndex = 0; colIndex < OthelloObj.CellNum; colIndex++)
                {
                    var cell = new CellControl(rowIndex, colIndex);
                    cell.Size = cellSize;
                    cell.Location = new Point(cellSize.Width * rowIndex + rowIndex, cellSize.Width * colIndex + colIndex);
                    cell.DataBindings.Add("state", OthelloObj.table[rowIndex, colIndex], "state", false, DataSourceUpdateMode.OnPropertyChanged);
                    Controls[0].Controls.Add(cell);
                    OthelloObj.table[rowIndex, colIndex].ChangeState(OthelloObj.GetInitialCellState(OthelloObj.CellNum / 2, rowIndex, colIndex));
                    cell.SizeMode = PictureBoxSizeMode.StretchImage;
                    cell.MouseClick += (cellSender, CellE) => OthelloObj.CellSelect(cellSender, CellE);
                }
            }

            var lblPlayerScore = new Label();
            var pctIndicator = new IndicatorControl();
            var lblPlayerIdentity = new Label();
            lblPlayerScore.Font = new Font(lblPlayerScore.Font.FontFamily,18);
            lblPlayerScore.DataBindings.Add("Text", OthelloObj.players[0], "identityVal");
            lblPlayerScore.Location = new Point(pctIndicator.Right, 0);
            pctIndicator.DataBindings.Add("state", OthelloObj.players[0], "myTurn");
            Controls[1].Controls.Add(pctIndicator);
            Controls[1].Controls.Add(lblPlayerScore);
            var lblPlayerScore2 = new Label();
            var pctIndicator2 = new IndicatorControl();
            lblPlayerScore2.Font = new Font(lblPlayerScore2.Font.FontFamily, 18);
            lblPlayerScore2.DataBindings.Add("Text", OthelloObj.players[1], "identityVal");
            lblPlayerScore2.Location = new Point(pctIndicator2.Right, 0);
            pctIndicator2.DataBindings.Add("state", OthelloObj.players[1], "myTurn");
            Controls[2].Controls.Add(pctIndicator2);
            Controls[2].Controls.Add(lblPlayerScore2);

            //pnlScoreBoard.Controls.Add(lblPlayerIdentity);
            //pnlScoreBoard.Controls.Add(lblPlayerScore);
            //pnlScoreBoard.Controls.Add(pctIndicator);

            OthelloObj.MarkNextClickPlace(Othello.OthelloClass.playerColorConverter[OthelloObj.nowPlayer.myColor]);
            OthelloObj.gameStarted = true;
            OthelloObj.nowPlayer.TurnStart();
        }    
        
        private void SetOthelloControl()
        {
            
            var pnlBoard = new Panel();
            Controls.Add(pnlBoard);
            this.Height = this.Width;
            pnlBoard.Height = this.Height;
            pnlBoard.Width = this.Width;
            var pnlPlayerScore = CreateScoreBoard();
            var pnlOpponentScore = CreateScoreBoard();
            Controls.Add(pnlPlayerScore);
            Controls.Add(pnlOpponentScore);
            pnlBoard.Location = new Point(pnlPlayerScore.Left, pnlPlayerScore.Bottom + 1);
            pnlOpponentScore.Location = new Point(pnlBoard.Left, pnlBoard.Bottom + 1);
            this.Height += pnlPlayerScore.Height * 2;
        }

        private Panel CreateScoreBoard()
        {
            var pnlScoreBoard = new Panel();
            pnlScoreBoard.Width = this.Width;
            pnlScoreBoard.Height = 100;
            pnlScoreBoard.Left += 5;
            pnlScoreBoard.BackColor = Color.Red;
            return pnlScoreBoard;
        }
    }
}
