using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.DomainService.Model.Exercises
{
    public class TB_QUEST_ANWSER
    {
        /// <summary>
        /// 选项名称
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
    }
}
