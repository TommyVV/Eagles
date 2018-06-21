// app
const appType = {
  SHOW_MODAL_CONTAINER: 'SHOW_MODAL_CONTAINER', // 显示模态框禁用
  HIDE_MODAL_CONTAINER: 'HIDE_MODAL_CONTAINER', // 显示模态框禁用
};
// 用户
const userType = {
  SAVE_USER_INFO: 'SAVE_USER_INFO', // 保存用户信息
};
// 分享
const shareType = {
  SAVE_SHARE_INFO: 'SAVE_SHARE_INFO', // 初始化加载保存分享详情
  DELETE_SHARE_FILE: 'DELETE_SHARE_FILE', // 上传图片时保存图片的url数组
  SAVE_SHARE_FILE_URL: 'SAVE_SHARE_FILE_URL', // 上传附件时保存附件的url数组
  CHANGE_IDENTITY: 'CHANGE_IDENTITY', // 改变发布者身份
  CLEAR_SHARE_INFO: 'CLEAR_SHARE_INFO', // 清除分享信息
};
// 机构
const agencyType = {
  SAVE_AGENCY_INFO: 'SAVE_AGENCY_INFO', // 初始化加载保存机构详情
  SAVE_FILE_URL: 'SAVE_FILE_URL', // 上传头像保存的url数组
  CLEAR_AGENY_INFO: 'CLEAR_AGENY_INFO', // 清除机构信息
  SAVE_LABEL: 'SAVE_LABEL', // 标签改变
  CHOOSE_MEMBER: 'CHOOSE_MEMBER', //选择成员
  REMOVE_MEMBER: 'REMOVE_MEMBER', //删除成员
};
// 项目
const projectType = {
  SAVE_PC_PROJECT_INFO: 'SAVE_PC_PROJECT_INFO', // 上传附件时保存附件的url数组
  CHOOSE_DEMAND: 'CHOOSE_DEMAND', //选择需求
  CHOOSE_MEMBER: 'CHOOSE_MEMBER', //选择成员
  CLEAR_PROJECT_INFO: 'CLEAR_PROJECT_INFO', // 清除项目文件信息
  REMOVE_MEMBER: 'REMOVE_MEMBER', //删除成员
  REMOVE_DEMAND: 'REMOVE_DEMAND',//删除需求
  SAVE_FILELIST_NAME: 'SAVE_FILELIST_NAME'//上传文件列表
};

export const PcActionType = {
  appType,
  shareType,
  agencyType,
  userType,
  projectType
};