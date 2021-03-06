import { actionType } from "../constants/ActionType/pc";

// SAVE_AGENCY_INFO: 'SAVE_AGENCY_INFO', // 初始化加载保存组织详情
export const saveOrgInfo = payload => {
  return {
    payload,
    type: actionType.orgType.SAVE_ORG_INFO
  };
};
// CLEAR_AGENY_INFO: 'CLEAR_AGENY_INFO', // 清除组织信息
export const clearInfo = () => {
  return {
    type: actionType.orgType.CLEAR_ORG_INFO
  };
};
