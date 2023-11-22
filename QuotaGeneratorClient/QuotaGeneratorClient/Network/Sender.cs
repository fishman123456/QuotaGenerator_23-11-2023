using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorClient.Network
{
    // Sender - класс для отправки текстового сообщения через сокет
    internal class Sender : IDisposable
    {
        private Socket socket;

        public Sender(Socket socket)
        {
            this.socket = socket;
        }

        public void Dispose()
        {
            socket?.Close();
        }

        public void Send(string message)
        {
            socket.Send(Encoding.UTF8.GetBytes(message));
        }
    }
}
