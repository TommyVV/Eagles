using System.Collections.Generic;

namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditExercisesRequset:RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public Model.Exercises Info { get; set; }

        /// <summary>
        /// 问题id
        /// </summary>
        public List<int> Subject { get; set; }
    }
}
