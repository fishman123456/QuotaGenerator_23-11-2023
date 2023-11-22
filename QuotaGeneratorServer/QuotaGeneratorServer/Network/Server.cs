using QuotaGeneratorServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuotaGeneratorServer.Network
{
    // Server - класс, реализующий сервер, который выполняет подключение клиентов,
    // направляет клиентов на обработку а так же отключает их
    internal class Server
    {
        // поля
        private Socket server;          // сокет сервера
        private int maxClientsCount;    // максимально допустимое кол-во клиентов
        private int clientsCount;        // кол-во клиентов, подключенных в данный момент

        // конструктор - принимает строку ip сервера (IPv4), порт сервера и максимально допустимое кол-во клиентов
        public Server(string serverIpStr, int serverPort,  int maxClientsCount)
        {
            clientsCount = 0;
            this.maxClientsCount = maxClientsCount;
            // 1. создание и подготовка сокета
            IPAddress serverIp = IPAddress.Parse(serverIpStr);
            IPEndPoint serverEndpoint = new IPEndPoint(serverIp, serverPort);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            // 2. биндинг и перевод в режим прослушивания входящих подключений
            server.Bind(serverEndpoint);
            server.Listen(maxClientsCount + 1);  // резервируем одно подключение для клиентов, для которых не будет места
        }

        // Run - метод запуска сервера
        public void Run(int bufferSize, IQuotaGenerator quotaGenerator)
        {
            // Алгоритм работы сервера:
            // Сервер работает в вечном цикле следующим образом:
            //      1) Подключить очередного клиента
            //      2) Если мест достаточно, то направить работу с этим клиентом обработчику клиентов
            //      3) Если мест не достаточно, то отправить клиенту сообщение об этом и отключиться
            ClientProcessor clientProcessor = new ClientProcessor(bufferSize, quotaGenerator);
            while (true)
            {
                // 1. ожидание подключения клиента
                Socket client = server.Accept();
                // 2. клиент подключен
                if (clientsCount < maxClientsCount)
                {
                    // все нормально, можно работать
                    Interlocked.Increment(ref clientsCount);
                    Console.WriteLine($"{DateTime.Now.TimeOfDay}> connected {client.RemoteEndPoint} ({clientsCount}/{maxClientsCount})");
                    Thread clientProcessorThread = new Thread(() => clientProcessor.Process(client, ProcessEndHandler));
                    clientProcessorThread.Start();
                } else
                {
                    // достигнуто максимальное кол-во клиентов, необходимо
                    // отправить сообщение клиенту и отключить его
                    using (Sender sender = new Sender(client))
                    {
                        Console.WriteLine($"{DateTime.Now.TimeOfDay}> can`t connect {client.RemoteEndPoint} ({clientsCount}/{maxClientsCount})");
                        sender.Send($"The maximum number of connections has been reached ({clientsCount} / {maxClientsCount})");
                        client.Shutdown(SocketShutdown.Both);
                    }
                }
            }
        }

        // ProcessEndHandler - callback, уменьшающий кол-во подключенных клиентов
        // при завершении работы с клиентом внутри ClientProcessor
        private void ProcessEndHandler(object sender, EventArgs eventArgs)
        {
            // используем Interlocked чтобы избежать гонки данных при работе с переменной из разных потоков
            Socket client = (Socket)sender;
            Interlocked.Decrement(ref clientsCount);
            Console.WriteLine($"{DateTime.Now.TimeOfDay}> disconnected {client.RemoteEndPoint} ({clientsCount}/{maxClientsCount})");
        }
    }
}
