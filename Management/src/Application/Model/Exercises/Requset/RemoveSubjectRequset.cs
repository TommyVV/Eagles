namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveSubjectRequset:RequestBase
    {

        /// <summary>
        /// 
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 试卷id
        /// </summary>
        public string ExercisesId { get; set; }
    }
}
