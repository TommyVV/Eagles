using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Task.GetTaskDetail
{
    /// <summary>
    /// 任务详情查询
    /// </summary>
    public class GetTaskDetailResponse 
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
        public string TaskBeginDate { get; set; }

        /// <summary>
        /// 任务结束日期
        /// </summary>
        public string TaskEndDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

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
        /// 发起用户名称
        /// </summary>
        public string InitiateUserName { get; set; }

        /// <summary>
        /// 接受用户编号
        /// </summary>
        public int AcceptUserId { get; set; }

        /// <summary>
        /// 接受用户名称
        /// </summary>
        public string AcceptUserName { get; set; }

        /// <summary>
        /// 创建类型 0上级发布 1下级向上级申请  
        /// </summary>
        public int CreateType { get; set; }

        /// <summary>
        /// 附件集合
        /// </summary>
        public List<Attachment> AcctachmentList { get; set; }
    }
}