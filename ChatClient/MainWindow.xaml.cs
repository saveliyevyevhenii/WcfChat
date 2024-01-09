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

namespace ChatClient
{
    public partial class MainWindow : Window
    {
        private bool IsConnected { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateUIState(bool isConnected)
        {
            tbUsername.IsEnabled = !isConnected;
            btConnDisconn.Content = isConnected ? "Disconnect" : "Connect";
            IsConnected = isConnected;
        }

        private void ConnectUser()
        {
            UpdateUIState(true);
        }

        private void DisconnectUser()
        {
            UpdateUIState(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }
    }
}
