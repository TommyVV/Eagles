using Eagles.Application.Model;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Application.Model.Goods.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IGoodsHandler : IInterfaceBase
    {
        /// <summary>
        /// 编辑 商品
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool EditGoods(EditGoodsRequset requset);

        /// <summary>
        /// 删除 商品
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveGoods(RemoveGoodsRequset requset);

        /// <summary>
        /// 商品 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetGoodsDetailResponse GetGoodsDetail(GetGoodsDetailRequset requset);

        /// <summary>
        /// 商品 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetGoodsResponse GetGoods(GetGoodsRequest requset);
    }
}
