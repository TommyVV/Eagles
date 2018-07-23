import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { MEETING } = serverConfig;
// 根据id查看详情
export const getInfoById = async params => {
  try {
    let res = await sendRequest({
      url: MEETING.DETAIL,
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

// 导入人员
export const createOrEdit = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: MEETING.EDIT,
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
