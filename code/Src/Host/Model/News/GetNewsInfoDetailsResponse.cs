using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.News
{
   public  class GetNewsInfoDetailsResponse:ResponseBase
    {
        public NewsInfoDetails info { get; set; }
    }
}
