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
using System.Windows.Shapes;

namespace QuotaGeneratorClient
{
    /// <summary>
    /// Логика взаимодействия для ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        // client является подключенным к серверу клиентом либо null если подключения не было
        private Client client;

        public Client ConnectedClient { get {  return client; } }

        public ConnectWindow()
        {
            InitializeComponent();
            client = null;
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serverIpStr = serverIpTextBox.Text;
                int port = Convert.ToInt32(serverPortTextBox.Text);
                client = new Client(serverIpStr, port);
                MessageBox.Show("Connection established successfully", 
                    "Connection OK", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();    // закрыть форму после успешного подключения
            } catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
