using System.Collections.Generic;

namespace Eagles.Application.Model.Exercises.Response
{
     /// <summary>
     /// 
     /// </summary>
     public class GetExercisesResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Exercises> List { get; set; }
    }
}
