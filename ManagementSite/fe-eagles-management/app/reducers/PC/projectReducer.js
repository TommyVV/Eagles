import {
  PcActionType
} from "../../constants/ActionType/pc";


const projectInfo = {
  projectId: '',
  projectMembers: [],
  projectName: '',
  requirementId: '',
  requirementName: '',
  demandData: {}, //选择的需求对象详情
  open_id: '',
  type: 0,
  fileListName: [],//文件列表，防止上传重命名文件
}


// SAVE_PROJECT_INFO: 'SAVE_PROJECT_INFO', // 上传附件时保存附件的url数组
// CHOOSE_DEMAND: 'CHOOSE_DEMAND', //选择需求
// CHOOSE_MEMBER: 'CHOOSE_MEMBER', //选择成员
// CLEAR_PROJECT_INFO: 'CLEAR_PROJECT_INFO', // 清除项目文件信息

const projectReducer = (state = projectInfo, action) => {
  switch (action.type) {
    case PcActionType.projectType.SAVE_PC_PROJECT_INFO:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.projectType.CHOOSE_DEMAND:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.projectType.CHOOSE_MEMBER:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.projectType.REMOVE_MEMBER:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.projectType.REMOVE_DEMAND:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.projectType.SAVE_FILELIST_NAME:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.projectType.CLEAR_PROJECT_INFO:
      return projectInfo
    default:
      return state
  }
}

export default projectReducer;