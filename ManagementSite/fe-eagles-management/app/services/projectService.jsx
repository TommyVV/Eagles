import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';
import { message } from "antd";

const { PROJECT } = serverConfig;
//  ------------------------ pc 端 ------------------------
// 根据项目id查看项目详情
export const getProjectInfoById = async (params) => {
  try {
    let res = await sendRequest({
      url: PROJECT.INFO,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return res.data.data
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};

// 查看项目列表
export const getProjectList = async (params) => {
  try {
    let res = await sendRequest({
      url: PROJECT.LIST,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return res.data.data
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};

// 查看文件列表
export const getFileList = async (params) => {
  try {
    let res = await sendRequest({
      url: PROJECT.FILELIST,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return res.data.data
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};

// 创建项目
export const createProject = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: PROJECT.UPDATE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

// 删除项目
export const deleteProject = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: PROJECT.DELETE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

// 删除项目文件
export const deleteProjectFile = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: PROJECT.DELETE_FILE,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};

//查看移动端项目列表
export const getAppProjectList = async (params) => {
  try {
    let res = await sendRequest({
      url: PROJECT.APP_LIST,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return res.data.data
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};

//查看移动端项目文件
export const getAppFileList = async (params) => {
  try {
    let res = await sendRequest({
      url: PROJECT.APPFILELIST,
      params
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
};
