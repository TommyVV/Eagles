import { actionType } from "../constants/ActionType/pc";

// 保存新闻详情
export const saveInfo = payload => {
  return {
    payload,
    type: actionType.newsType.SAVE_NEWS_INFO
  };
};
// 清除新闻信息
export const clearInfo = () => {
  return {
    type: actionType.newsType.CLEAR_NEWS_INFO
  };
};
