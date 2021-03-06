﻿using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.Interface.DataAccess.NewsDa;

namespace Ealges.DomianService.DataAccess.NewsDA
{
    public class NewsDa : INewsDa
    {
        private readonly IDbManager dbManager;

        public NewsDa(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbNews> GetModuleNews(int moduleId, int appId, int pageIndex = 1, int pageSize = 10)
        {
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            return dbManager.Query<TbNews>(@"select tb_news.OrgId,NewsId,ShortDesc,Title,HtmlContent,Author,Source,Module,Status,BeginTime,EndTime,TestId,
Attach1,Attach2,Attach3,Attach4,Attach5,OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrl,IsExternal,ExternalUrl
from eagles.tb_news where Module=@Module and OrgId=@OrgId order by CreateTime desc limit @PageIndex, @PageSize ", new {Module = moduleId, OrgId = appId, PageIndex = pageIndexParameter, PageSize = pageSize });
        }

        public TbNews GetNewsDetail(int newsId, int appId)
        {
            return dbManager.QuerySingle<TbNews>(@"select tb_news.OrgId,NewsId,ShortDesc,Title,HtmlContent,Author,Source,Module,Status,BeginTime,EndTime,
TestId,Attach1,Attach2,Attach3,Attach4,AttachName1,AttachName2,AttachName3,AttachName4,Attach5,OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrl,
IsExternal,ExternalUrl from eagles.tb_news where NewsId=@NewsId and OrgId=@OrgId and Status = 0 ", new { NewsId = newsId, OrgId = appId });
        }

        public int AddNewsViewCount(int newsId)
        {
            return dbManager.Excuted("update eagles.tb_news set ViewCount = ifnull(ViewCount,0) + 1 where NewsId=@NewsId ", new {NewsId = newsId});
        }
    }
}