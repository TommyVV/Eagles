using Eagles.Application.Model.Exercises.Model;

namespace Eagles.Application.Model.Exercises.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSubjectDetailResponse
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public SubjectDetails Info { get; set; }

    }
}
