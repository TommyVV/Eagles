using System;
using Eagles.Base;
using Eagles.Application.Model.Curd.Scroll.GetScrollImg;
using Eagles.Application.Model.Curd.Scroll.GetScrollNew;

namespace Eagles.Interface.Core.Scroll
{
    public interface IScrollHanler : IInterfaceBase
    {
        GetScrollImgResponse GetScoreExchangeLs(GetScrollImgRequest request);
        GetScrollNewsResponse GetScoreRank(GetScrollNewsRequest request);
    }
}