using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Application.Model.Menus.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MenusController : ApiController
    {
        private readonly IMenusHandler testHandler;

        public MenusController(IMenusHandler testHandler)
        {
            this.testHandler = testHandler;
        }
        
        /// <summary>
        /// 编辑  菜单
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditMenus(EditMenusRequset requset)
        {
            return testHandler.EditMenus(requset);
        }

        /// <summary>
        ///  菜单 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase RemoveMenus(RemoveMenusRequset requset)
        {
            return testHandler.RemoveMenus(requset);
        }

        /// <summary>
        /// 菜单 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetMenusResponse GetMenus(GetMenusRequset requset)
        {
            return testHandler.GetMenus(requset);
        }

        /// <summary>
        ///  菜单 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetMenusDetailResponse GetMenusDetail(GetMenusDetailRequest requset)
        {
            return testHandler.GetMenusDetail(requset);
        }

    }
}