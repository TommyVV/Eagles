import axios from "axios";
import qs from "qs";
import { serverConfig } from "./ServerConfigure";

const UPLOAD = serverConfig.API_SERVER + serverConfig.FILE.UPLOAD; // 上传文件
const DOWNLOAD = serverConfig.API_SERVER + serverConfig.FILE.DOWNLOAD; // 下载文件
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

// const downloadUrl = url => {
//   let iframe = document.createElement('iframe')
//   iframe.style.display = 'none'
//   iframe.src = url
//   iframe.onload = function () {
//     document.body.removeChild(iframe)
//   }
//   document.body.appendChild(iframe)
// }

axios.interceptors.request.use(function (config) {
  if (config.method === 'post') {
    // if (config.url === UPLOAD){
    //   console.log('文件上传')
    //   config.headers = {
    //     'Content-Type': 'multipart/form-data'
    //   }
    //   return config;
    // }
    config.data = qs.stringify(config.data)
  }
  return config
}, function (error) {
  return Promise.reject(error);
})

// axios.interceptors.response.use(res => {
//   // 处理下载文件
//   if (res.headers && (res.headers['content-type'] === 'application/octet-stream;charset=UTF-8')) {
//     console.log('下载文件 - start')
//     console.log(res.data)
//     let files = new Blob(res.data)
//     console.log(files)
//     downloadUrl(res.request.responseURL)
//     // downloadUrl(res.config.url)
//   }
//   return res
// }, error => {
//   // Do something with response error
//   return Promise.reject(error.response.data || error.message)
// })