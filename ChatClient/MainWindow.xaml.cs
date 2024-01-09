﻿using System;
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
using ChatClient.ChatService;

namespace ChatClient
{
    public partial class MainWindow : Window, IChatServiceCallback
    {
        int Id;
        private bool IsConnected { get; set; } = false;
        ChatServiceClient Client;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Client = new ChatServiceClient(new System.ServiceModel.InstanceContext(this));
        }

        private void UpdateUIState(bool isConnected)
        {
            tbUsername.IsEnabled = !isConnected;
            btConnDisconn.Content = isConnected ? "Disconnect" : "Connect";
            IsConnected = isConnected;
        }

        private void ConnectUser()
        {
            Id = Client.Connect(tbUsername.Text);
            UpdateUIState(true);
        }

        private void DisconnectUser()
        {
            Client.Disconnect(Id);

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

        public void MessageCallback(string message)
        {
            lbChat.Items.Add(message);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsConnected)
            {
                DisconnectUser();
            }
        }
    }
}
