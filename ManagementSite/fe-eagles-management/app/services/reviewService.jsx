import sendRequest from "../utils/requestUtil";
import { serverConfig } from '../constants/ServerConfigure';
import { message } from "antd";

const { REVIEW, SHARE } = serverConfig;

// 知识分享内容提交审核
export const reviewShare = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: REVIEW.SHARE_REVIEW,
      params
    });
    let { code } = res.data
    if (code === 0) {
      return code
    } else {
      throw new Error(`${code} - ${res.data.message}`)
    }
  } catch (e) {
    throw new Error(e)
  }
};
// 查询分享审核列表
export const getShareReviewList = async (params) => {
  try {
    let res = await sendRequest({
      url: SHARE.REVIEWLIST,
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 咨询机构信息提交审核
export const reviewAgency = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: REVIEW.AGENCY_REVIEW,
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
// 分页获取咨询机构审核列表
export const getAgencyReviewList = async (params) => {
  try {
    let res = await sendRequest({
      url: REVIEW.AGENCY_LIST,
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};

// 项目需求内容提交审核
export const reviewDeamnd = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: REVIEW.DEMAND_REVIEW,
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
// 分页获取咨询机构审核列表
export const getDemandReviewList = async (params) => {
  try {
    let res = await sendRequest({
      url: REVIEW.DEMAND_LIST,
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};
// 审核评论
export const reviewComment = async (params) => {
  try {
    let res = await sendRequest({
      method: 'post',
      url: REVIEW.REVIEW_COMMENT,
      params
    });
    let { code } = res.data;
    if (code === 0) {
      return res.data;
    } else {
      throw new Error(`${code} - ${res.data.message}`);
    }
  } catch (e) {
    throw new Error(e);
  }
};