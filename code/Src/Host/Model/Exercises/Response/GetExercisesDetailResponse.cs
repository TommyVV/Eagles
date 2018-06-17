namespace Eagles.Application.Model.Exercises.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetExercisesDetailResponse:ResponseBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public Model.ExercisesDetails Info { get; set; }
    }
}
