
import { serverConfig } from '../constants/ServerConfigure';
import sendRequest from "../utils/requestUtil";
import axios from 'axios';
/**
 * 登录app（移动端HCI）
 * @param {*} params 登录的qtcode等参数
 */
export const loginHci = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.LOGIN_HCI,
      params: { ...params }
    });
    if (res.data.code == 0) {
      return res.data.data;
    } else {
      throw new Error('异常');
    }
  } catch (e) {
    throw new Error(e);
  }
};
/**
 * 登录app（移动端 HCI运营）
 * @param {*} params 登录的qtcode等参数
 */
export const loginOperation = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.LOGIN_HCI_OPERATION,
      params: { ...params }
    });
    if (res.data.code == 0) {
      return res.data.data;
    } else {
      throw new Error('异常');
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 获取轻推用户
export const getMember = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.MEMBER,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 获取已关注HCI用户
export const getHciUser = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.HCI_USER,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 获取hci APP
export const getHciApp = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.GET_HCI_APP,
      method: 'post',
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

/**
 * 配置qtconfig
 */
export const getQtConfigInfo = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.QTCONFIG,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}
/**
 * 根据手机号获取是否是轻推用户
 */
export const getUserInfoByPhone = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.GET_OPEN_ID,
      params: { ...params }
    });
    if (res.data.code == '0') {
      return res.data;
    } else {
      throw new Error(e);
    }
  } catch (e) {
    throw new Error(e);
  }
}

