using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Organization
{
    public class GetOrganizationDetailsResponse:ResponseBase
    {
        public OrganizationInfoDetails info { get; set; }
    }
}
