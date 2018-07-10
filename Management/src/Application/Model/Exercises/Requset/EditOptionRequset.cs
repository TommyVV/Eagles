namespace Eagles.Application.Model.Exercises.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditOptionRequset : RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public Model.Option Info { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
    }
}
