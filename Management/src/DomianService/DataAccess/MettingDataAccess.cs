using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.Meeting.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class MettingDataAccess: IMettingDataAccess
    {
        private readonly IDbManager dbManager;

        public List<TbUserInfo> GetUserInfoByPhone(List<string> list)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_user_info`.`OrgId`,
    `tb_user_info`.`BranchId`,
    `tb_user_info`.`UserId`,
    `tb_user_info`.`Password`,
    `tb_user_info`.`Name`,
    `tb_user_info`.`Sex`,
    `tb_user_info`.`Ethnic`,
    `tb_user_info`.`Birthday`,
    `tb_user_info`.`Origin`,
    `tb_user_info`.`OriginAddress`,
    `tb_user_info`.`Phone`,
    `tb_user_info`.`IdNumber`,
    `tb_user_info`.`Education`,
    `tb_user_info`.`School`,
    `tb_user_info`.`Provice`,
    `tb_user_info`.`City`,
    `tb_user_info`.`District`,
    `tb_user_info`.`Address`,
    `tb_user_info`.`Company`,
    `tb_user_info`.`Dept`,
    `tb_user_info`.`Title`,
    `tb_user_info`.`PreMemberTime`,
    `tb_user_info`.`MemberTime`,
    `tb_user_info`.`MemberType`,
    `tb_user_info`.`Status`,
    `tb_user_info`.`MemberStatus`,
    `tb_user_info`.`PhotoUrl`,
    `tb_user_info`.`NickPhotoUrl`,
    `tb_user_info`.`CreateTime`,
    `tb_user_info`.`EditTime`,
    `tb_user_info`.`OperId`,
    `tb_user_info`.`IsCustomer`,
    `tb_user_info`.`Score`,
    `tb_user_info`.`IsLeader`,
    `tb_user_info`.`LoginErrorCount`,
    `tb_user_info`.`LockingTime`
FROM `eagles`.`tb_user_info`
  where Phone  in  @Phone;  
 ");
            dynamicParams.Add("Phone", list.ToArray());

            return dbManager.Query<TbUserInfo>(sql.ToString(), dynamicParams);

        }

        public int CreateUserInfo(List<TbMeetingUser> userinfo)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_meeting_user`
(`NewsId`,
`OrgId`,
`BranchId`,
`UserId`)
VALUES
(@NewsId,
@OrgId,
@BranchId,
@UserId);
", userinfo);
        
        
        }

        public List<TbMeetingUser> GetMettingUsers(GetMeetingRequest requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.MeetingId > 0)
            {
                parameter.Append(" and  NewsId = @NewsId ");
                dynamicParams.Add("NewsId", requset.MeetingId);
            }


            //if (!string.IsNullOrWhiteSpace(requset.MeetingNmae))
            //{
            //    parameter.Append(" and ProdName = @ProdName ");
            //    dynamicParams.Add("ProdName", requset.MeetingNmae);
            //}

            sql.AppendFormat(@" SELECT `tb_meeting_user`.`NewsId`,
    `tb_meeting_user`.`OrgId`,
    `tb_meeting_user`.`BranchId`,
    `tb_meeting_user`.`UserId`
FROM `eagles`.`tb_meeting_user`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbMeetingUser>(sql.ToString(), dynamicParams);
            
        }
    }
}
