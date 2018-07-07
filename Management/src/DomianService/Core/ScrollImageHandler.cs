using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.RollImage.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
using Eagles.Base;
using Eagles.DomainService.Model.ScrollImage;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class ScrollImageHandler : IScrollImageHandler
    {
        private readonly IScrollImageDataAccess dataAccess;


        public ScrollImageHandler(IScrollImageDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool EditRollImages(EditRollImageRequest requset)
        {
           

            TbScrollImage mod;


            if (requset.Info.Id > 0)
            {
                mod = new TbScrollImage
                {
                    Id = requset.Info.Id,
                    ImageUrl = requset.Info.Img,
                    OrgId = requset.OrgId,
                    PageType = requset.PageId
                };

                return dataAccess.EditRollImages(mod)>0;

            }
            else
            {
                mod = new TbScrollImage
                {
                    //   Id = requset.Info.Id,
                   
                    ImageUrl = requset.Info.Img,
                    OrgId = requset.OrgId,
                    PageType = requset.PageId
                };

                return dataAccess.CreateRollImages(mod)>0;

                
            }
            

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
            TbScrollImage detail = dataAccess.GetRollImagesDetail(requset);

            if (detail == null) throw new TransactionException("M01","无业务数据");

            response.Info = new RollImageInfo
            {
                Img = detail.ImageUrl,
                OrgId = detail.OrgId,
                Id = detail.Id,
                PageId = detail.PageType
            };
            return response;
        }

        public GetRollImageResponse GetRollImages(GetRollImageRequest requset)
        {
            var response = new GetRollImageResponse
            {
                TotalCount = 0,
               
            };
            List<TbScrollImage> list = dataAccess.GetRollImagesList(requset) ?? new List<TbScrollImage>();

            if (list.Count == 0) throw new TransactionException("M01","无业务数据");

            response.List = list.Select(x => new RollImageInfo
            {
                Img = x.ImageUrl,
                OrgId = x.OrgId,
                Id = x.Id,
                PageId = x.PageType
                //AuditStatus = AuditStatus.审核通过,
                //Author = x.Author,
                //CreateTime = x.CreateTime,
                //NewsId = x.NewsId,
                //NewsImg = x.ImageUrl,
                //NewsName = x.Title,
                //// NewsType=NewsType.
                //Source = x.Source,
                //OrgId = x.OrgId
            }).ToList();
            return response;
        }
    }
}
