namespace Eagles.Base.Logger
{
    public interface ILogger:IInterfaceBase
    {
        void LoggerInfo(string log);

        void LoggerDebug(string log);

        void LoggerError(string log);
    }
}
