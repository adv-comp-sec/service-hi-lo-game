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
using System.Windows.Interop;
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

        private Socket sender = null;
        IPAddress IP;
        int port;
        public MainWindow()
        {
            InitializeComponent();
            Instructions.Text = "This is Hi-Lo game.";
        }

        private void ConnectButton_Click(object s, RoutedEventArgs ev)
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
            if (strIP == "")
            {
                Instructions.Text = "[ERROR: You must enter the IP Address to start.]";
                return;
            }
            else if (!IPAddress.TryParse(strIP, out IP))
            {
                Instructions.Text = "[ERROR: You must enter the right form of the IP Address.]";
                return;
            }
            else
            {
                IP = IPAddress.Parse(strIP);
            }

            // Valiate the port number
            String strPort = PortNumber.Text;
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

            IPEndPoint remoteEP = new IPEndPoint(IP, port);
            sender = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);

                Instructions.Text += "Socket connected to " + sender.RemoteEndPoint.ToString() + "\n";
                Name.IsEnabled = IPAdd.IsEnabled = PortNumber.IsEnabled = ConnectButton.IsEnabled = false;
                GuessNumber.IsEnabled = GuessNumberButton.IsEnabled = true;

            }
            catch (ArgumentNullException ane)
            {
                Instructions.Text += "ArgumentNullException : " + ane.ToString();
            }
            catch (SocketException se)
            {
                Instructions.Text += "SocketException : " + se.ToString();
            }
            catch (Exception e)
            {
                Instructions.Text += "Unexpected exception : " + e.ToString();
            }

        }

        private void GuessNumberButton_Click(object s, RoutedEventArgs e)
        {

            IPEndPoint remoteEP = new IPEndPoint(IP, port);
            sender = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEP);

            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];

            // Validate the number 
            String strNumber = GuessNumber.Text;
            byte[] number;

            if (strNumber == "")
            {
                Instructions.Text = "[ERROR: You must guess the number first.]";
                return;
            }
            else
            {
                number = Encoding.ASCII.GetBytes(strNumber);
            }

            // Send the data through the socket.  
            int bytesSent = sender.Send(number);

            // Receive the response from the remote device.  
            int bytesRec = sender.Receive(bytes);

            Instructions.Text = Encoding.ASCII.GetString(bytes, 0, bytesRec);

            // Check if the user win
            if (Instructions.Text == "You Win!")
            {
                GuessNumberButton.Content = "Play Again";
            }
        }

        private void StopButton_Click(object s, RoutedEventArgs e)
        {
            // Data buffer for incoming data.  
            byte[] bytes = new byte[1024];
            byte[] exitCode = Encoding.ASCII.GetBytes("E");

            MessageBoxResult closingResult = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo);

            if (closingResult == MessageBoxResult.Yes)
            {
                if (sender != null && sender.Connected)
                {
                    int bytesSent = sender.Send(exitCode);
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                Environment.Exit(0);
            }
            else if (closingResult == MessageBoxResult.No)
            {
                // do nothing
            }

        }

        /*
        * FUNCTION:	    CloseButton_Click()
        * DESCRIPTION:	Close the window.
        * PARAMETERS:
        *       object sender:      The object has an event.
        *       CancleEventArgs e:  The cancel event.
        * RETURNS:      none
        */
        private void CloseButton_Click(object s, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult closingResult = MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButton.YesNo);

            if (closingResult == MessageBoxResult.Yes)
            {
                if (sender != null && sender.Connected)
                {
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                Environment.Exit(0);
            }
            else if (closingResult == MessageBoxResult.No)
            {
                // do nothing
            }
        }

    }

}
