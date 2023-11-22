using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaGeneratorServer.Service
{
    // IQuotaGenerator - интерфейс генератора цитат
    internal interface IQuotaGenerator
    {
        // GetRandomQuota - метод, выдающий случайную осмысленную цитату в виде строки
        string GetRandomQuota();
    }
}
