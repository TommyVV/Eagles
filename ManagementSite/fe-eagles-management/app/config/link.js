// ----------------- 分享 -----------------
const share = {
  // ---- 分享管理 ----
  delete: '/share/delete', // 删除分享
  edit: '/share/edit', // 根据分享id获取分享表单信息
  status: '/share/getSharesByStatus', // 根据状态查询分享发布信息
  // ---- 知识分享发布 ----
  save: '/sharePublish/save', // 分享发布-保存
  review: '/sharePublish/submitReview', // 分享发布-提交审核
  // ---- 知识分享查询 ----
  info: '/share/getShareInfo', // 按条件分页查询知识分享列表
  infoById: '/share/getShareInfoById', // 根据分享id查知识分享详情
  org: '/sharePublish/getOrgByUserId' // 分享发布-根据用户账号查找用户组织机构
}
// 登录
const login = {
  refresh: '/token/refresh', // 刷新token
  login: '/login' //基于轻推的验证登录
}
export default {
  share,
}