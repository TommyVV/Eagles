using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.Util
{
    public interface IUtil : IInterfaceBase
    {
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

        /// <summary>
        /// 用户积分流水
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="score"></param>
        /// <param name="rewardsType"></param>
        /// <param name="comment"></param>
        /// <param name="oriScore"></param>
        /// <returns></returns>
        int CreateScoreLs(int userId, int score, string rewardsType, string comment, int oriScore);

        /// <summary>
        /// 更新用户积分
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        int EditUserScore(int userId, int score);
    }
}