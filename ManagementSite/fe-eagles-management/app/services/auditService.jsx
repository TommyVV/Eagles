import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { AUDIT } = serverConfig;
// 审核
export const audit = async params => {
  try {
    let res = await sendRequest({
      method: "post",
      url: AUDIT.AUDIT,
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