export const typeMap = [
  {
    ExercisesType: "5",
    text: "在线考试"
  },
  {
    ExercisesType: "10",
    text: "阅读练习题"
  },
  {
    ExercisesType: "20",
    text: "投票"
  }
];
export const stateMap = [
  {
    Status: "1",
    text: "待审核"
  },
  {
    Status: "2",
    text: "审核通过"
  },
  {
    Status: "3",
    text: "审核不通过"
  }
];
export const ProgramaStateMap = [
  {
    Status: "5",
    text: "待审核"
  },
  {
    Status: "10",
    text: "审核通过"
  },
  {
    Status: "20",
    text: "审核不通过"
  }
];
export const auditStatus = [
  {
    Status: "-1",
    text: "待审核"
  },
  {
    Status: "0",
    text: "审核通过"
  },
  {
    Status: "1",
    text: "审核不通过"
  }
];
export const articleMap = [
  {
    Status: "0",
    text: "文章"
  },
  {
    Status: "1",
    text: "心得体会"
  },
  {
    Status: "2",
    text: "会议"
  }
];
export const pageMap = [
  {
    value: "0",
    text: "首页"
  },
  {
    value: "1",
    text: "党建门户"
  },
  {
    value: "2",
    text: "党务工作"
  },
  {
    value: "3",
    text: "党建学习"
  }
];
export const publicMap = [
  {
    value: "0",
    text: "默认"
  },
  {
    value: "1",
    text: "活动公开"
  },
  {
    value: "2",
    text: "任务公开"
  },
  {
    value: "3",
    text: "文章公开"
  }
];

export const fileSize = 1024 * 1024 * 10;
export const scoreType = [
  {
    value: "0",
    text: "发表文章奖励"
  },
  {
    value: "1",
    text: "文章字数奖励"
  },
  {
    value: "2",
    text: "文章关键字奖励"
  },
  {
    value: "10",
    text: "参加活动奖励"
  },
  {
    value: "11",
    text: "活动分享到支部奖励"
  },
  {
    value: "12",
    text: "活动分享到组织奖励"
  },
  {
    value: "20",
    text: "任务完成奖励"
  },
  {
    value: "21",
    text: "任务分享到支部奖励"
  },
  {
    value: "22",
    text: "任务分享到组织奖励"
  },
  {
    value: "30",
    text: "会议文章奖励"
  },
  {
    value: "40",
    text: "心得体会类型奖励"
  }
];

export const pageCodeMap = new Map([
  ["User0001", "党员维护"],
  ["User0002", "党员导入"],
  ["User0003", "党员上下级关系维护"],
  ["User0004", "积分排行"],
  ["User0005", "用户文章公开"],
  ["Prod0001", "商品维护"],
  ["Prod0002", "商品发货"],
  ["News0001", "新闻维护"],
  ["News0002", "试卷维护"],
  ["News0003", "习题维护"],
  ["News0004", "会议列表"],
  ["News0006", "积分设置"],
  ["orgs0001", "机构维护"],
  ["orgs0002", "机构短信维护"],
  ["orgs0003", "支部维护"],
  ["menu0001", "菜单维护"],
  ["menu0002", "页面滚动图片维护"],
  ["menu0003", "栏目维护"],
  ["actv0001", "活动维护"],
  ["actv0002", "公开活动列表"],
  ["task0001", "任务维护"],
  ["task0002", "公开任务列表"],
  ["ssms0001", "短信维护"],
  ["snew0001", "系统消息维护"],
  ["oper0001", "操作员维护"],
  ["oper0002", "权限组维护"],
  ["oper0003", "权限分配"],
  ["open0001", "活动公开支部审核"],
  ["open0002", "任务公开支部审核"],
  ["open0003", "用户文章公开支部审核"],
  ["open0004", "活动公开机构审核"],
  ["open0005", "任务公开机构审核"],
  ["open0006", "用户文章公开机构审核"],
  ["Audit001", "审核"],
  ["Audit002", "审核流水"],
]);
export const pageCodeGroup = [
  {
    text: "党员管理",
    next: new Map([
      ["User0001", "党员维护"],
      ["User0002", "党员导入"],
      ["User0003", "党员上下级关系维护"],
      ["User0004", "积分排行"],
      ["User0005", "用户文章公开"]
    ])
  },
  {
    text: "商品管理",
    next: new Map([["Prod0001", "商品维护"], ["Prod0002", "商品发货"]])
  },
  {
    text: "新闻管理",
    next: new Map([
      ["News0001", "新闻维护"],
      ["News0002", "试卷维护"],
      ["News0003", "习题维护"],
      ["News0004", "会议列表"],
      ["News0006", "积分设置"],
    ])
  },
  {
    text: "机构管理",
    next: new Map([["orgs0001", "机构维护"],
    ["orgs0002", "机构短信维护"],
    ["orgs0003", "支部维护"],])
  },
  {
    text: "应用管理",
    next: new Map([["menu0001", "菜单维护"],
    ["menu0002", "页面滚动图片维护"],
    ["menu0003", "栏目维护"]])
  },
  {
    text: "活动管理",
    next: new Map([["actv0001", "活动维护"],
    ["actv0002", "公开活动列表"]])
  },

  {
    text: "任务管理",
    next: new Map([["task0001", "任务维护"],
    ["task0002", "公开任务列表"]])
  },
  {
    text: "系统管理",
    next: new Map([["ssms0001", "短信维护"],
    ["snew0001", "系统信息维护"]])
  },
  {
    text: "操作员管理",
    next: new Map([["oper0001", "操作员维护"],
    ["oper0002", "权限组维护"],
    ["oper0003", "权限分配"]])
  },
  {
    text: "审核管理",
    next: new Map([["open0001", "活动公开支部审核"],
    ["open0002", "任务公开支部审核"],
    ["open0003", "用户文章公开支部审核"],
    ["open0004", "活动公开机构审核"],
    ["open0005", "任务公开机构审核"],
    ["open0006", "用户文章公开机构审核"],
    ["Audit001", "审核"],
    ["Audit002", "审核流水"]])
  },
];

// export const pageCodeMap = new Map([
//   ["User0001", "党员维护"],
//   ["User0002", "党员导入"],
//   ["User0003", "党员上下级关系维护"],
//   ["User0004", "积分排行"],
//   ["User0005", "用户文章公开"],
//   ["Prod0001", "商品维护"],
//   ["Prod0002", "商品发货"],
//   ["News0001", "新闻维护"],
//   ["News0002", "试卷维护"],
//   ["News0003", "习题维护"],
//   ["News0004", "会议列表"],
//   ["News0006", "积分设置"],
//   ["orgs0001", "机构维护"],
//   ["orgs0002", "机构短信维护"],
//   ["orgs0003", "支部维护"],
//   ["menu0001", "菜单维护"],
//   ["menu0002", "页面滚动图片维护"],
//   ["menu0003", "栏目维护"],
//   ["actv0001", "活动维护"],
//   ["actv0002", "公开活动列表"],
//   ["task0001", "任务维护"],
//   ["task0002", "公开任务列表"],
//   ["ssms0001", "短信维护"],
//   ["snew0001", "系统信息维护"],
//   ["oper0001", "操作员维护"],
//   ["oper0002", "权限组维护"],
//   ["oper0003", "权限分配"],
//   ["open0001", "活动公开支部审核"],
//   ["open0002", "任务公开支部审核"],
//   ["open0003", "用户文章公开支部审核"],
//   ["open0004", "活动公开机构审核"],
//   ["open0005", "任务公开机构审核"],
//   ["open0006", "用户文章公开机构审核"],
//   ["Audit001", "审核"]
// ]);
