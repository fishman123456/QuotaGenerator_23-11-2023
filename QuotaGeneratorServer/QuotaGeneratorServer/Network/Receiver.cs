using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorServer.Network
{
    // Receiver - класс для чтения текстового сообщения через сокет
    internal class Receiver : IDisposable
    {
        private Socket socket;
        private byte[] buffer;

        public Receiver(Socket socket, int bufferSize)
        {
            this.socket = socket;
            buffer = new byte[bufferSize];
        }

        public void Dispose()
        {
            socket?.Close();
        }

        public string Receive()
        {
            int bytesRead =socket.Receive(buffer);
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            return message;
        }
    }
}
