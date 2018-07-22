using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.ActivityTask.Model
{
    public class GetActivityDetail: ActivityTaskModel
    {
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }
        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        ///// <summary>
        ///// 考试类型
        ///// </summary>
        //public ExamType ExamType { get; set; }

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

        ///// <summary>
        ///// 是否允许评论 允许投票是习题 选择才能生效
        ///// </summary>
        //public int IsVote { get; set; }

        /// <summary>
        /// 习题id
        /// </summary>
        public int ExampleId { get; set; }

        ///// <summary>
        ///// 附件 json格式
        ///// </summary>
        //public string Enclosure { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }
    }
}
