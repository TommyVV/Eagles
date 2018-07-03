using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Login.Model;
using Eagles.Application.Model.Login.Requset;
using Eagles.Application.Model.Login.Response;
using Eagles.Base;
using Eagles.Base.ValidateVode;
using Eagles.DomainService.Model.Oper;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Configuration;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class LoginHandler : ILoginHandler
    {
        private readonly ILoginAccess dataAccess;

        private readonly IValidateCode validateCode;

        private readonly IEaglesConfig configuration;

        private readonly IOperDataAccess userAccess;

        public LoginHandler(ILoginAccess dataAccess, IValidateCode validateCode, IEaglesConfig configuration, IOperDataAccess userAccess)
        {
            this.dataAccess = dataAccess;
            this.validateCode = validateCode;
            this.configuration = configuration;
            this.userAccess = userAccess;
        }

        /// <summary>
        ///根据账号查询 是否存储表中.
        ///让后密码进行比较判断 不相等 继承账号登陆失败次数 找不到抛出用户无用户信息
        ///oper 不为空 登陆失败记录登陆次数
        ///失败3次 需要输入验证码 重置图像验证码表中 图像验证码信息
        ///失败6次 进行锁定用户 记录锁定截至时间
        /// 
        ///登陆验证 是否锁定  是否密码相等  
        /// 
        ///登陆成功 重置登陆失败次数 并且返回Token
        ///判断登陆次数是否小于配置文件次数 进行登陆失败次数累加
        ///等于失败次数则进行 锁定账号 锁定时间为 当前时间 + 配置文件中时间 时间搓形式进行记录
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public LoginResponse Login(LoginRequset requset)
        {

            var respone = new LoginResponse
            {
                IsVerificationCode = false,
                Token = "",
            };

            var oper = userAccess.GetOperInfo(requset);

            try
            {
                if (oper == null)
                {
                    throw new TransactionException("M05", "用户名或密码错误");
                }

                var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                var time = startTime.AddSeconds(oper.LockingTime);

                if (DateTime.Compare(DateTime.Now, time) < 0) throw new TransactionException("M07", "账号已被锁定,请" + time.ToString("u") + "后在进行登陆");



                if (!oper.Password.Equals(requset.Password) || string.IsNullOrWhiteSpace(oper.Password))
                {
                    throw new TransactionException("M05", "用户名或密码错误");
                }

                if (oper.LoginErrorCount >= configuration.EaglesConfiguration.CheckVerificationCode)
                {

                    //得到验证码值
                    var result = dataAccess.GetverificationInfo(new Verification
                    {
                        OrgId = oper.OrgId,
                        Account = oper.OperName,
                        Code = GetRandomArray(6, 1, 9).ToString(),

                    });

                    if (result <= 0) throw new TransactionException("M06", "验证码错误");
                }


                respone.Token = Guid.NewGuid().ToString();

                dataAccess.InsertToken(new TbUserToken());

            }
            catch (TransactionException ex)
            {
                //记录失败次数
                if (oper != null)
                {
                    var info = new TbOper
                    {
                        OperName = requset.Account,
                        OrgId = requset.OrgId,
                    };

                    //验证码校验
                    if (oper.LoginErrorCount >= configuration.EaglesConfiguration.CheckVerificationCode)
                    {
                        respone.IsVerificationCode = true;

                        //每次登陆未成功 跟新验证码
                        dataAccess.CreateverificationInfo(new Verification
                        {
                            OrgId = oper.OrgId,
                            Account = oper.OperName,
                            Code = GetRandomArray(6, 1, 9).ToString(),

                        });


                        oper.LoginErrorCount = oper.LoginErrorCount + 1;
                    }
                    //锁定账号
                    else if (oper.LoginErrorCount >= configuration.EaglesConfiguration.LoginErrorCount)
                    {
                        var now = DateTime.Now;
                        now = now.AddHours(configuration.EaglesConfiguration.LockingTime);

                        DateTime startTime =
                            TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                        var intResult = (now - startTime).TotalSeconds;

                        //触发锁定后 重置失败次数
                        info.LockingTime = intResult;
                        oper.LoginErrorCount = 0;
                    }

                    dataAccess.UpdateOperErrorCount(info);

                }

                throw new TransactionException(ex.ErrorCode, ex.ErrorMessage);


            }

            return respone;

        }

        /// <summary>
        /// 获取图像验证码
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        public string VerificationCode(Verification requset)
        {
            var code = dataAccess.GetverificationInfo(requset);

            return validateCode.GenerateValidCodeToBase64(code);
        }


        // Number随机数个数
        // minNum随机数下限
        // maxNum随机数上限
        public int[] GetRandomArray(int number, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[number];
            var r = new Random();
            for (j = 0; j < number; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }
    }
}

