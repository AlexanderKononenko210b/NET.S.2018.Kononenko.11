using NLog;
using Books.Interfaces;

namespace Books.Loggers
{
    /// <summary>
    /// Class for decorate NLog Framework
    /// </summary>
    public class NLogLogger : ILog
    {
        private readonly Logger logger;

        public NLogLogger()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        public void WriteInfo(string info)
        {
            logger.Info(info);
        }

        public void WriteError(string stackTrace)
        {
            logger.Error(stackTrace);
        }

        public void WriteDebug(string stackTrace)
        {
            logger.Debug(stackTrace);
        }

        public void WriteWarn(string stackTrace)
        {
            logger.Warn(stackTrace);
        }

        public void WriteFatal(string stackTrace)
        {
            logger.Fatal(stackTrace);
        }
    }
}
