using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A06Service
{
    internal class GameEngine
    {
        private int minNumber = Convert.ToInt32(ConfigurationManager.AppSettings["MinNumber"]); // get range from config file
        private int maxNumber = Convert.ToInt32(ConfigurationManager.AppSettings["MaxNumber"]);

        public int randomNumber = 0;

        public byte[] msg = null;

        public Socket handler = null;

        public volatile bool listen = true;

        public void StartListening()
        {

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());       // name of the host 
            IPAddress ipAddress = ipHostInfo.AddressList[0];                    // ip address of the host
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 13000);        // local end point for the socket

            Socket lisneter = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);     // create socket

            Random random = new Random();
            randomNumber = random.Next(minNumber, maxNumber); // generate random number within the range

            try
            {
                Console.WriteLine("Server");
                lisneter.Bind(localEndPoint);   // bind the socket
                lisneter.Listen(10);

                while (listen)
                {
                    handler = lisneter.Accept();     // accepts connection
                    ParameterizedThreadStart ts = new ParameterizedThreadStart(Worker);
                    Thread clientThread = new Thread(ts);
                    clientThread.Start(handler);
                }
            }
            catch (Exception ex)
            {
                msg = Encoding.ASCII.GetBytes(ex.ToString());
                handler.Send(msg);
                Logger.Log(ex.ToString());
            }
            finally
            {
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }

        public void Worker(Object o)
        {
            String data = null;                 // data passed by the client
            Byte[] bytes = new Byte[1024];      // data buffer
            Socket handler = (Socket)o;         // cast object to socket

            int bytesReceived = handler.Receive(bytes);

            data += Encoding.ASCII.GetString(bytes, 0, bytesReceived);      // process the data to ASCII values (decoding)

            int guess = Convert.ToInt32(data);

            if (guess == randomNumber)            // check if guess is correct
            {
                msg = Encoding.ASCII.GetBytes("You Win!");
            }
            else                                        // change the range accordingly the guess
            {
                if (guess < randomNumber)
                {
                    minNumber = guess + 1;
                    msg = Encoding.ASCII.GetBytes("Your allowable range is " + minNumber + " to " + maxNumber);

                }
                else
                {
                    maxNumber = guess - 1;
                    msg = Encoding.ASCII.GetBytes("Your allowable range is " + minNumber + " to " + maxNumber);
                }
            }

            handler.Send(msg);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }

        public void StopListening()
        {
            //msg = Encoding.ASCII.GetBytes("Stopping the server.");
            //handler.Send(msg);
            listen = false;
        }
    }
}
