﻿using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.Interface.DataAccess.NewsDa;

namespace Ealges.DomianService.DataAccess.NewsDA
{
    public class NewsDa: INewsDa
    {
        private readonly IDbManager dbManager;

        public NewsDa(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TB_NEWS> GetModuleNews(int moduleId,int appId,int count)
        {
            return dbManager.Query<TB_NEWS>(@"SELECT OrgId,NewsId,ShortDesc,Title,HtmlContent,Author,Source,Module,Status,BeginTime,EndTime,TestId,Attach1,Attach2,Attach3,Attach4,Attach5,
OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrl FROM eagles.tb_news where Module=@Module And OrgId=@OrgId limit @Count",new {Module = moduleId, OrgId = appId, Count = count});

        }

        public TB_NEWS GetNewsDetail(int newsId, int appId)
        {
            return dbManager.QuerySingle<TB_NEWS>(@"SELECT OrgId,NewsId,ShortDesc,Title,HtmlContent,Author,Source,Module,Status,BeginTime,EndTime,TestId,Attach1,Attach2,Attach3,Attach4,Attach5,
OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrlFROM eagles.tb_news where NewsId=@NewsId And OrgId=@OrgId", new { NewsId = newsId, OrgId = appId });
        }
    }
}