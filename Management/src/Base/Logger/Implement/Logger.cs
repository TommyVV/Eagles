using NLog;

namespace Eagles.Base.Logger.Implement
{
    public class Logger : ILogger
    {
        private static readonly NLog.Logger log = LogManager.GetCurrentClassLogger();

        public void LoggerInfo(string val)
        {
            log.Info(val);
        }

        public void LoggerDebug(string val)
        {
            log.Debug(val);
        }

        public void LoggerError(string val)
        {
            log.Error(val);
        }
    }
}
