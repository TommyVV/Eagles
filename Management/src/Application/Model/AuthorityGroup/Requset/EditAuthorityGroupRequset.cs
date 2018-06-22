namespace Eagles.Application.Model.AuthorityGroup.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditAuthorityGroupRequset:RequestBase
    {
  
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public Model.AuthorityGroup Info { get; set; }

 
    }
}
