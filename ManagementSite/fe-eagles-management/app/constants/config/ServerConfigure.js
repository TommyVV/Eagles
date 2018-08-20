/**
 * 服务器请求URL配置
 */

// const API_SERVER = ""; //mock
const API_SERVER = "http://51service.xyz/ManagementService"; //test

const LOGIN = {
  LOGIN: "/api/Login/Login", //登录
  AUTH: "/api/Login/GetAuthorityByToken" //权限
};
// 活动
const ACTIVITY = {
  EDIT: "/api/Activity/EditActivity", //编辑活动
  DELETE: "/api/Activity/RemoveActivity", //删除活动
  DETAIL: "/api/Activity/GetActivityDetail", //活动详情
  LIST: "/api/Activity/GetActivity" //活动列表
};
// 菜单
const MENU = {
  MENU_EDIT: "/api/Menus/EditMenus", //编辑菜单
  MENU_DELETE: "/api/Menus/RemoveMenus", //删除菜单
  MENU_LIST: "/api/Menus/GetMenus", //菜单列表
  MENU_DETAIL: "/api/Menus/GetMenusDetail", //菜单详情
  MENU_NEXT_LIST: "/api/Menus/GetSubordinate", //下级菜单列表
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
  NEWS_DETAIL: "/api/News/GetNewsDetail", //新闻详情
  NEWS_IMPORT: "/api/News/ImportNews", //新闻导入
};
// 操作员
const OPERATOR = {
  OPERATOR_EDIT: "/api/Operator/EditOperator", //编辑操作员
  OPERATOR_DELETE: "/api/Operator/RemoveOperator", //删除操作员
  OPERATOR_LIST: "/api/Operator/GetOperator", //操作员列表
  OPERATOR_DETAIL: "/api/Operator/GetOperatorDetail" //操作员详情
};
// 发货
const ORDER = {
  ORDER_EDIT: "/api/Order/EditOrders", //编辑发货
  ORDER_LIST: "/api/Order/GetOrders", //发货列表
  ORDER_DETAIL: "/api/Order/GetOrdersDetail" //发货详情
};
// 组织机构
const ORG = {
  ORG_EDIT: "/api/Organization/EditOrganization", //编辑机构
  ORG_DELETE: "/api/Organization/RemoveOrganization", //删除机构
  ORG_LIST: "/api/Organization/GetOrganization", //机构列表
  ORG_DETAIL: "/api/Organization/GetOrganizationDetail", //机构详情
  ORG_TREE: "/api/Organization/GetOrganizationChart", //机构树
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
  TESTPAPER_EDIT: "/api/TestPaper/EditExercises", //编辑试卷
  TESTPAPER_DELETE: "/api/TestPaper/RemoveExercises", //删除试卷
  TESTPAPER_LIST: "/api/TestPaper/GetExercises", //试卷列表
  TESTPAPER_DETAIL: "/api/TestPaper/GetExercisesDetail" //试卷详情
};
//todo  习题（接口注释有问题）
const EXERCISE = {
  EDIT: "/api/TestPaper/EditSubject", //编辑习题
  DELETE: "/api/TestPaper/RemoveSubjectInfo", //删除习题
  DELETE_RELATION: "/api/TestPaper/RemoveSubject", //删除习题与试卷的关系
  LIST: "/api/TestPaper/GetSubjectList", //试卷习题
  DETAIL: "/api/TestPaper/GetSubjectDetail", //习题
  RANDOM: "/api/TestPaper/GetRandomSubject" //习题随机生成
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
//积分
const SCORE = {
  EDIT: "/api/Score/EditScoreSetUp", //编辑
  DELETE: "/api/Score/RemoveScoreSetUp", //删除
  DETAIL: "/api/Score/GetScoreSetUpDetail", //积分详情
  LIST: "/api/Score/GetScoreSetUp", //积分列表
  RANK_LIST: "/api/Score/GetScoreRank", //积分排行列表
  RANK_DETAIL: "/api/Score/GetScoreRankDetail" //积分排行详情
};
//党员
const MEMBER = {
  EDIT: "/api/User/EditPartyMember", //编辑
  DELETE: "/api/User/RemovePartyMember", //删除
  DETAIL: "/api/User/GetPartyMemberDetail", //详情
  LIST: "/api/User/GetPartyMember", //列表
  NEXT_LIST: "/api/User/GetAuthorityUserSetUp", //下级列表
  SET_NEXT: "/api/User/CreateAuthorityUserSetUp", //设置下级
  IMPORT: "/api/User/BatchImportUser" //导入
};
//支部
const BRANCH = {
  EDIT: "/api/Branch/EditBranch", //编辑
  DELETE: "/api/Branch/RemoveBranch", //删除
  DETAIL: "/api/Branch/GetBranchDetail", //详情
  LIST: "/api/Branch/GetBranch" //列表
};
//系统消息
const SYSTEM = {
  EDIT: "/api/SystemNews/EditSystemNews", //编辑
  DELETE: "/api/SystemNews/RemoveSystemNews", //删除
  DETAIL: "/api/SystemNews/GetSystemNewsDetail", //详情
  LIST: "/api/SystemNews/GetSystemNews" //列表
};
//系统短信
const SYSTEMSMS = {
  EDIT: "/api/SMS/EditSMS", //编辑
  DELETE: "/api/SMS/RemoveSMS", //删除
  DETAIL: "/api/SMS/GetSMSDetail", //详情
  LIST: "/api/SMS/GetSMS" //列表
};
//机构短信
const ORGSMS = {
  EDIT: "/api/SMS/EditOrgSmsConfig", //编辑
  DETAIL: "/api/SMS/GetSMSOrgDetail", //详情
  LIST: "/api/SMS/GetSMSOrg" //列表
};
//权限组
const AUTHGROUP = {
  EDIT: "/api/OperGroup/EditOperGroup", //编辑
  DETAIL: "/api/OperGroup/GetOperGroupDetail", //详情
  LIST: "/api/OperGroup/GetOperGroupList", //列表
  DELETE: "/api/OperGroup/RemoveOperGroup", //删除
  PERMISSION_DETAIL: "/api/OperGroup/GetAuthorityGroupSetUp", //权限详情
  PERMISSION_EDIT: "/api/OperGroup/EditAuthorityGroupSetUp" //权限编辑
};
//会议
const MEETING = {
  EDIT: "/api/Metting/ImportMeetingUser", //编辑
  DETAIL: "/api/Metting/GetMettingUsers" //详情
};
//公开任务
const PUBLIC_TASK = {
  LIST_BRANCH: "/api/Publicity/GetBrnPublicTask", //列表 支部
  LIST_ORG: "/api/Publicity/GetOrgPublicTask", //列表 机构
  // EDIT: "/api/Metting/ImportMeetingUser", //审核
  DETAIL: "/api/Publicity/GetPublicTaskDetail" //详情
};
//公开活动
const PUBLIC_ACTIVITY = {
  LIST_BRANCH: "/api/Publicity/GetBrnPublicActivity", //列表 支部
  LIST_ORG: "/api/Publicity/GetOrgPublicActivity", //列表 机构
  // EDIT: "/api/Metting/ImportMeetingUser", //审核
  DETAIL: "/api/Publicity/GetPublicActivityDetail" //详情
};
//用户文章
const PUBLIC_ARTICLE = {
  LIST_BRANCH: "/api/Publicity/GetBrnPublicArticle", //列表 支部
  LIST_ORG: "/api/Publicity/GetOrgPublicArticle", //列表 机构
  // EDIT: "/api/Metting/ImportMeetingUser", //审核
  DETAIL: "/api/Publicity/GetAritcleDetail" //详情
};
//审核
const AUDIT = {
  AUDIT: "/api/Audit/Audit",
  LIST:"/api/Audit/GetAudit"
};
//报表
const REPORT = {
  PARTYMEMBER: "/api/Chart/GetPieChart",
  ACTIVITY_TASK:"/api/Chart/GetHistogram",
  ARTICLE:"/api/Chart/GetLineChart",
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
  ORDER,
  ORG,
  GOODS,
  IMAGE,
  TESTPAPER,
  EXERCISE,
  EDITPARTYMEMBER,
  FILE,
  AREA,
  SCORE,
  MEMBER,
  BRANCH,
  SYSTEM,
  SYSTEMSMS,
  ORGSMS,
  AUTHGROUP,
  MEETING,
  PUBLIC_TASK,
  PUBLIC_ACTIVITY,
  PUBLIC_ARTICLE,
  AUDIT,
  REPORT
};
