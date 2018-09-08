using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace P2PLIB
{
    public class UDP
    {
        private UdpClient client;
        private UdpClient recieveClient;
        private IPEndPoint localEndPoint;
        private IPEndPoint sendEndPoint;
        public UDP(IPAddress ip,int port,IPAddress sendIp,int sendPort)
        {
            this.localEndPoint = new IPEndPoint(ip,port);
            this.client = new UdpClient();
            recieveClient = new UdpClient(this.localEndPoint);
            this.sendEndPoint = new IPEndPoint(sendIp, sendPort);
        }

        public void Send<T>(T data)
        {
            
            var bFormatter = new BinaryFormatter();
            using (var mem = new MemoryStream())
            {
                bFormatter.Serialize(mem, data);
                var bytes = mem.GetBuffer();
                client.Send(bytes, bytes.Length, this.sendEndPoint);

            }
        }     

        public T Recieve<T>()
        {
            IPEndPoint remoteEP = null;
            var recieve = recieveClient.Receive(ref remoteEP);
            var bFormatter = new BinaryFormatter();
            using (var mem = new MemoryStream())
            {
                mem.Write(recieve, 0, recieve.Length);
                mem.Position = 0;
                var res = (T)bFormatter.Deserialize(mem);
                return res;
            }
        }
    }
}
