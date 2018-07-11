import sendRequest from "../utils/requestUtil";
import { serverConfig } from "../constants/config/ServerConfigure";

const { AREA } = serverConfig;

// 查询所有地区
export const getAllArea = async () => {
  try {
    let res = await sendRequest({
      url: AREA.ALL_AREA,
    });
    if (res.status === 200) {
      return res.data;
    } else {
      throw new Error(`area error`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
