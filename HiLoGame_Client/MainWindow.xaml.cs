using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HiLoGame_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Instructions.Text = "This is Hi-Lo game.";
        }

        private void ConnectButton_Click(object s, RoutedEventArgs e)
        {
            // Validate the user name
            String name = Name.Text;
            if (name == "")
            {
                Instructions.Text = "[ERROR: You must enter the name to start.]";
                return;
            }

            // Validate the IP Address
            String strIP = IPAdd.Text;
            IPAddress IP;
            if (strIP == "")
            {
                Instructions.Text = "[ERROR: You must enter the IP Address to start.]";
                return;
            }
            else if (!IPAddress.TryParse(strIP, out IP))
            {
                Instructions.Text = "[ERROR: You must enter the right form of the IP Address. (e.g. 127.0.0.1)]";
                return;
            }
            else
            {
                IP = IPAddress.Parse(strIP);
            }

            // Valiate the port number
            String strPort = PortNumber.Text;
            int port;
            if (strPort == "")
            {
                Instructions.Text = "[ERROR: You must enter the port number to start.]";
                return;
            }
            else if (!int.TryParse(strPort, out port))
            {
                Instructions.Text = "[ERROR: You must enter the right form of the port number.]";
                return;
            }
            else
            {
                port = Convert.ToInt32(strPort);
            }

            // Start the game.
            Instructions.Text = "Hello, " + name + "! Let's start a game!\n";
            Instructions.Text += "Try to connect into " + IP.ToString() + ":" + port.ToString() + " server...\n";
            Socket sender = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
