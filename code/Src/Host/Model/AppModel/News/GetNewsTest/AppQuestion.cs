using System.Collections.Generic;

namespace Eagles.Application.Model.AppModel.News.GetNewsTest
{
    /// <summary>
    /// 试卷
    /// </summary>
    public class AppQuestion
    {
        /// <summary>
        /// 问题id
        /// </summary>
        public string QuestionId { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 答案类型;0:默认:1:自定义
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 答案类型;是否允许多选;0:否;1:是
        /// </summary>
        public string Multiple { get; set; }

        /// <summary>
        /// 多选数量
        /// </summary>
        public string MultipleCount { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        public List<AppAnswer> SubMenus { get; set; }

    }
}