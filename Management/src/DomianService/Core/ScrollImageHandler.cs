using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model.RollImage.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.ScrollImage;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class ScrollImageHandler : IScrollImageHandler
    {
        private readonly IScrollImageDataAccess dataAccess;

        private readonly ICacheHelper cacheHelper;

        public ScrollImageHandler(IScrollImageDataAccess dataAccess,ICacheHelper cacheHelper)
        {
            this.dataAccess = dataAccess;
            this.cacheHelper = cacheHelper;
        }

        public bool EditRollImages(EditRollImageRequest requset)
        {
           

            TbScrollImage mod;
            var tokenInfo = cacheHelper.GetData<TbUserToken>(requset.Token);

            if (requset.Info.Id > 0)
            {
                mod = new TbScrollImage
                {
                    Id = requset.Info.Id,
                    ImageUrl = requset.Info.Img,
                    OrgId = tokenInfo.OrgId,
                    PageType = requset.Info.PageId,
                    TargetUrl =requset.Info.TargetUrl
                };

                return dataAccess.EditRollImages(mod)>0;

            }
            mod = new TbScrollImage
            {
                //   Id = requset.Info.Id,
                   
                ImageUrl = requset.Info.Img,
                OrgId = tokenInfo.OrgId,
                PageType = requset.Info.PageId,
                TargetUrl = requset.Info.TargetUrl
            };

            return dataAccess.CreateRollImages(mod)>0;


        }

        public bool RemoveRollImages(RemoveRollImageRequset requset)
        {
          
           return dataAccess.RemoveRollImages(requset)>0;
                 
        }

        public GetRollImageDetailsResponse GetRollImagesDetail(GetRollImageDetailRequset requset)
        {
            var response = new GetRollImageDetailsResponse
            {
              
            };
            var detail = dataAccess.GetRollImagesDetail(requset.Id);

            if (detail == null) throw new TransactionException("M01","无业务数据");

            response.Info = new RollImageInfo
            {
                Img = detail.ImageUrl,
                OrgId = detail.OrgId,
                Id = detail.Id,
                PageId = detail.PageType,
                TargetUrl=detail.TargetUrl,
                OrgName=detail.OrgName
            };
            return response;
        }

        public GetRollImageResponse GetRollImages(GetRollImageRequest requset)
        {
            var tokenInfo = cacheHelper.GetData<TbUserToken>(requset.Token);
            var response = new GetRollImageResponse
            {
                TotalCount = 0,
               
            };
            var list = dataAccess.GetRollImagesList(requset, out var totalCount, tokenInfo.OrgId) ?? new List<TbScrollImage>();

            if (list.Count == 0) throw new TransactionException("M01","无业务数据");
            response.TotalCount = totalCount;
            response.List = list.Select(x => new RollImageInfo
            {
                Img = x.ImageUrl,
                OrgId = x.OrgId,
                OrgName = x.OrgName,
                Id = x.Id,
                PageId = x.PageType,
                TargetUrl=x.TargetUrl
            }).ToList();
            return response;
        }
    }
}
