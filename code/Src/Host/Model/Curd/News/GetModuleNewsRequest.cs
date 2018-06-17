namespace Eagles.Application.Model.Curd.News
{
    public class GetModuleNewsRequest:RequestBase
    {
        public int ModuleId { get; set; }

        public int NewsCount { get; set; }
    }
}
