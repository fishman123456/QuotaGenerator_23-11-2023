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
    /// Логика взаимодействия для CommunicateWindow.xaml
    /// </summary>
    public partial class CommunicateWindow : Window
    {
        // Объект для взаимодействия с сервером
        private Client client;
        public CommunicateWindow(Client client)
        {
            InitializeComponent();
            this.client = client;
        }

        private void sendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string serverCommand = serverCommandTextBox.Text;
                if (serverCommand == "quit")
                {
                    Close();
                }
                string result = client.SendServerCommand(serverCommand);
                resultTextBox.Text = result;
            } catch (Exception ex)
            {
                MessageBox.Show($"Send command error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // обработка выхода
        private void MakeQuit()
        {
            try
            {
                string result = client.SendServerCommand("quit");
                resultTextBox.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Send quit command error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            } finally
            {
                client.Dispose();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MakeQuit();
        }
    }
}
