using Eagles.Interface.Core.Scroll;
using Eagles.Interface.DataAccess.Util;
using Eagles.Application.Model.Scroll.GetScrollImg;
using Eagles.Application.Model.Scroll.GetScrollNew;
using Eagles.Interface.Core.DataBase.ScrollAccess;

namespace Eagles.DomainService.Core.Scroll
{
    public class ScrollHandler : IScrollHandler
    {
        private readonly IScrollAccess iScrollAccess;
        private readonly IUtil util;

        public ScrollHandler(IScrollAccess iScrollAccess, IUtil util)
        {
            this.iScrollAccess = iScrollAccess;
            this.util = util;
        }

        public GetScrollImgResponse GetScrollImg(GetScrollImgRequest request)
        {
            var response = new GetScrollImgResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iScrollAccess.GetScrollImg();
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

        public GetScrollNewsResponse GetScrollNews(GetScrollNewsRequest request)
        {
            var response = new GetScrollNewsResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iScrollAccess.GetScrollNews();
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