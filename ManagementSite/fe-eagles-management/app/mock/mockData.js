import Mock from 'mockjs';
//我的
import personInfo from './My/personInfo.json';
import concernOrg from './My/concernOrg.json';
import concernPerson from './My/concernPerson.json';
import myFans from './My/myfans.json';
import addFocus from './My/addFocus.json';
import myShare from './My/myShare.json';
import myShareIncome from './My/income.json';
import myShareStatistics from './My/shareStatistics.json';
import myStatisticsDetail from './My/statisticsDetail.json';
import shareHistory from './My/shareHistory.json';
import demandHistory from './My/demandHistory.json';
import orgHistory from './My/orgHistory.json';


import login from './login/login.json';
import agencyList from './Agency/agencyList.json';
import common from './common.json';
import indexData from './Index/indexData.json';

const Random = Mock.Random;
// ---------------------------------------- 登录 ----------------------------------------
// 基于轻推的验证登录
Mock.mock(/^\/login/, login);

// 刷新token
Mock.mock(/^\/token\/refresh/, {
  refresh_token: () => Random.guid(),
  token: () => Random.guid(),
});
// ---------------------------------------- 首页 ----------------------------------------
// 获取首页数据
// Mock.mock(/^\/consultation\/home/, indexData);

// 获取咨询机构列表
Mock.mock(/^\/org\/getOrgList/, agencyList);
// --------------------------------- PC ---------------------------------
import './PC/demand'; // 项目需求
import './PC/share'; // 分享
//我的
//获取我的信息
Mock.mock(/^\/getPersonInfo/, personInfo);

//获取我已关注的对象-机构
Mock.mock(/^\/followed\/org\/get/, concernOrg);

//获取我已关注的对象-个人
Mock.mock(/^\/followed\/person\/get/, concernPerson);

//获取我的粉丝
Mock.mock(/^\/fans\/get/, myFans);

//添加关注
Mock.mock(/^\/addFocus/, addFocus);

//取消关注
Mock.mock(/^\/deleteFocus/, common);

//某人的分享
Mock.mock(/^\/person\/share\/get/, myShare);

//我的分享累计收入
Mock.mock(/^\/person\/share\/income/, myShareIncome);

//获取我的分享统计
Mock.mock(/^\/person\/share\/statistics/, myShareStatistics);

//获取我的分享统计明细
Mock.mock(/^\/share\/statistics\/detail/, myStatisticsDetail);

//分享历史
Mock.mock(/^\/history\/share\/get/, shareHistory);
Mock.mock(/^\/history\/org\/get/, orgHistory);
Mock.mock(/^\/history\/requirement\/get/, demandHistory);