using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorClient.Network
{
    // Client обеспечивает взаимодействие клиента с сервером
    public class Client : IDisposable
    {
        // объекты клиента для взаимодействия с сервером
        private Sender sender;
        private Receiver receiver;

        // Конструктор, инициализирущий клиента
        public Client(string serverIpStr, int serverPort, int bufferSize=1024)
        {
            // подготовка сокета клиента
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress serverIp = IPAddress.Parse(serverIpStr);
            IPEndPoint ipEndPoint = new IPEndPoint(serverIp, serverPort);
            // подключаемся к серверу
            client.Connect(ipEndPoint); // в данном месте при ошибке подключения выбрасывается exception
            // если подключились, то создаем объекты для общения
            sender = new Sender(client);
            receiver = new Receiver(client, bufferSize);
            if (client.Poll(10000, SelectMode.SelectRead))
            {
                throw new ApplicationException(receiver.Receive()); 
            }
        }

        public void Dispose()
        {
            sender.Dispose();
            receiver.Dispose();
        }

        // SendServerCommand - отправляет команду серверу и возвращает результат
        public string SendServerCommand(string command) {
            sender.Send(command);
            return receiver.Receive();
        }
    }
}
