using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.SystemMessage
{
    public class AlterSystemMessageInfoDetailsRequset
    {
        public SystemMessageInfoDetails info { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
