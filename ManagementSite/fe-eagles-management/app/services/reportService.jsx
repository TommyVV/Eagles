import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { REPORT } = serverConfig;

// 查看党员报表
export const getPartyMember = async params => {
  try {
    let res = await sendRequest({
      url: REPORT.PARTYMEMBER,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : {};
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 查看活动\任务报表柱状图
export const getActivity = async params => {
  try {
    let res = await sendRequest({
      url: REPORT.ACTIVITY_TASK,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : {};
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 查看文章报表
export const getArticle = async params => {
  try {
    let res = await sendRequest({
      url: REPORT.ARTICLE,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : {};
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 查看活动\任务报表饼图
export const getActivityPie = async params => {
  try {
    let res = await sendRequest({
      url: REPORT.ACTIVITY_TASK_PIE,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Code == "00" ? Result : {};
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};