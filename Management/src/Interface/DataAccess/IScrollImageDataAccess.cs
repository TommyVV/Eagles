using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        TbScrollImage GetRollImagesDetail(GetRollImageDetailRequset requset);
        List<TbScrollImage> GetRollImagesList(GetRollImageRequest requset, out int totalCount);
    }
}
