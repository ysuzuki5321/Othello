using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using P2PLIB;
namespace VirtualNetworkPlayer
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (File.Exists(SOCKET_SAVE_TEXT)) ReadSaveFile();


        }

        private void ReadSaveFile()
        {
            var addressSet = File.ReadAllText(SOCKET_SAVE_TEXT).Split(',');
            this.txtIp.Text = addressSet[0];
            this.txtPort.Text = addressSet[1];
            this.txtSendIP.Text = addressSet[2];
            this.txtSendPort.Text = addressSet[3];
        }

        private const string SOCKET_SAVE_TEXT = "SAVE001.txt";
        private UDP udp;
        private void btnUDPSocketCreate_Click(object sender, EventArgs e)
        {
            var ip = txtIp.Text.TrimEnd();
            var port = txtPort.Text.TrimEnd();
            var sendIP = txtSendIP.Text.TrimEnd();
            var sendPort = txtSendPort.Text.TrimEnd();
            try
            {
                udp = new UDP(IPAddress.Parse(ip), int.Parse(port), IPAddress.Parse( sendIP), int.Parse(sendPort));
                File.WriteAllText(SOCKET_SAVE_TEXT, string.Format($"{ip},{port},{sendIP},{sendPort}"));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void AsyncReadSocktPosVal()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var ret = udp.Recieve<int>();
                        var rowIndex = ret >> 16;
                        var colIndex = ret & 0xFF;
                        MessageBox.Show($"rowIndex:{rowIndex} colIndex:{colIndex}");
                    }
                    catch
                    {

                    }
                }

            });

        }
        private void AsyncReadSocketConnectVal()
        {
                    var ret = udp.Recieve<string>();
                    MessageBox.Show(ret);
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            int rowIndex;
            int colIndex;
            if(int.TryParse(txtRow.Text,out rowIndex) && int.TryParse(txtCol.Text,out colIndex))
            {
                udp.Send((rowIndex << 16) | colIndex);
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            int val;
            if(int.TryParse(txtRandNum.Text,out val))
            {
                udp.Send(val);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                    System.Threading.Thread.Sleep(1000);
                    udp.Send("Connection");

            });

            //udp.Send("Connection");
            AsyncReadSocketConnectVal();
        }
    }
}
