using System;
using System.Net;
using System.Net.Sockets;

namespace HiLoGame_Server
{
    internal class SynchronousSocketListenenr
    {
        public string data = null;              // data from client

        internal void StartListening()
        {
            byte[] bytes = new Byte[1024];      // data buffer

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());       // name of the host 
            IPAddress ipAddress = ipHostInfo.AddressList[0];                    // ip address of the host
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 13000);        // local end point for the socket

            Socket lisneter = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);     // create socket

            try
            {
                lisneter.Bind(localEndPoint);   // bind the socket
                lisneter.Listen(10);

                Socket handler = lisneter.Accept();     // accepts connection
                data = null;

            }
            catch
            {

            }
        }
    }
}