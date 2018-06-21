import {
  PcActionType
} from "../../constants/ActionType/pc";

// 存储默认项目信息
export const saveProjectInfo = payload => {
  return {
    payload,
    type: PcActionType.projectType.SAVE_PC_PROJECT_INFO
  }
}
// 选择成员
export const chooseMember = payload => {
  return {
    payload,
    type: PcActionType.projectType.CHOOSE_MEMBER
  }
}
// 选择需求
export const chooseDemand = payload => {
  return {
    payload,
    type: PcActionType.projectType.CHOOSE_DEMAND
  }
}
// 清除信息
export const clearProjectInfo = payload => {
  return {
    type: PcActionType.projectType.CLEAR_PROJECT_INFO
  }
}
// 移除成员
export const removeMemberFn = payload => {
  return {
    payload,
    type: PcActionType.projectType.REMOVE_MEMBER
  }
}
// 移除成员
export const removeDemandFn = payload => {
  return {
    payload,
    type: PcActionType.projectType.REMOVE_DEMAND
  }
}
// 保存上传文件
export const saveFileName = payload => {
  return {
    payload,
    type: PcActionType.projectType.SAVE_FILELIST_NAME
  }
}