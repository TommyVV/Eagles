import Mock from 'mockjs';
const Random = Mock.Random;
// ---------------------------------------- 项目需求管理 ----------------------------------------
// 根据状态或关键字分页查询需求发布列表
Mock.mock(/^\/requirement\/search/, {
  code: 0,
  message: "获取成功",
  data: {
    "list|5": [{
      id: () => Random.id(),
      approvalStatus: () => Random.cname(),
      name: () => Random.ctitle(),
      publishTime: () => Random.date(),
      rejectReason: () => Random.cparagraph(),
    }],
    totalSize: 50
  }
})
// 删除需求
Mock.mock(/^\/requirement\/delete/, {
  code: 0,
  message: "删除成功",
})
// ---------------------------------------- 项目需求列表 ----------------------------------------
// 按条件分页查询项目需求列表
Mock.mock(/^\/requirement\/list/, {
  code: 0,
  message: "获取成功",
  data: {
    "list|5": [{
      id: () => Random.id(),
      name: () => Random.ctitle(),
      creatorName: () => Random.cname(),
      description: () => Random.cparagraph(),
      creatorId: () => Random.guid(),
      creatorAvatar: () => Random.image()
    }],
    hasMore: true,
    totalSize: 50
  }
})
// 根据需求id查项目需求详情
Mock.mock(/^\/requirement\/info/, {
  code: 0,
  message: "获取成功",
  data: [{
    comments: [{
      "avatar": 1,
      "content": 1,
      "createTime": 1,
      "id": 1,
      "type": 1,
      "typeId": 1,
      "updateTime": 1,
      "userId": 1,
      "userName": 1
    }],
    isFocus: 0,
    requirementData: {
      "address": () => Random.city(),
      "company": () => Random.ctitle(),
      "contacts": () => Random.name(),
      "createTime": () => Random.date(),
      "creatorAvatar": () => Random.image(),
      "creatorId": () => Random.guid(),
      "creatorName": () => Random.name(),
      "description": () => Random.cparagraph(),
      "effectiveTime": () => Random.date(),
      "id": () => Random.guid(),
      "mobilePhone": '12345678910',
      "name": () => Random.ctitle(),
      "officePhone": '12345678910',
      "period": () => Random.ctitle(),
      "scope": () => Random.cparagraph(),
      "serviceEligibility": () => Random.cparagraph(),
      "sourcesFunds": () => Random.ctitle(),
      "standard": () => Random.cparagraph(),
    }
  }],
})
// ---------------------------------------- 项目需求发布 ----------------------------------------
// 保存或编辑（更新）草稿
Mock.mock(/^\/requirement\/saveOrUpdate/, {
  code: 0,
  message: "保存成功",
  data: {
    require_id: () => Random.guid()
  }
})
// 保存或编辑（更新）草稿
Mock.mock(/^\/requirement\/submit/, {
  code: 0,
  message: "提交审核",
  data: {
    require_id: () => Random.guid()
  }
})