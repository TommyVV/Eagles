namespace Eagles.Base.Configuration
{
    public interface IConfigurationManager:IInterfaceBase
    {
        T GetConfiguration<T>();
    }
}
