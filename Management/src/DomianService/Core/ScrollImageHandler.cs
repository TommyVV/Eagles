using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.RollImage.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
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

        public ResponseBase EditRollImages(EditRollImageRequest requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

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

                int result = dataAccess.EditRollImages(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
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

                int result = dataAccess.CreateRollImages(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;

        }

        public ResponseBase RemoveRollImages(RemoveRollImageRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            int result = dataAccess.RemoveRollImages(requset);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public GetRollImageDetailsResponse GetRollImagesDetail(GetRollImageDetailRequset requset)
        {
            var response = new GetRollImageDetailsResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbScrollImage detail = dataAccess.GetRollImagesDetail(requset);

            if (detail == null) throw new Exception("无数据");

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
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbScrollImage> list = dataAccess.GetRollImagesList(requset) ?? new List<TbScrollImage>();

            if (list.Count == 0) throw new Exception("无数据");

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
