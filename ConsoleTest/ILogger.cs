using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public interface ILogger
    {
        void WriteInfo(string info);

        void WriteError(string stackTrace);
    }
}
