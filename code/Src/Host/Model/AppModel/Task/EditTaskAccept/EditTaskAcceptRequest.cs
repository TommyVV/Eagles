using Eagles.Application.Model.AppModel.Enum;

namespace Eagles.Application.Model.AppModel.Task.EditTaskAccept
{
    /// <summary>
    /// 任务接受(审核)接口
    /// </summary>
    public class EditTaskAcceptRequest : RequestBase
    {
        /// <summary>
        /// 01-上级审核任务 02-下级接受任务 03-下级申请完成
        /// </summary>
        public TaskTypeEnum Type { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}