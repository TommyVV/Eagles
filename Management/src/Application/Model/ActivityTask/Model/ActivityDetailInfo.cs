using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ActivityTask.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityDetailInfo : ActivityTaskModel
    {

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 考试类型
        /// </summary>
        public ExamType ExamType { get; set; }

        /// <summary>
        /// 最大参与人数
        /// </summary>
        public int MaxPartakePeople { get; set; }

        /// <summary>
        /// 每人参与人数
        /// </summary>
        public int EverybodyPeople { get; set; }

        /// <summary>
        /// 是否允许评论
        /// </summary>
        public int IsComment { get; set; }

        /// <summary>
        /// 是否允许评论 允许投票是习题 选择才能生效
        /// </summary>
        public bool IsVote { get; set; }

        /// <summary>
        /// 习题id
        /// </summary>
        public int ExampleId { get; set; }

        /// <summary>
        /// 附件 json格式
        /// </summary>
        public string Enclosure { get; set; }

        /// <summary>
        /// 附件1
        /// </summary>
        public string Attach1 { get; set; }
        /// <summary>
        /// 附件2
        /// </summary>
        public string Attach2 { get; set; }
        /// <summary>
        /// 附件3
        /// </summary>
        public string Attach3 { get; set; }
        /// <summary>
        /// 附件4
        /// </summary>
        public string Attach4 { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }
    }
}
