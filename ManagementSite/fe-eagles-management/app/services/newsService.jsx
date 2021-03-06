import sendRequest from "../utils/requestUtil";
import Util from "../utils/util";
import { serverConfig } from "../constants/config/ServerConfigure";

const { NEWS } = serverConfig;
// 根据id查看新闻详情
export const getNewsInfoById = async params => {
  try {
    let res = await sendRequest({
      url: NEWS.NEWS_DETAIL,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Result;
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 查看新闻列表
export const getNewsList = async params => {
  try {
    let res = await sendRequest({
      url: NEWS.NEWS_LIST,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : { List: [], Message };
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 创建或编辑新闻
export const createOrEditNews = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: NEWS.NEWS_EDIT,
      params
    });
    if (res.status === 200) {
      return res.data;
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 删除新闻
export const deleteNews = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: NEWS.NEWS_DELETE,
      params
    });
    let { Code, Message } = res.data;
    if (res.status === 200) {
      return res.data;
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 新闻org
export const setNewsOrg = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: NEWS.NEWS_ORG,
      params
    });
    let { Code, Message } = res.data;
    if (res.status === 200) {
      return res.data;
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 导入
export const bitchCreate= async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: NEWS.NEWS_IMPORT,
      params
    });
    let { Code, Message } = res.data;
    if (res.status === 200) {
      return res.data;
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};