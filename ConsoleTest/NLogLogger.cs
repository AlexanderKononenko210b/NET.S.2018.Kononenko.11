using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ConsoleTest
{
    public class NLogLogger : ILogger
    {
        private readonly Logger logger;

        public NLogLogger()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void WriteError(string stackTrace)
        {
            logger.Error(stackTrace);
        }

        public void WriteInfo(string info)
        {
            logger.Info(info);
        }
    }
}
