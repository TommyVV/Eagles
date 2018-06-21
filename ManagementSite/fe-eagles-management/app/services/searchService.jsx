import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';


/**
 * 全文搜索接口
 * @param {*} params 
 */
export const searchAll = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.SEARCH.SEARCH_ALL,
      params: { ...params }
    });
    if (res.data.code == 0) {
      return res.data;
    } else {
      throw new Error('异常');
    }
  } catch (e) {
    throw new Error(e);
  }
};
/**
 * 查询搜索记录
 * @param {*} params 
 */
export const getSearchRecord = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.SEARCH.RECORD,
      params: { ...params }
    });
    if (res.data.code == 0) {
      return res.data;
    } else {
      throw new Error('异常');
    }
  } catch (e) {
    throw new Error(e);
  }
};

/**
 * 清空搜索记录
 * @param {*} params 
 */
export const clearSearchRecord = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.SEARCH.CLEAR,
      params: { ...params },
      method: 'post'
    });
    if (res.data.code == 0) {
      return res.data;
    } else {
      throw new Error('异常');
    }
  } catch (e) {
    throw new Error(e);
  }
};