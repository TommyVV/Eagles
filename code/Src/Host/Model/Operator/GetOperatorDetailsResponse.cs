using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Operator
{
    public class GetOperatorDetailsResponse:ResponseBase
    {
        public OperatorInfoDetails Info { get; set; }
    }
}
