using Eagles.Base;
using Eagles.Application.Model.Curd.Scroll.GetScrollImg;
using Eagles.Application.Model.Curd.Scroll.GetScrollNew;

namespace Eagles.Interface.Core.Scroll
{
    public interface IScrollHandler : IInterfaceBase
    {
        GetScrollImgResponse GetScrollImg(GetScrollImgRequest request);

        GetScrollNewsResponse GetScrollNews(GetScrollNewsRequest request);
    }
}