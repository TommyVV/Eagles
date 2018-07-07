import axios from "axios";
import { serverConfig } from "../constants/ServerConfigure";
const { FILE } = serverConfig;

// 根据id查看新闻详情
export const uploadFile = async file => {
  try {
    var formdata = new FormData(); // 创建form对象
    formdata.append("img", file, file.name); // 通过append向form对象添加数据,可以通过append继续添加数据
    //或formdata1.append('img',file);
    // let res =await axios.post('/xapi/upimage',formdata1,config);
    let res = await axios({
      method: "post",
      url: `${serverConfig.API_SERVER}${FILE.UPLOAD}`,
      headers: { "Content-Type": "multipart/form-data" },
      data: formdata // post请求的数据
    });
    debugger;
    let { Code, Result, Message } = res.data;
    if (res.status === 200) {
      return Result;
    } else {
      throw new Error(`${Code} - ${Message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
