using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Column
{
    public class GetColumnInfoResponse:ResponseBase
    {
        public ColumnInfoDetails info { get; set; }
    }
}
