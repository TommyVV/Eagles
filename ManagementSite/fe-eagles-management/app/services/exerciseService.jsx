import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";
import Util from "../utils/util";

const { EXERCISE } = serverConfig;
// 根据id查看详情
export const getInfoById = async params => {
  try {
    let res = await sendRequest({
      url: EXERCISE.DETAIL,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Result;
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 查看列表
export const getList = async params => {
  try {
    let res = await sendRequest({
      url: EXERCISE.LIST,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : { List: [] };
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 创建或编辑
export const createOrEdit = async params => {
  try {
    const user = Util.getOrgIdAndBranchId();
    params = { ...params, ...user };
    let res = await sendRequest({
      method: "post",
      url: EXERCISE.EDIT,
      params
    });
    let { Code, Message } = res.data;
    if (res.status === 200) {
      return res.data;
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 删除
export const del = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: EXERCISE.DELETE,
      params
    });
    let { Code, Message } = res.data;
    if (res.status === 200) {
      return res.data;
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
