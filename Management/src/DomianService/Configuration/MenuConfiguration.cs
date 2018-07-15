using Eagles.Base.Configuration;
using Eagles.DomainService.Model.Config;
using Eagles.Interface.Configuration;

namespace Ealges.DomainService.Configuration
{
    public class MenuConfiguration: IMenuConfiguration
    {
        private readonly IConfigurationManager configuration;

        public MenuConfiguration(IConfigurationManager configuration)
        {
            this.configuration = configuration;
        }

        public FunctionMenu FunctionMenu => configuration.GetConfiguration<FunctionMenu>();
    }
}
