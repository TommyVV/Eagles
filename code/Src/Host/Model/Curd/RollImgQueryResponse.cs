using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 滚动图片查询
    /// </summary>
    class RollImgQueryResponse : ResponseBase
    {
        public string RollImgId { get; set; }
        public string RollImgName { get; set; }
        public string RollImgOrder { get; set; }
        public string RollImgUrl { get; set; }
    }
}
