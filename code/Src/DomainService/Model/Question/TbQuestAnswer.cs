﻿
namespace Eagles.DomainService.Model.Question
{
    /// <summary>
    /// TB_QUEST_ANSWER
    /// </summary>
    public class TbQuestAnswer
    {
        /// <summary>
        /// 答案选项
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 选项编号
        /// </summary>
        public int AnswerId { get; set; }
        /// <summary>
        /// 答案类型 0默认 1自定义
        /// </summary>
        public int AnswerType { get; set; }
        /// <summary>
        /// 选项图片名称
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 答案是否是正确答案 0 是 1否
        /// </summary>
        public int IsRight { get; set; }
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 问题编号
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 用户当前选择数量
        /// </summary>
        public int UserCount { get; set; }
    }
}