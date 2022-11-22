using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace HiLoGame_Server
{
    internal class SynchronousSocketListenenr
    {
        private int minNumber = Convert.ToInt32(ConfigurationManager.AppSettings["MinNumber"]); // get range from config file
        private int maxNumber = Convert.ToInt32(ConfigurationManager.AppSettings["MaxNumber"]);

        public int randomNumber = 0;   

        internal void StartListening()
        {
            

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());       // name of the host 
            IPAddress ipAddress = ipHostInfo.AddressList[0];                    // ip address of the host
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 13000);        // local end point for the socket

            Socket lisneter = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);     // create socket

            Random random = new Random();
            randomNumber = random.Next(minNumber, maxNumber); // generate random number within the range

            try
            {
                lisneter.Bind(localEndPoint);   // bind the socket
                lisneter.Listen(10);

                while(true)
                {
                    Socket handler = lisneter.Accept();     // accepts connection
                    ParameterizedThreadStart ts = new ParameterizedThreadStart(Worker);
                    Thread clientThread = new Thread(ts);
                    clientThread.Start(client);
                }

            }
            catch
            {

            }
        }

        public void Worker(Object o)
        {
            String data = null;                 // data passed by the client
            Byte[] bytes = new Byte[1024];      // data buffer
            Socket handler = (Socket)o;         // cast object to socket

            while (true)                        // loop to get all the data received by the client
            {
                int bytesReceived = handler.Receive(bytes);
                
                data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);      // process the data to ASCII values (decoding)

                if (data.IndexOf("<EOF>") > -1)     // check whether is the end of the data
                {
                    break;
                }
            }

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
    }


}