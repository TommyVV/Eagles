import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';

const { SHARE, AGENCY } = serverConfig;

/**
 * 查询咨询机构列表
 * @param {*} params 
 */
export const getAgencyList = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.AGENCY.LIST,
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

// 通过userId拿到OrgId
export const getAgencyIdById = async (params) => {
  try {
    const res = await sendRequest({
      url: SHARE.ORG,
      // params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 通过agencyId获取咨询机构详情
export const getAgencyInfoById = async (params) => {
  try {
    const res = await sendRequest({
      url: AGENCY.GET_AGENCY_INFO,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 通过agencyId获取咨询机构详情
export const getAgencyShareList = async (params) => {
  try {
    const res = await sendRequest({
      url: AGENCY.SHARELIST,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 保存机构
export const saveAgency = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: AGENCY.SAVE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 审核机构
export const reviewAgency = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: AGENCY.REVIEW,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 增加机构浏览历史
export const addAgencyHistory = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: AGENCY.HISTORY,
      params
    });
    if(res.data.code==0){
      return res.data;
    }else{
      throw new Error('增加机构历史失败');
    }
  } catch (e) {
    throw new Error(e);
  }
};