using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Base.Cache;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.App;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class MenusDataAccess : IMenusDataAccess
    {
        private readonly IDbManager dbManager;

        private readonly ICacheHelper cacheHelper;

        public MenusDataAccess(IDbManager dbManager, ICacheHelper cacheHelper)
        {
            this.dbManager = dbManager;
            this.cacheHelper = cacheHelper;
        }
        public int EditNews(TbAppMenu mod)
        {
            return dbManager.Excuted(@" UPDATE `eagles`.`tb_app_menu`
SET
`OrgId` = @OrgId,
`MenuId` = @MenuId,
`MenuName` = @MenuName,
`Level` = @Level,
`ParentMenuId` = @ParentMenuId,
`TargetUrl` = @TargetUrl
WHERE  
`MenuId` = @MenuId

 ", mod);
        }

        public int CreateNews(TbAppMenu mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_app_menu`
(`OrgId`,
`MenuId`,
`MenuName`,
`Level`,
`ParentMenuId`,
`TargetUrl`)
VALUES
(@OrgId,
@MenuId,
@MenuName,
@Level,
@ParentMenuId,
@TargetUrl);



", mod);
        }

        public int RemoveMenus(RemoveMenusRequset requset)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_app_menu`
WHERE
              `MenuId` = @MenuId
", new { requset.MenuId });
        }

        public TbAppMenu GetMenusDetail(GetMenusDetailRequest requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_app_menu`.`OrgId`,
    `tb_app_menu`.`MenuId`,
    `tb_app_menu`.`MenuName`,
    `tb_app_menu`.`Level`,
    `tb_app_menu`.`ParentMenuId`,
    `tb_app_menu`.`TargetUrl`
FROM `eagles`.`tb_app_menu`
  where MenuId=@MenuId;
 ");
            dynamicParams.Add("MenuId", requset.MenuId);

            return dbManager.QuerySingle<TbAppMenu>(sql.ToString(), dynamicParams);
        }

        public List<TbAppMenu> GetMenusList(GetMenusRequset requset, out int totalCount)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            var tokenInfo = cacheHelper.GetData<TbUserToken>(requset.Token);
            if (tokenInfo.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", tokenInfo.OrgId);
            }

            //if (requset.BranchId > 0)
            //{
            //    parameter.Append(" and BranchId = @BranchId ");
            //    dynamicParams.Add("BranchId", requset.BranchId);
            //}

            //if (requset.MenuLevel > 0)
            //{
            //    parameter.Append(" and Level = @MenuLevel ");
            //    dynamicParams.Add("MenuLevel", (int)requset.MenuLevel);
            //}


            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_app_menu`  where 1=1  {0} 
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();


            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);


            sql.AppendFormat(@" SELECT `tb_app_menu`.`OrgId`,
    `tb_app_menu`.`MenuId`,
    `tb_app_menu`.`MenuName`,
    `tb_app_menu`.`Level`,
    `tb_app_menu`.`ParentMenuId`,
    `tb_app_menu`.`TargetUrl`
FROM `eagles`.`tb_app_menu` 
  where  1=1  {0}   order by MenuId desc limit  @pageStart ,@pageSize
 ", parameter);

            return dbManager.Query<TbAppMenu>(sql.ToString(), dynamicParams);
        }

        public List<TbAppMenu> GetSubordinate(GetSubordinateRequset requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.MenuId > 0)
            {
                parameter.Append(" and  ParentMenuId = @ParentMenuId ");
                dynamicParams.Add("ParentMenuId", requset.MenuId);
            }


            sql.AppendFormat(@" SELECT `tb_app_menu`.`OrgId`,
    `tb_app_menu`.`MenuId`,
    `tb_app_menu`.`MenuName`,
    `tb_app_menu`.`Level`,
    `tb_app_menu`.`ParentMenuId`,
    `tb_app_menu`.`TargetUrl`
FROM `eagles`.`tb_app_menu` 
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbAppMenu>(sql.ToString(), dynamicParams);

        }
    }
}
