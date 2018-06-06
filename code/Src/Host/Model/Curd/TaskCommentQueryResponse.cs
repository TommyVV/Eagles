using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 任务评论查询
    /// </summary>
    class TaskCommentQueryResponse : ResponseBase
    {
        public List<TaskComment> TaskCommentList { get; }
    }

    class TaskComment
    {
        public string CommentUserName { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
