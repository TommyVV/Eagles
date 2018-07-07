using System;
using Eagles.Application.Model;
using Eagles.Application.Model.Register;
using Eagles.Base;
using Eagles.DomainService.Model.Sms;
using Eagles.Interface.Core.Register;
using Eagles.Interface.DataAccess.UserInfo;

namespace Eagles.DomainService.Core.Register
{
    public class RegisterHandler: IRegisterHandler
    {
        private readonly IUserInfoAccess userInfoAccess;

        public RegisterHandler(IUserInfoAccess userInfoAccess)
        {
            this.userInfoAccess = userInfoAccess;
        }

        public ValidateCodeResponse GenerateSmsCode(ValidateCodeRequest request)
        {
            //if (request.Phone<=0||request.Phone!=11)
            //{
            //    throw new TransactionException(MessageCode.ParametersIsEmpty, MessageKey.ParametersIsEmpty);
            //}

            if (request.AppId <= 0)
            {
                throw new TransactionException(MessageCode.ParametersIsEmpty, MessageKey.ParametersIsEmpty);
            }

            //校验手机号码是否存在
            var result = userInfoAccess.GetLogin(request.Phone.ToString());
            if (result != null)
            {
                throw new TransactionException(MessageCode.ExistsPhone, MessageKey.ExistsPhone);
            }
            //生成验证码
            var rd=new Random();
            var seq = rd.Next(10, 99);
            var code = rd.Next(1000, 9999);

            //todo 调用短信模块（暂无）

            var validateCode = new TbValidCode
            {
                CreateTime = DateTime.Now,
                ValidCode = code,
                Seq = seq,
                OrgId = request.AppId,
                Phone = request.Phone.ToString(),
                ExpireTime = DateTime.Now.AddMinutes(10)//10分钟后过期
            };
            //塞入数据库
            userInfoAccess.InsertSmsCode(validateCode);

            return new ValidateCodeResponse()
            {
                CodeSeq = seq
            };
        }
    }
}
