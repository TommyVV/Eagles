using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.ScoreRank.Requset;
using Eagles.Application.Model.ScoreRank.Response;
using Eagles.Application.Model.ScoreSetUp.Requset;
using Eagles.Application.Model.ScoreSetUp.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IScoreHandler: IInterfaceBase
    {
        /// <summary>
        /// 编辑 积分设置
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool EditScoreSetUp(EditScoreSetUpRequset requset);

        /// <summary>
        /// 删除 积分设置
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveScoreSetUp(RemoveScoreSetUpRequset requset);

        /// <summary>
        /// 积分设置 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetScoreSetUpDetailResponse GetScoreSetUpDetail(GetScoreSetUpDetailRequset requset);

        /// <summary>
        /// 积分设置 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetScoreSetUpResponse GetScoreSetUp(GetScoreSetUpRequset requset);


        /// <summary>
        /// 积分排行 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetScoreRankDetailResponse GetScoreRankDetail(GetScoreRankDetailRequset requset);

        /// <summary>
        /// 积分排行 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetScoreRankResponse GetScoreRank(GetScoreRankRequset requset);


        
    }
}
