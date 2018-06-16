using System;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.Scroll;
using Eagles.Application.Model.Curd.Scroll.GetScrollImg;
using Eagles.Application.Model.Curd.Scroll.GetScrollNew;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.Scroll
{
    public class ScrollHandler : IScrollHanler
    {
        private readonly IDbManager dbManager;

        public GetScrollImgResponse GetScoreExchangeLs(GetScrollImgRequest request)
        {
            var response = new GetScrollImgResponse();
            var token = request.Token;
            var result = dbManager.Query<DomainModel.ScrollImage.ScrollImage>("select OrgId,PageType,ImageUrl from eagles.TB_SCROLL_IMAGE ", null);
            if (result != null && result.Count > 0)
            {
                foreach (var image in result)
                    response.RollImgUrl.Add(image.ImageUrl);
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetScrollNewsResponse GetScoreRank(GetScrollNewsRequest request)
        {
            var response = new GetScrollNewsResponse();
            var token = request.Token;
            var result = dbManager.Query<DomainModel.News.SystemNews>("select OrgId,PageType,ImageUrl from eagles.TB_SYSTEM_NEWS ", null);
            if (result != null && result.Count > 0)
            {
                response.NewsId = result[0].NewsId;
                response.NewsName = result[0].NewsName;
                response.NewsContent = result[0].NewsContent;
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}