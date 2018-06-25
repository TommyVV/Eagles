using System;
using Eagles.Application.Model.UserMessage;
using Eagles.Interface.Core.UserMessage;
using Eagles.Interface.DataAccess.UserMessage;
using Eagles.Interface.DataAccess.Util;

namespace Eagles.DomainService.Core.UserMessage
{
    public class UserMessageHandler: IUserMessageHandler
    {
        private readonly IUserMessageDataAccess userMessage;

        private readonly IUtil util;

        public UserMessageHandler(IUserMessageDataAccess userMessage, IUtil util)
        {
            this.userMessage = userMessage;
            this.util = util;
        }

        public GetUserUnreadMessage GetUserUnreadMessage(string token)
        {
            var userInfo = util.GetUserId(token, 0);
            if (userInfo == null)
            {
                return new GetUserUnreadMessage();
            }
            
            var count= userMessage.GetUserUnreadMessageCount(userInfo.UserId);
            return new GetUserUnreadMessage()
            {
                IsSuccess = true,
                Code = "00",
                Message = "成功",
                DateTime = DateTime.Now,
                UnreadMessageCount = count
            };
        }
    }
}
