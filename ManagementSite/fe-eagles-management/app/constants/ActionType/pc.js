// app
const appType = {
  SHOW_MODAL_CONTAINER: "SHOW_MODAL_CONTAINER", // 显示模态框禁用
  HIDE_MODAL_CONTAINER: "HIDE_MODAL_CONTAINER" // 显示模态框禁用
};
// 用户
const userType = {
  SAVE_USER_INFO: "SAVE_USER_INFO" // 保存用户信息
};

// 试卷
const questionType = {
  SAVE_INFO: "SAVE_INFO", //保存详情
  CLEAR_INFO: "CLEAR_INFO" // 清空详情
};
// 商品
const goodsType = {
  SAVE_INFO: "SAVE_INFO", //保存商品详情
  CLEAR_INFO: "CLEAR_INFO" // 清空商品详情
};
// 机构
const orgType = {
  SAVE_ORG_INFO: "SAVE_ORG_INFO", //保存机构详情
  CLEAR_ORG_INFO: "CLEAR_ORG_INFO" // 清空机构详情
};
// 新闻
const newsType = {
  SAVE_NEWS_INFO: "SAVE_NEWS_INFO", // 保存新闻详情
  CLEAR_NEWS_INFO: "CLEAR_NEWS_INFO" // 清空新闻详情
};

export const actionType = {
  appType,
  userType,
  questionType,
  goodsType,
  orgType,
  newsType
};
