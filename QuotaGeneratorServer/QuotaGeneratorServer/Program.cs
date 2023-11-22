using QuotaGeneratorServer.Network;
using QuotaGeneratorServer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string serverIpStr = "127.0.0.1";
            int serverPort = 1024;
            int maxClientsCount = 1;
            int bufferSize = 1024;
            IQuotaGenerator quotaGenerator = new PlugQuotaGenerator();
            Server server = new Server(serverIpStr, serverPort, maxClientsCount);
            Console.WriteLine($"Starting server on {serverIpStr}:{serverPort}");
            server.Run(bufferSize, quotaGenerator);

            // 
            // 
        }
    }
}
