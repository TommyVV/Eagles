using System.Web.Http;
using Eagles.Application.Model.UserMessage;
using Eagles.Interface.Core.UserMessage;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// UserMessageControllerv
    /// </summary>
    public class UserMessageController: ApiController
    {
        private readonly IUserMessageHandler userMessage;

        public UserMessageController(IUserMessageHandler userMessage)
        {
            this.userMessage = userMessage;
        }

        /// <summary>
        /// 获取用户未读消息数量
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public GetUserUnreadMessage GetUserUnreadMessage(string token)
        {
            return userMessage.GetUserUnreadMessage(token);
        }
    }
}