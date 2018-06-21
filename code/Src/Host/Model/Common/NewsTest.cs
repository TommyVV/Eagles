using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 试卷
    /// </summary>
    public class NewsTest
    {
        public string TestId { get; set; }

        public string QuestionId { get; set; }

        public string Question { get; set; }

        public string Multiple { get; set; }

        public string MultipleCount { get; set; }

        public string AnswerId { get; set; }

        public string Answer { get; set; }

        public string AnswerType { get; set; }

        public string IsRight { get; set; }

        public string ImageUrl { get; set; }
    }
}