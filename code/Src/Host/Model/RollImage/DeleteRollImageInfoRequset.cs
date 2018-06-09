using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.RollImage
{
    public class DeleteRollImageInfoRequset
    {
        /// <summary>
        /// 级菜单名称
        /// </summary>
        public int RollImageId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
