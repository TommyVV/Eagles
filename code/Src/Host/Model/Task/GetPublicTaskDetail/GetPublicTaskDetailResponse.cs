using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Task.GetPublicTaskDetail
{
    /// <summary>
    /// 公开任务详情查询
    /// </summary>
    public class GetPublicTaskDetailResponse 
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
        
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务创建者
        /// </summary>
        public int TaskFounder { get; set; }
        
        /// <summary>
        /// 任务内容
        /// </summary>
        public string TaskContent { get; set; }

        /// <summary>
        /// 任务开始日期
        /// </summary>
        public DateTime TaskBeginDate { get; set; }

        /// <summary>
        /// 任务结束日期
        /// </summary>
        public DateTime TaskEndDate { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public int TaskStatus { get; set; }
                
        /// <summary>
        /// 任务图片Url
        /// </summary>
        public string TaskImageUrl { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 任务评分
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// 发起用户编号
        /// </summary>
        public int InitiateUserId { get; set; }

        /// <summary>
        /// 接受用户编号
        /// </summary>
        public int AcceptUserId { get; set; }

        /// <summary>
        /// 附件集合
        /// </summary>
        public List<Attachment> AcctachmentList { get; set; }
    }
}