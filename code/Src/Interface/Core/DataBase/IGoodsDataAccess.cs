using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Product;

namespace Eagles.Interface.Core.DataBase
{
    public interface IGoodsDataAccess : IInterfaceBase
    {
        int EditGoods(TB_PRODUCT mod);
        int CreateGoods(TB_PRODUCT mod);
        int RemoveGoods(RemoveGoodsRequset requset);
        TB_PRODUCT GetGoodsDetail(GetGoodsDetailRequset requset);
        List<TB_PRODUCT> GetNewsList(GetGoodsRequest requset);
    }
}
