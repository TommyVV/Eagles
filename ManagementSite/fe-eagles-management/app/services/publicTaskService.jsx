import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { PUBLIC_TASK } = serverConfig;
// 根据id查看详情
export const getInfoById = async params => {
  try {
    let res = await sendRequest({
      url: PUBLIC_TASK.DETAIL,
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

// 查看列表(支部)
export const getListBranch = async params => {
  try {
    let res = await sendRequest({
      url: PUBLIC_TASK.LIST_BRANCH,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : { Tasks: [], Message };
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 查看列表(组织)
export const getListOrg = async params => {
  try {
    let res = await sendRequest({
      url: PUBLIC_TASK.LIST_ORG,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : { Tasks: [], Message };
    } else {
      throw new Error(Message);
    }
  } catch (e) {
    throw new Error(e);
  }
};

