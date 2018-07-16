using System.Web.Http;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Application.Model.Menus.Response;
using Eagles.Interface.Core;
using Eagles.Application.Host.Common;
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
        ///新增/编辑菜单
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditMenus(EditMenusRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.EditMenus(requset));
        }

        /// <summary>
        ///  删除菜单
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveMenus(RemoveMenusRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.RemoveMenus(requset));
        }

        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetMenusResponse> GetMenus(GetMenusRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.GetMenus(requset));
        }

        /// <summary>
        /// 获取当前菜单的子菜单
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetSubordinateResponse> GetSubordinate(GetSubordinateRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.GetSubordinate(requset));
        }

        /// <summary>
        ///  菜单 详情（暂时无用）
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetMenusDetailResponse> GetMenusDetail(GetMenusDetailRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) => testHandler.GetMenusDetail(requset));
        }

    }
}