/**
 * 服务器请求URL配置
 */

// const API_SERVER = 'http://10.65.52.226:8080/hci-platform'; //比干
// const API_SERVER = 'http://10.65.4.15:8080/hci-platform'; //必烈
// const API_SERVER = 'http://10.65.50.109:8081/hci-platform'; //家家
// const API_SERVER = 'https://operation.qingtui.cn/hci-platform'; //8G服务器
const API_SERVER = ""; //mock

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
  PERMISSION_EDIT: "/api/OperGroup/EditAuthorityGroupSetUp", //权限编辑
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
  NEXT_LIST: "/api/User/GetAuthorityUserSetUp", //下级人员列表
};

// 搜索
const SEARCH = {
  SEARCH_ALL: "/consultation/search",
  RECORD: "/consultation/history/get",
  CLEAR: "/consultation/history/clear"
};
// 分享
const SHARE = {
  DELETE: "/share/delete", // 删除分享
  EDIT: "/share/edit", // 根据分享id获取分享表单信息
  STATUS: "/share/getSharesByStatus", // 根据状态查询分享发布信息
  SEARCH: "/share/getSharesByStatus", // 根据状态查询分享发布信息
  SAVE: "/sharePublish/save", // 分享发布-保存
  REVIEW: "/sharePublish/submitReview", // 分享发布-提交审核
  INFO: "/share/getShareInfo", // 按条件分页查询知识分享列表
  INFOBYID: "/share/getShareInfoById", // 根据分享id查知识分享详情
  ORG: "/sharePublish/getOrgByUserId", // 分享发布-根据用户账号查找用户组织机构
  LIST: "/share/getShareInfo", // 分享发布-根据用户账号查找用户组织机构
  REVIEWLIST: "/review/share/get", // 分页获取分享内容审核列表
  SUBMIT: "/review/share/submit", //知识分享内容提交审核
  HISTORY: "/history/share/add", // 增加分享浏览历史
  PAY: "/pay/share/pay" //购买知识分享
};
// 咨询机构
const AGENCY = {
  GET_AGENCY_LIST: "/org/getOrgList", // 分页查询机构列表
  LIST: "/org/getOrgList", // 分页查询机构列表
  SHARELIST: "/org/getSharedInfoList", // 咨询机构知识分享列表
  CONCERN: "/org/focus", // 关注咨询机构
  GET_AGENCY_INFO: "/org/info/get", // 咨询机构详情
  SAVE: "/orgPublish/Save", // 机构发布-保存
  REVIEW: "/orgPublish/submitReview", // 机构发布-提交审核
  HISTORY: "/history/org/add" // 增加机构浏览历史
};
// 项目需求
const DEMAND = {
  LIST: "/requirement/list", // 按条件分页查询项目需求列表
  INFO: "/requirement/info", // 根据需求id查项目需求详情
  EDIT_INFO: "/requirement/information", // 根据需求id查项目需求详情（编辑的时候）
  SAVE: "/requirement/saveOrUpdate", // 保存或编辑（更新）草稿
  REVIEW: "/requirement/submit", // 提交审核
  DELETE: "/requirement/delete", // 删除需求
  SEARCH: "/requirement/search", // 根据状态或关键字分页查询需求发布列表
  CLOSE: "/requirement/close", //关闭需求
  HASCLOSE: "/requirement/closed/list", //已关闭需求
  HISTORY: "/history/requirement/add", // 增加需求浏览历史
  RELEASE: "/requirement/project/delete" //取消需求-项目 关联关系
};
// 项目管理
const PROJECT = {
  LIST: "/project/pc/getProjectsList", // 查看项目列表
  INFO: "/project/getProjectInfoById", // 根据项目id查看项目详情
  UPDATE: "/project/update", // 创建项目
  DELETE: "/project/delete", // 删除项目
  DELETE_FILE: "/project/delete/file", //删除项目文件
  FILELIST: "/project/pc/getFileInfo", //项目文件查询,
  APPFILELIST: "/project/app/getFileInfo", //移动端项目文件查询
  UPLOAD: "/project/upload/file", //上传项目文件
  APP_LIST: "/project/app/getProjectsList" //移动端查看项目列表
};
// 审核评论
const REVIEW = {
  SHARE_LIST: "/review/share/get", // 分页获取分享内容审核列表
  SHARE_REVIEW: "/review/share/submit", // 知识分享内容提交审核
  AGENCY_LIST: "/review/org/get", // 分页获取咨询机构审核列表
  AGENCY_REVIEW: "/review/org/submit", // 咨询机构信息提交审核
  DEMAND_LIST: "/review/requirement/get", //分页获取项目需求内容审核列表
  DEMAND_REVIEW: "/review/requirement/submit", // 咨询机构信息提交审核
  REVIEW_COMMENT: "/review/comment/submit" //评论提交审核
};
// 登录
const LOGIN = {
  REFRESH: "/token/refresh", // 刷新token
  LOGIN_HCI: "/login", // 基于轻推的验证登录
  LOGIN_HCI_OPERATION: "/adminLogin", // 基于轻推的验证登录(hci运营)
  QTCONFIG: "/qingtui/jspai/get", //QT配置
  MEMBER: "/qt/member", // 分页查询轻推团队内成员
  HCI_USER: "/qt/user", //按搜索条件分页查询已关注HCI的团队成员
  GET_HCI_APP: "/application/follow/add", //获取app
  GET_OPEN_ID: "/member/get" //根据手机号获取openid
};
// 首页
const INDEX = {
  GET_INDEX_DATA: "/consultation/home", // 获取首页数据
  GET_ADVERTISEMENT: "/advertisement/get" // 获取广告信息
};
//我的
const MY = {
  GET_PERSONINFO: "/getPersonInfo", //获取某人信息
  GET_CONCERNED_PERSON: "/followed/person/get", //获取关注的人
  GET_CONCERNED_ORG: "/followed/org/get", //获取关注的机构
  GET_FANS: "/fans/get", //获取我的粉丝列表
  DELETE_FUCOS: "/deleteFocus", //取消关注
  ADD_fOCUS: "/addFocus", //添加关注
  GET_MY_SHARE: "/my/share/get", //某人,机构的分享
  GET_SHARE_INCOME: "/share/income", //我的分享累计收入（个人，机构）
  GET_SHARE_STATISTICS: "/person/share/statistics", //个人分享统计
  GET_ORG_SHARE_STATISICS: "/org/share/statistics", //机构分享统计
  GET_SHARE_STATISTICS_DETAIL: "/share/statistics/detail", // 个人分享统计明细
  GET_HISTORY_SHARE: "/history/share/get", //分享浏览历史
  GET_HISTORY_ORG: "/history/org/get", //机构浏览历史
  GET_HISTORY_REQUIREMENT: "/history/requirement/get", //需求浏览历史
  DELETE_HISTORY_VIEW: "/history/view/delete", //删除浏览记录
  GET_BOUGHT_SHARE: "/getPersonBoughtShare", //获取某人已购分享
  GET_COLLECT_SHARE: "/collection/getShareList", //获取收藏的分享
  GET_COLLECT_REQUIREMENT: "/collection/getRequirement", //获取收藏的项目需求
  GET_ORG_INFO: "/org/info", //获取我的机构介绍
  GET_PERSON_REQUIREMENT: "/requirement/getRequireByUserId", //获取某人项目需求
  GET_MY_REQUIREMENT: "/requirement/getRequireByMyself", //获取我的项目需求
  GET_PROJECT_LIST: "/project/getProjectsList", //获取某人的项目
  COLLECT_ARTICLE: "/collection/add", //收藏分享或需求
  DEL_COLLECT_ARTICLE: "/collection/delete" //取消收藏分享或需求
};
//文件
const FILE = {
  UPLOAD: "/file/upload", //文件上传
  PROGRESS: "/file/download/progress", //下载进度查询
  DOWNLOAD: "/file/download", //文件下载
  DELETE: "/file/delete", //文件删除
  DELETEPROJECTFILE: "/project/delete/file", //项目文件删除
  UPLOAD_PERJECT_FILE: "/project/file/upload", //移动端上传文件接口
  GET_DOWNLOAD_FILE: "/getPersonDownloadFile", //获取某人已下载的文件列表
  GET_DOWNLOADING_FILE: "/getPersonDownloading", //获取下载中文件
  BATCH: "/file/batch/download?fileIdList=" //文件下载（批量打包下载）
};
// 评论
const COMMENT = {
  LIST: "/review/comment/get", //获取评论列表
  RELEASE: "/review/comment/release" //发表评论，
};

export const serverConfig = {
  API_SERVER,
  SEARCH,
  AGENCY,
  SHARE,
  DEMAND,
  PROJECT,
  LOGIN,
  INDEX,
  MY,
  FILE,
  COMMENT,
  REVIEW
};
