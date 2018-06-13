using System;

namespace Eagles.DomainService.Model.Question
{
    public class Question
    {
        public int Answer { get; set; }
        public int Multiple { get; set; }
        public int MultipleCount { get; set; }
        public int OrgId { get; set; }
        public string Questions { get; set; }
        public int QuestionId { get; set; }
    }
}