using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.ScrollImage;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class ScrollImageDataAccess: IScrollImageDataAccess
    {

        private readonly IDbManager dbManager;

        public ScrollImageDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public int CreateRollImages(TbScrollImage mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_scroll_image`
SET
`OrgId` = @OrgId,
`PageType` = @PageType,
`ImageUrl` = @ImageUrl
WHERE `Id` = @Id

", mod);
        }

        public int EditRollImages(TbScrollImage mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_news`
(`OrgId`,
`NewsId`,
`ShortDesc`,
`Title`,
`HtmlContent`,
`Author`,
`Source`,
`Module`,
`Status`,
`BeginTime`,
`EndTime`,
`TestId`,
`Attach1`,
`Attach2`,
`Attach3`,
`Attach4`,
`Attach5`,
`OperId`,
`CreateTime`,
`IsImage`,
`IsVideo`,
`IsAttach`,
`IsClass`,
`IsLearning`,
`IsText`,
`ViewCount`,
`ReviewId`,
`CanStudy`,
`ImageUrl`)
VALUES
(@OrgId,
@NewsId,
@ShortDesc,
@Title,
@HtmlContent,
@Author,
@Source,
@Module,
@Status,
@BeginTime,
@EndTime,
@TestId,
@Attach1,
@Attach2,
@Attach3,
@Attach4,
@Attach5,
@OperId,
@CreateTime,
@IsImage,
@IsVideo,
@IsAttach,
@IsClass,
@IsLearning,
@IsText,
@ViewCount,
@ReviewId,
@CanStudy,
@ImageUrl);

", mod);
            throw new NotImplementedException();
        }

        public int RemoveRollImages(RemoveRollImageRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_scroll_image`
WHERE
                `Id` = @Id;
", new { requset.Id });
        }

        public TbScrollImage GetRollImagesDetail(GetRollImageDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_scroll_image`.`OrgId`,
    `tb_scroll_image`.`Id`,
    `tb_scroll_image`.`PageType`,
    `tb_scroll_image`.`ImageUrl`
FROM `eagles`.`tb_scroll_image`
  where Id=@Id;
 ");
            dynamicParams.Add("Id", requset.Id);

            return dbManager.QuerySingle<TbScrollImage>(sql.ToString(), dynamicParams);
        }

        public List<TbScrollImage> GetRollImagesList(GetRollImageRequest requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(requset.PageId))
            {
                parameter.Append(" and PageType = @PageType ");
                dynamicParams.Add("PageType", requset.PageId);
            }

            if (requset.OrgId > 0)
            {
                parameter.Append(" and OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }
 
            sql.AppendFormat(@" SELECT `tb_scroll_image`.`OrgId`,
    `tb_scroll_image`.`Id`,
    `tb_scroll_image`.`PageType`,
    `tb_scroll_image`.`ImageUrl`
FROM `eagles`.`tb_scroll_image`
FROM `eagles`.`tb_user_info`  where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbScrollImage>(sql.ToString(), dynamicParams);
        }
    }
}
