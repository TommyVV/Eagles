using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Base.Configuration;
using Eagles.DomainService.Model.Config;
using Eagles.Interface.Configuration;

namespace Ealges.DomainService.Configuration
{
    public class EaglesConfig: IEaglesConfig
    {
        private readonly IConfigurationManager configuration;

        public EaglesConfig(IConfigurationManager configuration)
        {
            this.configuration = configuration;
        }

        public EaglesConfiguration EaglesConfiguration => configuration.GetConfiguration<EaglesConfiguration>();
    }
}
