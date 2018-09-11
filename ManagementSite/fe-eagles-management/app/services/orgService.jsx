import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { ORG } = serverConfig;
// 根据id查看组织详情
export const getOrgInfoById = async params => {
  try {
    let res = await sendRequest({
      url: ORG.ORG_DETAIL,
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

// 查看组织列表
export const getOrgList = async params => {
  try {
    let res = await sendRequest({
      url: ORG.ORG_LIST,
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

// 创建或编辑组织
export const createOrEditOrg = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: ORG.ORG_EDIT,
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

// 删除组织
export const deleteOrg = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: ORG.ORG_DELETE,
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

// 查看组织数
export const getTreeInfo = async params => {
  try {
    let res = await sendRequest({
      url: ORG.ORG_TREE,
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