using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IScrollImageHandler : IInterfaceBase
    {
        /// <summary>
        /// 编辑 滚动图
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool EditRollImages(EditRollImageRequest requset);

        /// <summary>
        /// 删除 滚动图
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveRollImages(RemoveRollImageRequset requset);

        /// <summary>
        /// 滚动图 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetRollImageDetailsResponse GetRollImagesDetail(GetRollImageDetailRequset requset);

        /// <summary>
        /// 滚动图 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetRollImageResponse GetRollImages(GetRollImageRequest requset);
    }
}
