import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';

const { COMMENT, MY } = serverConfig;

// 获取评论列表
export const getCommentList = async (params) => {
  try {
    let res = await sendRequest({
      url: COMMENT.LIST,
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 发表评论
export const releaseComment = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: COMMENT.RELEASE,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return res.data.data
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 收藏文章（需求和分享）
export const collectArticle = async (params) => {
  try {
    let res = await sendRequest({
      url: MY.COLLECT_ARTICLE,
      method: 'post',
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 取消收藏文章（需求和分享）
export const delCollectArticle = async (params) => {
  try {
    let res = await sendRequest({
      url: MY.DEL_COLLECT_ARTICLE,
      method: 'post',
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
