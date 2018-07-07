import {
  PcActionType
} from "../../constants/ActionType/pc";

// attachment	附件	string
// creatorAvatar	创建者头像	string
// creatorId	创新者id	string
// creatorName	创建者名字	string
// identityId	发布身份id	string
// identityName	发布身份名字	string
// identityType	发布身份类型	string	0: 个人; 1: 组织机构;
// img	图片	string
// indroduction	简介	string
// price	售价	string
// shareId		string	若share_id为null，则为分享发布；若不为null，则为分享管理的编辑
// title	标题	string
// token	身份令牌

const shareinfo = {
  shareId: '',
  creatorId: '',
  creatorName: '',
  creatorAvatar: '',
  title: '',
  price: '',
  introduction: '',
  img: '', //以;拼接id
  attachment: '', //以;拼接id
  identityId: '',
  identityName: '',
  identityType: '',
  imgList: [], //文件对象列表
  attachmentList: [], //附件对象列表
  deleteList: [], //删除图片，文件列表ID列表,所有的,
  uploadDeleteList:[],//已经上传后保存审核需要删除的图片
  count: 0,//图片总数量
}

// SAVE_SHARE_INFO: 'SAVE_SHARE_INFO',// 初始化加载保存分享详情
// DELETE_FILE: 'DELETE_FILE', // 上传图片时保存图片的url数组
// SAVE_FILE_URL: 'SAVE_FILE_URL', // 上传附件时保存附件的url数组
// CHANGE_IDENTITY: 'CHANGE_IDENTITY',// 改变发布者身份
const shareReducer = (state = shareinfo, action) => {
  switch (action.type) {
    case PcActionType.shareType.SAVE_SHARE_INFO:
      return {
        ...state,
        ...action.payload,
      }
    case PcActionType.shareType.DELETE_SHARE_FILE:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.shareType.SAVE_SHARE_FILE_URL:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.shareType.CHANGE_IDENTITY:
      return {
        ...state,
        ...action.payload
      }
    case PcActionType.shareType.CLEAR_SHARE_INFO:
      return { ...shareinfo, deleteList: [], uploadDeleteList:[] }
    default:
      return state
  }
}

export default shareReducer;