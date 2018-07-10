using System.Collections.Generic;
using Eagles.Application.Model.Exercises.Model;

namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditSubjectRequset : RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public Subject Info { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<int> OptionId { get; set; }

    }
}
