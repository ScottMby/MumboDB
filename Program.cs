using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MumboDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Setup socket to use TCP and IPv4
            Socket listenSocket = new Socket(AddressFamily.InterNetwork,
                                     SocketType.Stream,
                                     ProtocolType.Tcp);

            //Bind the listening ip and port
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 2121);
            listenSocket.Bind(ep);

            listenSocket.Listen(1);

            //Accept when we get an incoming connection
            Socket connectedSocket = listenSocket.Accept();

            while(true)
            {
                //Set buffer 
                byte[] buffer = new byte[2048];

                Int32 bytesLength = connectedSocket.Receive(buffer, buffer.Length, SocketFlags.None);
                string command = Encoding.UTF8.GetString(buffer, 0, bytesLength);

            }
        }
    }
}
