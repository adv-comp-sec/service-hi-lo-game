using System;
using System.Collections.Generic;
using System.Linq;
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

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            // get a name first
            String name = Name.Text;
            if (name == "")
            {
                Instructions.Text = "[ERROR: You must enter the name to start.]";
            }

            try
            {
               
            }
            catch (Exception ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
        }

        private void Instructions_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
