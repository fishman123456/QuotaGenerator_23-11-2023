using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorServer.Service
{
    internal class PlugQuotaGenerator : IQuotaGenerator
    {
        private Random random = new Random();
        public string GetRandomQuota()
        {
            return new string[] {
                "Programming is the art of algorithm design and the craft of debugging errant code. – Ellen Ullman",
                "Experience is the name everyone gives to their mistakes. – Oscar Wilde",
                "Software is like sex: it’s better when it’s free. – Linus Torvalds",
                "A programmer's work is an endless search for perfect solutions that will lead to new discoveries and innovations",
            }[random.Next(0, 4)];
        }
    }
}
