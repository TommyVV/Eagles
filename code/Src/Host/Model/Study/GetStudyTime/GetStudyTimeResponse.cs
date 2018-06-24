
namespace Eagles.Application.Model.Study.GetStudyTime
{
    /// <summary>
    /// 学习时间查询
    /// </summary>
    public class GetStudyTimeResponse :ResponseBase
    {
        /// <summary>
        /// 学习时间
        /// </summary>
        public int StudyTime { get; set; }
    }
}