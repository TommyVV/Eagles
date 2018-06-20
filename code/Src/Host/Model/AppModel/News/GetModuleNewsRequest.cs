namespace Eagles.Application.Model.AppModel.News
{
    /// <summary>
    /// 模块查询
    /// </summary>
    public class GetModuleNewsRequest : RequestBase
    {
        public int ModuleId { get; set; }

        public int NewsCount { get; set; }
    }
}