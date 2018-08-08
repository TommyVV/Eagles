import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";
import Util from "../utils/util";
const { TESTPAPER } = serverConfig;

// 根据id查看试卷详情
export const getQuestionInfoById = async params => {
  try {
    let res = await sendRequest({
      url: TESTPAPER.TESTPAPER_DETAIL,
      method: "post",
      params
    });
    let { Code, Result, Message } = res.data;
    if (Code === "00") {
      return Result;
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 查看试卷列表
export const getQuestionList = async params => {
  try {
    let res = await sendRequest({
      url: TESTPAPER.TESTPAPER_LIST,
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

// 创建或编辑试卷
export const createOrEditQuestion = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: TESTPAPER.TESTPAPER_EDIT,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e);
  }
};

// 删除试卷
export const deleteQuestion = async params => {
  try {
    const user = Util.getOrgIdAndBranchId();
    params = { ...params, ...user };
    let res = await sendRequest({
      method: "post",
      url: TESTPAPER.TESTPAPER_DELETE,
      params
    });
    if (res.status === 200) {
      return res.data;
    } else {
      let { Code,  Message } = res.data;
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
