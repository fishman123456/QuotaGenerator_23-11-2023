using QuotaGeneratorServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorServer.Network
{
    // ClientProcessor - обработчик, который работает с клиентом, получая от него команды и отправляя ответы
    internal class ClientProcessor
    {
        private int bufferSize;
        private IQuotaGenerator quotaGenerator;

        public ClientProcessor(int bufferSize, IQuotaGenerator quotaGenerator)
        {
            this.bufferSize = bufferSize;
            this.quotaGenerator = quotaGenerator;
        }

        // Process - метод обработки одного клиента, запускается в отдельном потоке
        public void Process(Socket client, EventHandler processEndHandler)
        {
            // Алгоритм работы с клиентом:
            // 1. считывает сообщение от клиента (команду)
            // 2. выполняет команду
            try
            {
                Sender sender = new Sender(client);
                Receiver receiver = new Receiver(client, bufferSize);
                bool isEnd = false;
                // цикл работы с клиентом
                while (!isEnd)
                {
                    // 1. считать сообщение от клиента
                    string command = receiver.Receive();
                    // 2. выполнить команду и отправить ответ
                    switch (command)
                    {
                        case "ping":
                            sender.Send("pong\n");
                            break;
                        case "quit":
                            sender.Send("bye-bye\n");
                            isEnd = true;
                            break;
                        case "quota":
                            sender.Send($"{quotaGenerator.GetRandomQuota()}\n");
                            break;
                        case "help":
                        default:
                            sender.Send("ping - check server availability; quit - end processing;\n");
                            break;
                    }
                }
            } catch (Exception ex) { 
                // ...
            } finally
            {
                processEndHandler(client, null);
                // завершать работу с клиентом
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }

    }
}
