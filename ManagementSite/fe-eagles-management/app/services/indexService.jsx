
import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';
/**
 * 获取首页
 * @param {*} params 
 */
export const getIndexData = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.INDEX.GET_INDEX_DATA,
      params: { ...params }
    });
    if (res.data.code == 0) {
      return res.data;
    } else {
      throw new Error('系统异常');
    }
  } catch (e) {
    throw new Error(e);
  }
};
/**
 * 获取广告信息
 * @param {*} params 
 */
export const getAdvertisement = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.INDEX.GET_ADVERTISEMENT,
      params: { ...params }
    });
    if (res.data.code == 0) {
      return res.data;
    } else {
      throw new Error('系统异常');
    }
  } catch (e) {
    throw new Error(e);
  }
};
