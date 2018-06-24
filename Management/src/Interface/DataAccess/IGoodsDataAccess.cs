using System.Collections.Generic;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Product;

namespace Eagles.Interface.DataAccess
{
    public interface IGoodsDataAccess : IInterfaceBase
    {
        int EditGoods(TbProduct mod);
        int CreateGoods(TbProduct mod);
        int RemoveGoods(RemoveGoodsRequset requset);
        TbProduct GetGoodsDetail(GetGoodsDetailRequset requset);
        List<TbProduct> GetNewsList(GetGoodsRequest requset);
    }
}