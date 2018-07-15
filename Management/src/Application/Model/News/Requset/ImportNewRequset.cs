using System.Collections.Generic;
using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.News.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public  class ImportNewRequset
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public List<ExternalNew> Info { get; set; }
    }
}
