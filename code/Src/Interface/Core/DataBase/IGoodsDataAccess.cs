using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.Product;
using Eagles.Application.Model.Goods.Requset;

namespace Eagles.Interface.Core.DataBase
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