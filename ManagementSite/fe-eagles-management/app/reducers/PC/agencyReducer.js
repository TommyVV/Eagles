import {
  PcActionType
} from "../../constants/ActionType/pc";

// abbreviation	公司简称	string
// avatar	头像	string
// businessName	业务名称	string
// capacityProtection	能力保障	string
// capacityProtectionImg	能力保障图片	string	机构保存的图片全以; 隔开的字符串
// companyName	公司名称	string
// creatorAvatar	创建者头像	string
// creatorId	创建者id	string
// creatorName	创建者名称	string
// label	标签	string
// managerIds	管理者id列表	string	多条数据使用“；”隔开
// otherInformation	其他信息	string
// otherInformationImg	其他信息图片	string
// outstandingPerformance	突出业绩	string
// outstandingPerformanceImg	突出业绩图片	string
// qualification	公司资质	string
// serviceContent	服务内容	string
// serviceContentImg	服务内容图片	string
// token	身份令牌	string
// typicalCase	典型案例	string
// typicalCaseImg	典型案例图片	string

const agencyInfo = {
  abbreviation: '', //公司简称 string
  avatar: '', //头像 string
  businessName: '', //业务名称 string
  capacityProtection: '', //能力保障 string
  capacityProtectionImg: '', //能力保障图片 string 机构保存的图片全以;隔开的字符串
  capacityProtectionImgCount:0,
  companyName: '', //公司名称 string
  creatorAvatar: '', //创建者头像 string
  creatorId: '', //创建者id string
  creatorName: '', //创建者名称 string
  label: '', //标签 string
  managerIds: '', //管理者id列表 string 多条数据使用“；” 隔开
  otherInformation: '', //其他信息 string
  otherInformationImg: '', //其他信息图片 string
  otherInformationImgCount: 0,
  outstandingPerformance: '', //突出业绩 string
  outstandingPerformanceImg: '', //突出业绩图片 string
  outstandingPerformanceImgCount: 0,
  qualification: '', //公司资质 string
  serviceContent: '', //服务内容 string
  serviceContentImg: '', //服务内容图片 string
  serviceContentImgCount: 0,
  token: '', //身份令牌 string
  typicalCase: '', //典型案例 string
  typicalCaseImg: '', //典型案例图片 string  
  typicalCaseImgCount: 0,
  handleHistoryList: [],
  memberList: [],
  cheackStatus: '',
  deleteList: [], //删除图片，文件列表ID列表,所有的,
  uploadDeleteList: [],//已经上传后保存审核需要删除的图片
}

// SAVE_AGENCY_INFO: 'SAVE_AGENCY_INFO', // 初始化加载保存机构详情
// SAVE_AVATAR_URL: 'SAVE_AVATAR_URL', // 上传头像保存的url数组
// SAVE_SERVICE_URL: 'SAVE_SERVICE_URL', // 上传服务内容保存的url数组 capacity
// SAVE_CAPACITY_URL: 'SAVE_CAPACITY_URL', // 上传能力保障保存的url数组
// SAVE_OUTSTAND_URL: 'SAVE_OUTSTAND_URL', // 上传突出业绩保存的url数组
// SAVE_CASE_URL: 'SAVE_CASE_URL', // 上传典型案例保存的url数组
// SAVE_OTHER_URL: 'SAVE_OTHER_URL', // 上传其他信息保存的url数组
// CLEAR_AGENY_INFO: 'CLEAR_AGENY_INFO', // 清除机构信息

const shareReducer = (state = agencyInfo, action) => {
  switch (action.type) {
    case PcActionType.agencyType.SAVE_AGENCY_INFO:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.agencyType.SAVE_LABEL:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.agencyType.SAVE_FILE_URL:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.agencyType.CHOOSE_MEMBER:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.agencyType.REMOVE_MEMBER:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.agencyType.CLEAR_AGENY_INFO:
      return { ...agencyInfo, deleteList: [], uploadDeleteList: [] }
    default:
      return state
  }
}

export default shareReducer;