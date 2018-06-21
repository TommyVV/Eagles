
import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';
/**
 * 获取我的数据
 * @param {*} regionId 区域id
 */
export const getPersonInfo = async (params)=> {
    try {
      const res = await sendRequest({
        url: serverConfig.MY.GET_PERSONINFO,
        params: { ...params}
      });
      console.log(res);
      return res.data;
    } catch (e) {
      throw new Error(e)
    }
};

/**
 * 获取我关注的人
 */
export const getConcernedPerson = async(params) =>{
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_CONCERNED_PERSON,
      params: { ...params },
      // method: 'post'
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取我关注的机构
 */
export const getConcernedOrg = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_CONCERNED_ORG,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}


/**
 * 获取我的粉丝
 */
export const getMyFans = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_FANS,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 取消关注
 */
export const deleteConcern = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.DELETE_FUCOS,
      params: { ...params },
      method: 'post'
    });
    if(res.data.code==0){
      return res.data;
    }else{
      throw new Error('取消关注失败');
    }
  } catch (e) {
    throw new Error(e);
  }
}

/**
 * 添加关注
 */
export const addConcern = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.ADD_fOCUS,
      params: { ...params },
      method: 'post'
    });
    if(res.data.code==0){
      return res.data;
    }else{
      throw new Error('关注失败');
    }
  } catch (e) {
    throw new Error(e);
  }
}

/**
 * 获取我的分享
 */
export const getMyShare = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_MY_SHARE,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取我的分享累计收入
 */
export const getMyIncome = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_SHARE_INCOME,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取我的分享统计
 */
export const getMyShareStatistics = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_SHARE_STATISTICS,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取我的机构的分享统计
 */
export const getOrgStatistics = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_ORG_SHARE_STATISICS,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取我的分享统计明细
 */
export const getStatisticsDetail = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_SHARE_STATISTICS_DETAIL,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取分享浏览历史
 */
export const getShareHistory = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_HISTORY_SHARE,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取机构浏览历史
 */
export const getOrgHistory = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_HISTORY_ORG,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取需求浏览历史
 */
export const getDemandHistory = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_HISTORY_REQUIREMENT,
      params: { ...params },
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 删除浏览历史
 */
export const deleteViewHistory = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.DELETE_HISTORY_VIEW,
      params: { ...params },
      method: 'post'
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
}

/**
 * 获取已购分享列表
 */
export const getBoughtSHare = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_BOUGHT_SHARE,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
} 

/**
 * 获取收藏的分享
 */
export const getCollectShare = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_COLLECT_SHARE,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
} 

/**
 * 获取收藏的项目需求
 */
export const getCollectDemand = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_COLLECT_REQUIREMENT,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
} 

/**
 * 获取我的机构介绍
 */
export const getMyOrgInfo = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_ORG_INFO,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
} 

/**
 * 获取某人的需求
 */
export const getPersonDemand = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_PERSON_REQUIREMENT,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
} 

/**
 * 获取我的需求
 */
export const getMyDemand = async (params) => {
  try {
    const res = await sendRequest({
      url: serverConfig.MY.GET_MY_REQUIREMENT,
      params: { ...params }
    });
    return res.data;
  } catch (e) {
    throw new Error(e)
  }
} 
