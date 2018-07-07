using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Application.Model.Menus.Response;
using Eagles.Base;
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
        public ResponseFormat<bool> EditMenus(EditMenusRequset requset)
        {
            return ApiActuator.Runing(() => testHandler.EditMenus(requset));
        }

        /// <summary>
        ///  菜单 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveMenus(RemoveMenusRequset requset)
        {
            return ApiActuator.Runing(() => testHandler.RemoveMenus(requset));
        }

        /// <summary>
        /// 菜单 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetMenusResponse> GetMenus(GetMenusRequset requset)
        {
            return ApiActuator.Runing(() => testHandler.GetMenus(requset));
        }

        /// <summary>
        ///  菜单 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetMenusDetailResponse> GetMenusDetail(GetMenusDetailRequest requset)
        {
            return ApiActuator.Runing(() => testHandler.GetMenusDetail(requset));
        }

    }
}