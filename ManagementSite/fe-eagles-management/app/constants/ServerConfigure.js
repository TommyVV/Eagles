/**
 * 服务器请求URL配置
 */

// const API_SERVER = 'http://10.65.52.226:8080/hci-platform'; //比干
// const API_SERVER = 'http://10.65.4.15:8080/hci-platform'; //必烈
// const API_SERVER = 'http://10.65.50.109:8081/hci-platform'; //家家
// const API_SERVER = 'https://operation.qingtui.cn/hci-platform'; //8G服务器
const API_SERVER = ""; //mock
// const API_SERVER = "http://51service.xyz/ManagementService"; //test

// 活动
const LOGIN = {
  LOGIN: "/api/Login/Login", //登录
}
// 活动
const ACTIVITY = {
  ACTIVITY_EDIT: "/api/Activity/EditActivityTask", //编辑活动
  ACTIVITY_REMOVE: "/api/Activity/RemoveActivityTask", //删除活动
  ACTIVITY_DETAIL: "/api/Activity/GetActivityTaskDetail", //活动详情
  ACTIVITY_LIST: "/api/Activity/GetActivityTask" //活动列表
};
// 审核
const AUDIT = {
  AUDIT_CREATE: "/api/CreateAudit", //审核
  AUDIT_LIST: "/api/GetAudit" //审核列表
};
// 菜单
const MENU = {
  MENU_EDIT: "/api/Menus/EditMenus", //编辑菜单
  MENU_DELETE: "/api/Menus/RemoveMenus", //删除菜单
  MENU_LIST: "/api/Menus/GetMenus", //菜单列表
  MENU_DETAIL: "/api/Menus/GetMenusDetail" //菜单详情
};
// 栏目
const MODULE = {
  MODULE_EDIT: "/api/Module/EditColumn", //编辑栏目
  MODULE_DELETE: "/api/Module/RemoveColumn", //删除菜单
  MODULE_LIST: "/api/Module/GetColumn", //栏目列表
  MODULE_DETAIL: "/api/Module/GetColumnDetail" //栏目详情
};
// 新闻
const NEWS = {
  NEWS_EDIT: "/api/News/EditNews", //编辑新闻
  NEWS_DELETE: "/api/News/RemoveNews", //删除新闻
  NEWS_LIST: "/api/News/GetNews", //新闻列表
  NEWS_DETAIL: "/api/News/GetNewsDetail" //新闻详情
};
// 操作员
const OPERATOR = {
  OPERATOR_EDIT: "/api/Operator/EditOperator", //编辑操作员
  OPERATOR_DELETE: "/api/Operator/RemoveOperator", //删除操作员
  OPERATOR_LIST: "/api/Operator/GetOperator", //操作员列表
  OPERATOR_DETAIL: "/api/Operator/GetOperatorDetail" //操作员详情
};
// 权限组
const OPERGROUP = {
  OPERGROUP_EDIT: "/api/OperGroup/EditOperGroup", //编辑权限组
  OPERGROUP_DELETE: "/api/OperGroup/RemoveOperGroup", //删除权限组
  OPERGROUP_DETAIL: "/api/OperGroup/GetOperGroupDetail", //权限组详情
  OPERGROUP_LIST: "/api/OperGroup/GetOperGroupList", //权限组列表
  PERMISSION_DETAIL: "/api/OperGroup/GetAuthorityGroupSetUp", //权限详情
  PERMISSION_EDIT: "/api/OperGroup/EditAuthorityGroupSetUp" //权限编辑
};
// 发货
const ORDER = {
  ORDER_EDIT: "/api/EditOrders", //编辑发货
  ORDER_LIST: "/api/GetOrders", //发货列表
  ORDER_DETAIL: "/api/GetOrdersDetail" //发货详情
};
// 组织机构
const ORG = {
  ORG_EDIT: "/api/Organization/EditOrganization", //编辑机构
  ORG_DELETE: "/api/Organization/RemoveOrganization", //删除机构
  ORG_LIST: "/api/Organization/GetOrganization", //机构列表
  ORG_DETAIL: "/api/Organization/GetOrganizationDetail" //机构详情
};
// 商品
const GOODS = {
  GOODS_EDIT: "/api/Product/EditGoods", //编辑商品
  GOODS_DELETE: "/api/Product/RemoveGoods", //删除商品
  GOODS_LIST: "/api/Product/GetGoods", //商品列表
  GOODS_DETAIL: "/api/Product/GetGoodsDetail" //商品详情
};
// 滚动图片
const IMAGE = {
  IMAGE_EDIT: "/api/ScrollImage/EditScrollImage", //编辑滚动图片
  IMAGE_DELETE: "/api/ScrollImage/RemoveScrollImage", //删除滚动图片
  IMAGE_LIST: "/api/ScrollImage/GetScrollImage", //滚动图片列表
  IMAGE_DETAIL: "/api/ScrollImage/GetScrollImageDetail" //滚动图片详情
};
//todo  试卷（接口注释有问题）
const TESTPAPER = {
  TESTPAPER_EDIT: "/api/TestPaper/EditSubject", //编辑试卷
  TESTPAPER_DELETE: "/api/TestPaper/RemoveExercises", //删除试卷
  TESTPAPER_LIST: "/api/TestPaper/GetExercises", //试卷列表
  TESTPAPER_DETAIL: "/api/TestPaper/GetExercisesDetail" //试卷详情
};
// 党员
const EDITPARTYMEMBER = {
  EDITPARTYMEMBER_EDIT: "/api/User/EditPartyMember", //编辑党员
  EDITPARTYMEMBER_DELETE: "/api/User/RemovePartyMember", //删除党员
  EDITPARTYMEMBER_LIST: "/api/User/GetPartyMember", //党员列表
  EDITPARTYMEMBER_DETAIL: "/api/User/GetPartyMemberDetail", //党员详情
  NEXT_CREATE: "/api/User/CreateAuthorityUserSetUp", //创建下级人员
  NEXT_DELETE: "/api/User/RemoveAuthorityUserSetUp", //删除下级人员
  NEXT_LIST: "/api/User/GetAuthorityUserSetUp" //下级人员列表
};

//文件
const FILE = {
  UPLOAD: "/api/Upload/UploadFile", //文件上传
  DOWNLOAD: "/file/download", //文件下载
  DELETE: "/file/delete" //文件删除
};
//地区
const AREA = {
  ALL_AREA: "/api/Area/GetAreaInfo" //全国地区
};

export const serverConfig = {
  API_SERVER,
  LOGIN,
  ACTIVITY,
  AUDIT,
  MENU,
  MODULE,
  NEWS,
  OPERATOR,
  OPERGROUP,
  ORDER,
  ORG,
  GOODS,
  IMAGE,
  TESTPAPER,
  EDITPARTYMEMBER,
  FILE,
  AREA
};
