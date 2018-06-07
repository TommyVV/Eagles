using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.RollImage
{
   public  class GetRollImageDetailsRequset
    {
        /// <summary>
        /// 滚动图片组ID
        /// </summary>
        public int RollImageId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
