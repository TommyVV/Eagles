import {
  PcActionType
} from "../../constants/ActionType/pc";
// SAVE_LABEL: 'SAVE_LABEL', // 标签改变
export const saveLabel = payload => {
  return {
    payload,
    type: PcActionType.agencyType.SAVE_LABEL
  }
}
// SAVE_AVATAR_URL: 'SAVE_AVATAR_URL', // 上传头像保存的url数组
export const saveFileUrl = payload => {
  return {
    payload,
    type: PcActionType.agencyType.SAVE_FILE_URL
  }
}

// SAVE_AGENCY_INFO: 'SAVE_AGENCY_INFO', // 初始化加载保存机构详情
export const saveAgencyInfo = payload => {
  return {
    payload,
    type: PcActionType.agencyType.SAVE_AGENCY_INFO
  }
}
// CLEAR_AGENY_INFO: 'CLEAR_AGENY_INFO', // 清除机构信息
export const clearInfo = payload => {
  return {
    type: PcActionType.agencyType.CLEAR_AGENY_INFO
  }
}

// 移除成员
export const removeMemberFn = payload => {
  return {
    payload,
    type: PcActionType.agencyType.REMOVE_MEMBER
  }
}

// 选择成员
export const chooseMember = payload => {
  return {
    payload,
    type: PcActionType.agencyType.CHOOSE_MEMBER
  }
}