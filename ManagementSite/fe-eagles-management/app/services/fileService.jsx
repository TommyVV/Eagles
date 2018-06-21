import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';

const { FILE } = serverConfig;

// 文件上传
export const fileUpload = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: FILE.UPLOAD,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
// 文件下载
export const downloadFile = async (params) => {
  try {
    const res = await sendRequest({
      url: FILE.DOWNLOAD,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

//文件删除
export const deleteFile = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: FILE.DELETE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

//移动端上传文件接口
export const uploadProjectFile = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: FILE.UPLOAD_PERJECT_FILE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

//获取某人已下载的文件列表
export const getDownLoadFileList = async (params) => {
  try {
    const res = await sendRequest({
      method: 'get',
      url: FILE.GET_DOWNLOAD_FILE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

//获取某人下载中的文件列表
export const getDownLoadingFileList = async (params) => {
  try {
    const res = await sendRequest({
      method: 'get',
      url: FILE.GET_DOWNLOADING_FILE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

// 文件下载（批量打包下载）
export const downloadBatchFile = async (url) => {
  try {
    const res = await sendRequest({
      url,
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};