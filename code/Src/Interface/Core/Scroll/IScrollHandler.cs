using Eagles.Base;
using Eagles.Application.Model.Scroll.GetScrollImg;
using Eagles.Application.Model.Scroll.GetScrollNew;

namespace Eagles.Interface.Core.Scroll
{
    public interface IScrollHandler : IInterfaceBase
    {
        GetScrollImgResponse GetScrollImg(GetScrollImgRequest request);

        GetScrollNewsResponse GetScrollNews(GetScrollNewsRequest request);
    }
}