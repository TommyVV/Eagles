using System.Collections.Generic;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.ScrollImage;

namespace Eagles.Interface.DataAccess
{
    public interface IScrollImageDataAccess : IInterfaceBase
    {
        int CreateRollImages(TbScrollImage mod);
        int EditRollImages(TbScrollImage mod);
        int RemoveRollImages(RemoveRollImageRequset requset);
        TbScrollImage GetRollImagesDetail(int id);
        List<TbScrollImage> GetRollImagesList(GetRollImageRequest requset, out int totalCount,int orgId);
    }
}
