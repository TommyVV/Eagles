import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/ServerConfigure";

const { MODULE } = serverConfig;
// 根据id查看试卷详情
export const getProgramaInfoById = async params => {
  try {
    let res = await sendRequest({
      url: MODULE.MODULE_DETAIL,
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
export const getProgramaList = async params => {
  try {
    let res = await sendRequest({
      url: MODULE.MODULE_LIST,
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
export const createOrEditPrograma = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: MODULE.MODULE_EDIT,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e);
  }
};

// 删除试卷
export const deletePrograma = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: MODULE.MODULE_DELETE,
      params
    });
    let { Code, Message } = res.data;
    if (Code === "00") {
      return res.data;
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
