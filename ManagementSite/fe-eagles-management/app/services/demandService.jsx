import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';
import { message } from "antd";

const { DEMAND } = serverConfig;

// 获取需求列表
export const getDemandList = async (params) => {
  try {
    let res = await sendRequest({
      url: DEMAND.LIST,
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
// 获取已关闭需求列表
export const getCloseDemandList = async (params) => {
  try {
    let res = await sendRequest({
      url: DEMAND.HASCLOSE,
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

// 根据状态或关键字分页查询需求发布列表
export const searchDemand = async (params) => {
  try {
    let res = await sendRequest({
      url: DEMAND.SEARCH,
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
// 删除需求
export const deleteDemand = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: DEMAND.DELETE,
      params
    });

    let { code } = res.data
    if (code === 0) {
      return res.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
    // let { code } = res.data
    // if (code === 0) {
    //   message.success('删除成功')
    // } else {
    //   throw new Error(`${code} - ${res.data.message}`)
    // }
  } catch (e) {
    throw new Error(e)
  }
};
// 保存需求
export const saveDemand = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: DEMAND.SAVE,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return res.data;
    }
    // if (code === 0) {
    //   message.success('保存成功')
    // } else {
    //   throw new Error(`${code} - ${res.data.message}`)
    // }
  } catch (e) {
    throw new Error(e)
  }
};
// 审核需求
export const reviewDemand = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: DEMAND.REVIEW,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return res.data;
    }
    // if (code === 0) {
    //   message.success('已提交审核')
    // } else {
    //   message.error(res.data.message)
    // }
  } catch (e) {
    throw new Error(e)
  }
};
// 根据需求id查项目需求详情
export const getDemandInfo = async (params) => {
  try {
    let res = await sendRequest({
      url: DEMAND.INFO,
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
// 根据需求id查项目需求详情(编辑的时候需要这个接口)
export const getDemandDetail = async (params) => {
  try {
    let res = await sendRequest({
      url: DEMAND.EDIT_INFO,
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 关闭需求
export const closeDemand = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: DEMAND.CLOSE,
      params
    });
    let { code } = res.data
    if (code === 0) {
      // message.success('关闭成功')
      return res.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 增加需求浏览历史
export const addDemandHistory = async (params) => {
  try {
    const res = await sendRequest({
      method: 'post',
      url: DEMAND.HISTORY,
      params
    });
    if(res.data.code==0){
      return res.data;
    }else{
      throw new Error('增加需求历史失败');
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 取消需求-项目 关联关系
export const releaseDemand = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: DEMAND.RELEASE,
      params
    });

    let { code } = res.data
    console.log('释放 - ',res.data)
    if (code === 0) {
      return res.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e)
  }
};