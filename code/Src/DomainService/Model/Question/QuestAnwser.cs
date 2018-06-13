using System;

namespace Eagles.DomainService.Model.Question
{
    /// <summary>
    /// 
    /// </summary>
    public class QuestAnwser
    {
        public string Answer { get; set; }
        public int AnswerId { get; set; }
        public int AnswerType { get; set; }
        public string ImageUrl { get; set; }
        public int IsRight { get; set; }
        public int OrgId { get; set; }
        public int QuestionId { get; set; }
    }
}