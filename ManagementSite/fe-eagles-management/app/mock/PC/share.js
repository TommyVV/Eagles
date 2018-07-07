import Mock from 'mockjs';
const Random = Mock.Random;
// ---------------------------------------- 分享管理 ----------------------------------------
// 根据状态查询分享发布信息
Mock.mock(/^\/share\/getSharesByStatus/, {
  code: 0,
  message: "获取成功",
  data: {
    "shareList|5": [{
      id: () => Random.id(),
      identityName: () => Random.cname(),
      "price|+1": 1,
      rejectReason: () => Random.cparagraph(),
      title: () => Random.ctitle(),
      updateTime: () => Random.date()
    }],
    totalSize: 50
  }
})
// 根据分享id获取分享表单信息
Mock.mock(/^\/share\/edit/, {
  code: 0,
  message: "获取成功",
  data: {
    attachment: [{
      fileId: () => Random.guid(),
      fileName: () => Random.cname(),
      fileUrl: () => Random.image(),
    }],
    creatorAvatar: () => Random.image(),
    creatorId: () => Random.guid(),
    creatorName: () => Random.cname(),
    identityId: () => Random.guid(),
    identityName: () => Random.cname(),
    identityType: 0,
    img: [() => Random.image(), () => Random.image()],
    indroduction: () => Random.cparagraph(),
    price: 200,
    title: () => Random.ctitle(),
  },
})
// 根据分享id获取分享表单信息
Mock.mock(/^\/sharePublish\/getOrgByUserId/, {
  code: 0,
  message: "获取成功",
  'data|3': [{
    identityId: () => Random.guid(),
    identityName: () => Random.cname()
  }]
})
// 删除分享
Mock.mock(/^\/share\/delete/, {
  code: 0,
  message: "删除成功",
})
// 发布分享 - 保存
Mock.mock(/^\/sharePublish\/save/, {
  code: 0,
  message: "发布成功",
  data: {
    shareId: () => Random.guid()
  }
})