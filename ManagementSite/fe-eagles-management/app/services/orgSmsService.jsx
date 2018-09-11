import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { ORGSMS } = serverConfig;
// 根据id查看详情
export const getInfoById = async params => {
  try {
    let res = await sendRequest({
      url: ORGSMS.DETAIL,
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

// 查看列表
export const getList = async params => {
  try {
    let res = await sendRequest({
      url: ORGSMS.LIST,
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

// 创建或编辑
export const createOrEdit = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: ORGSMS.EDIT,
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
