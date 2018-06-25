using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
using Eagles.DomainService.Model.ScrollImage;
using Eagles.Interface.Core;

namespace Eagles.DomainService.Core
{
    public class ScrollImageHandler : IScrollImageHandler
    {
        public ResponseBase EditRollImages(EditRollImageRequest requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TbScrollImage mod;


            //if (requset.OrgId > 0 && requset.PageId>0)
            //{
            //    mod = new TbScrollImage
            //    {
                    
            //    };

            //    int result = dataAccess.EditNews(mod);

            //    if (result > 0)
            //    {
            //        response.IsSuccess = true;
            //    }
            //}
            //else
            //{
            //    mod = new TbNews
            //    {
            //        Attach1 = requset.Info.Attach1,
            //        Attach2 = requset.Info.Attach2,
            //        Attach3 = requset.Info.Attach3,
            //        Attach4 = requset.Info.Attach4,
            //        Attach5 = requset.Info.Attach5,
            //        Author = requset.Info.Author,
            //        BeginTime = requset.Info.StarTime,
            //        EndTime = requset.Info.EndTime,
            //        HtmlContent = requset.Info.Content,
            //        CreateTime = requset.Info.CreateTime,
            //        Module = requset.Info.ModuleId,
            //        //    NewsId = requset.DetailInfo.NewsId,
            //        ImageUrl = requset.Info.NewsImg,
            //        Title = requset.Info.NewsName,
            //        // NewsType=NewsType.
            //        Source = requset.Info.Source,
            //        TestId = requset.Info.TestId,
            //        OrgId = requset.Info.OrgId,
            //    };

            //    int result = dataAccess.CreateNews(mod);

            //    if (result > 0)
            //    {
            //        response.IsSuccess = true;
            //    }
            //}

            return response;

            
        }

        public ResponseBase RemoveRollImages(RemoveRollImageRequset requset)
        {
            throw new NotImplementedException();
        }

        public GetRollImageDetailsResponse GetRollImagesDetail(GetRollImageDetailRequset requset)
        {
            throw new NotImplementedException();
        }

        public GetRollImageResponse GetRollImages(GetRollImageRequest requset)
        {
            throw new NotImplementedException();
        }
    }
}
