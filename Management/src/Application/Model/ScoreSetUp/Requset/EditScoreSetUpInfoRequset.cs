using Eagles.Application.Model.ScoreSetUp.Model;

namespace Eagles.Application.Model.ScoreSetUp.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditScoreSetUpInfoRequset:RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public ScoreSetUpInfo Info { get; set; }

    }
}
