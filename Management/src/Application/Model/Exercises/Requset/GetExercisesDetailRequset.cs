namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetExercisesDetailRequset:RequestBase
    {

        /// <summary>
        /// 试卷id
        /// </summary>
        public int ExercisesId { get; set; }
    }
}
