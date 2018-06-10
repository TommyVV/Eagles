using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.SystemMessage
{
    public class DeleteSystemMessageInfoDetailsRequset
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SystemMessageId { get; set; }
    }
}
