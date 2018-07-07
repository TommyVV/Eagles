using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_TEST_QUESTION
    /// </summary>
    public class TbUserTestQuestion
    {
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public int IsRight { get; set; }
        public int OrgId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
    }
}