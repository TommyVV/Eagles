import axios from 'axios';
import {
  message
} from 'antd';
import base from "../config/base";
// import link from '../config/link.js';
import link from "../constants/ServerConfigure";

/**
 * 重定向到login页面  防止用户修改localstoreage的值导致报错
 * @return {undefined} 没有返回值
 */
function redirectLogin() {
  localStorage.clear();
  if (location.pathname === '/login') {
    return
  } else {
    location.href = '/login';
  }
}
/**
 * 刷新token
 * @param  {string} refresh 接受的refresh参数
 * @return {string}         新的token和refresh
 */
let refreshToken = async (refresh) => {
  try {
    let res = await axios({
      // url : 'http://10.65.50.55:8080/qtomp/refreshToken',
      url: `${base}${link.login.refreshToken}`,
      // url: login.refreshToken,
      params: {
        refresh_token: refresh,
      }
    });
    if (res.data.code === '0') { //获取成功
      let {
        token,
        refresh_token
      } = res.data;
      let old_localStorage = JSON.parse(localStorage.info);
      let info = { ...old_localStorage,
        token,
        refresh_token
      };
      localStorage.info = JSON.stringify(info);
    } else {
      return redirectLogin()
    }
  } catch (e) {
    throw new Error(e)
  }
}
/**
 * 获取token  
 * 	1.是否失效   取localStorage   反之刷新获取
 * 	2.是否存在		跳转login
 * @return {string} token和refresh
 */
let getToken = () => {
  let info = localStorage.info ? JSON.parse(localStorage.info) : {};
  let {
    token,
    refresh_token
  } = info;
  if (token && refresh_token) {
    return {
      token,
      refresh_token
    }
  } else {
    return redirectLogin();
  }
}
/**
 * 请求url方法
 * @param  {config} config 配置 （method，url，params）
 * @return {json}        数据列表
 */
async function request({ method = 'get', url, params }) {
  // let { token } = await getToken();
  // let { method = 'get', url, params } = config;
  // params = { ...params, token };
  try {
    console.log('curd - params', params)
    let res = await axios({
      method: method,
      url: `${link.API_SERVER}${url}`,
      // url,
      params: method === 'get' ? params : '', // get请求的数据
      headers: {
        Accept: 'application/json',
        // 'Content-Type': 'application/json;charset=utf-8'
        // 'Content-Type': 'x-www-form-urlencoded'
      },
      data: params // post请求的数据
    })
    return res;
  } catch (e) {
    throw new Error(e);
  }
}
/**
 * 请求url方法
 * @param  {object} config 请求参数配置（method,url,params）
 * @return {json}        数据列表
 */
export default async (config) => {
  // let { refresh_token } = await getToken();
  try {
    let res = await request(config); // 首次请求
    let { code } = res.data;
    if (code === -1) {
      message.error(res.data.message); //系统错误
      throw new Error(res.data.message); //直接抛出错误
    }
    if (code === '4003') { // message==='token已过期'
      await refreshToken(refresh_token); //刷新token
      return request(config); // 再次重新请求
    }
    if (code === '4002') { // message==='token不合法'
      return redirectLogin(); //重定向到login页面
    }
    return res;
  } catch (e) {
    throw new Error(e);
  }
}