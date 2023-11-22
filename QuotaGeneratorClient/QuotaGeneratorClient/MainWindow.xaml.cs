using QuotaGeneratorClient.Network;
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

namespace QuotaGeneratorClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            // выйти из приложения
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // обработка выхода
            var result = MessageBox.Show(
                "Are you really want to exit?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;    
            }
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            // 1. открыть окно подключения к серверу
            ConnectWindow connectWindow = new ConnectWindow();
            connectWindow.ShowDialog();
            Client connectedClient = connectWindow.ConnectedClient;
            if (connectedClient != null)
            {
                // работать с этим клиентом
                CommunicateWindow communicateWindow = new CommunicateWindow(connectedClient);
                Hide();
                communicateWindow.ShowDialog();
                Show();
            } else
            {
                MessageBox.Show("Connection not established", "Not connected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
