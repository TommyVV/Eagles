namespace Eagles.Application.Model.Module
{
    public class GetAppModuleRequest
    {
        public ModulType ModuleType { get; set; }

        public int AppId { get; set; }
    }

    public enum ModulType
    {
        /// <summary>
        /// 首页
        /// </summary>
        IndexPage=0,

        /// <summary>
        /// 党建门户
        /// </summary>
        Protal=1,

        /// <summary>
        /// 党务工作
        /// </summary>
        Work=2,

        /// <summary>
        /// 党建学习
        /// </summary>
        Study=3,
    }
}
