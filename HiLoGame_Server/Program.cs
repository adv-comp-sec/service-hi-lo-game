using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HiLoGame_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SynchronousSocketListenenr ssl = new SynchronousSocketListenenr();
            ssl.StartListening();

        }
    }
}
