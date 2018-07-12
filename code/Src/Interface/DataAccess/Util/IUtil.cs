using System.Collections.Generic;
using Eagles.Application.Model.Common;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.RewardScore;

namespace Eagles.Interface.DataAccess.Util
{
    public interface IUtil : IInterfaceBase
    {
        /// <summary>
        /// 用户通知
        /// </summary>
        /// <param name="userNotice"></param>
        /// <returns></returns>
        int CreateUserNotice(TbUserNotice userNotice);

        /// <summary>
        /// 用户积分流水
        /// </summary>
        /// <param name="userScoreTrace"></param>
        /// <returns></returns>
        int CreateScoreLs(TbUserScoreTrace userScoreTrace);

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        int EditUserScore(int userId, int score);

        /// <summary>
        /// 批量更新用户积分
        /// </summary>
        /// <param name="userList"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        bool BatchEditUserScore(List<JoinPeople> userList, int score);

        /// <summary>
        /// 校验AppId是否存在
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        bool CheckAppId(int appId);

        /// <summary>
        /// 查询积分配置
        /// </summary>
        /// <param name="rewardType"></param>
        /// <returns></returns>
        TbRewardScore RewardScore(string rewardType);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        TbUserToken GetUserId(string token, int tokenType);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        TbUserInfo GetUserInfo(int userId);

    }
}