import Mock from "mockjs";

import login from "./login/login.json";
import list from "./question/list.json";
import detail from "./question/detail.json";

const Random = Mock.Random;
// ---------------------------------------- 登录 ----------------------------------------
// 基于轻推的验证登录
Mock.mock(/^\/api\/Login\/Login/, login);

// 刷新token
Mock.mock(/^\/token\/refresh/, {
  refresh_token: () => Random.guid(),
  token: () => Random.guid()
});
// 试卷详情
Mock.mock(/^\/api\/TestPaper\/GetExercisesDetail/, detail);
// 试卷列表
Mock.mock(/^\/api\/TestPaper\/GetExercises/, list);