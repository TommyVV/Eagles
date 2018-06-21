
import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';
import { message } from "antd";

const { SHARE } = serverConfig;

// 根据状态或关键字分页查询分享发布列表
export const searchShare = async (params) => {
  try {
    let res = await sendRequest({
      url: SHARE.SEARCH,
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
// 删除分享
export const deleteShare = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: SHARE.DELETE,
      params
    });
    let { code } = res.data
    if (code === 0) {
      message.success('删除成功')
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 保存分享
export const saveShare = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: SHARE.SAVE,
      params
    });
    let { code } = res.data
    if (code === 0) {
      message.success('保存成功')
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 审核分享
export const reviewShare = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: SHARE.REVIEW,
      params
    });
    let { code } = res.data
    if (code === 0) {
      message.success('已提交审核')
    } else {
      message.error(res.data.message)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 根据分享id查项目分享详情
export const getShareInfo = async (params) => {
  try {
    let res = await sendRequest({
      url: SHARE.INFOBYID,
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
// 分享列表
export const getShareList = async (params) => {
  try {
    let res = await sendRequest({
      url: SHARE.LIST,
      params
    });
    let { code } = res.data;
    console.log(res)
    if (code === 0) {
      return res.data.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 增加分享浏览历史
export const addShareHistory = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: SHARE.HISTORY,
      params
    });
    if(res.data.code==0){
      return res.data;
    }else{
      throw new Error('增加分享历史失败');
    }
  } catch (e) {
    throw new Error(e);
  }
};


// 分享购买
export const payShare = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: SHARE.PAY,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e);
  }
};