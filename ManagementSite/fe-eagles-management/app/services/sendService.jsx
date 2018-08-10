import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { ORDER } = serverConfig;
// 根据id查看详情
export const getInfoById = async params => {
  try {
    let res = await sendRequest({
      url: ORDER.ORDER_DETAIL,
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

// 查看列表
export const getList = async params => {
  try {
    let res = await sendRequest({
      url: ORDER.ORDER_LIST,
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

// 编辑
export const edit = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: ORDER.ORDER_EDIT,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e);
  }
};
