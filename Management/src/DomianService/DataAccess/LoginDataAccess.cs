using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Login.Model;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Oper;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{

    public class LoginDataAccess : ILoginDataAccess
    {
        private readonly IDbManager dbManager;

        public LoginDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public void UpdateOperErrorCount(TbOper mod)
        {

            var result =  dbManager.Excuted(@" UPDATE `eagles`.`tb_oper`
SET
`LoginErrorCount` = @LoginErrorCount,
`LockingTime` = @LockingTime
WHERE `OperId` = @OperId;


 ", new
            {
                mod.LoginErrorCount,
                LockingTime = Convert.ToInt64(mod.LockingTime),
                mod.OperId
            });
        }

        public void CreateverificationInfo(Verification verification)
        {
            throw new NotImplementedException();
        }

        public int GetverificationInfo(Verification verification)
        {
            throw new NotImplementedException();
        }

        public void InsertToken(TbUserToken tbUserToken)
        {
            dbManager.Excuted(@" INSERT INTO `eagles`.`tb_user_token`
(`OrgId`,
`BranchId`,
`UserId`,
`Token`,
`CreateTime`,
`ExpireTime`,
`TokenType`)
VALUES
(@OrgId,
@BranchId,
@UserId,
@Token,
@CreateTime,
@ExpireTime,
@TokenType);

 ", tbUserToken);
        }
    }
}
