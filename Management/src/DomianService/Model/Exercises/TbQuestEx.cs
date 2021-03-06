﻿using System;

namespace Eagles.DomainService.Model.Exercises
{
    /// <summary>
    /// 问卷扩展表 TB_QUESTION+TB_QUEST_ANWSER 
    /// </summary>
    public class TbQuestEx
    {
        /// <summary>
        /// 问题编号
        /// </summary>
        public int QuestionId { get; set; }

        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// 是否允许多选;0:否;1:是
        /// </summary>
        public int Multiple { get; set; }

        /// <summary>
        /// 多选数量 
        /// </summary>
        public int MultipleCount { get; set; }

        /// <summary>
        /// 选项编号
        /// </summary>
        public int AnswerId { get; set; }
        
        /// <summary>
        /// 选项
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 答案类型;0:默认 1:自定义
        /// </summary>
        public int AnswerType { get; set; }

        /// <summary>
        /// 答案是否是正确答案;0:否;1:是
        /// </summary>
        public int IsRight { get; set; }

        /// <summary>
        /// 选项图片 
        /// </summary>
        public string ImageUrl { get; set; }
    }
}