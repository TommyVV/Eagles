using Eagles.Base;
using Eagles.Application.Model.AppModel.Scroll.GetScrollImg;
using Eagles.Application.Model.AppModel.Scroll.GetScrollNew;

namespace Eagles.Interface.Core.Scroll
{
    public interface IScrollHandler : IInterfaceBase
    {
        GetScrollImgResponse GetScrollImg(GetScrollImgRequest request);

        GetScrollNewsResponse GetScrollNews(GetScrollNewsRequest request);
    }
}