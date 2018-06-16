using Eagles.Base.Configuration;
using Eagles.DomainService.Model.Config;
using Eagles.Interface.Configuration;

namespace Ealges.DomainService.Configuration
{
    public class DataBaseConfiguration: IDataBaseConfiguration
    {
        private readonly IConfigurationManager configuration;

        public DataBaseConfiguration(IConfigurationManager configuration)
        {
            this.configuration = configuration;
        }

        public DataBaseConfig DBconfig => configuration.GetConfiguration<DataBaseConfig>();
    }
}
