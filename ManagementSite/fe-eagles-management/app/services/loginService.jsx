import { serverConfig } from "../constants/config/ServerConfigure";
import sendRequest from "../utils/requestUtil";
/**
 * 登录
 * @param {*} params 参数
 */
export const login = async params => {
  try {
    const res = await sendRequest({
      url: serverConfig.LOGIN.LOGIN,
      method:"post",
      params: { ...params }
    });
    if (res.status === 200) {
      return res.data;
    } else {
      let { Code, Message } = res.data;
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
