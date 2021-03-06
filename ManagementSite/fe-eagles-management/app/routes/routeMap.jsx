import React, { Component } from "react";
import { Route, IndexRoute, hashHistory, Router } from "react-router";

// -------------------------------------------------  pc端路由  -------------------------------------------------
import PcApp from "../container/App";
import Login from "../container/Login/";

import Exercise from "../container/Exercise/"; // 习题问卷列表
import ExerciseDetail from "../container/Exercise/ExDetail"; // 习题问卷详情
import QuestionDetail from "../container/Exercise/QuDetail"; // 习题详情
import HomePage from "../container/Home";
import PartyMemberList from "../container/PartyMember";
import PartyMemberDetail from "../container/PartyMember/Detail";
import SetNextPartyMember from "../container/PartyMember/Next";
import ImportMember from "../container/PartyMember/ImportMember";
import GoodsList from "../container/Goods";
import GoodsDetail from "../container/Goods/Detail";
import SendList from "../container/Send";
import SendDetail from "../container/Send/Detail";
import SystemList from "../container/System";
import SystemDetail from "../container/System/Detail";
import RankList from "../container/Rank";
import RankDetail from "../container/Rank/Detail";
import OperatorList from "../container/Operator";
import OperatorDetail from "../container/Operator/Detail";
import OrgList from "../container/Org";
import OrgDetail from "../container/Org/Detail";
import ImageList from "../container/Image";
import ImageDetail from "../container/Image/Detail";
import MenuList from "../container/Menu";
import MenuDetailOne from "../container/Menu/DetailOne";
import MenuDetailTwo from "../container/Menu/DetailTwo";
import MeetList from "../container/Meet";
import MeetDetail from "../container/Meet/Detail";
import ImportMeetMember from "../container/Meet/ImportMeetMember";
import ProgramaList from "../container/Programa";
import ProgramaDetail from "../container/Programa/Detail";
import ActivityList from "../container/Activity";
import ActivityDetail from "../container/Activity/Detail";
import NewsList from "../container/News";
import NewsDetail from "../container/News/Detail";
import PermissionList from "../container/Permission";
import PermissionDetail from "../container/Permission/Detail";
import PermissionManage from "../container/Permission/Manage";
import ImportNews from "../container/News/ImportNews";
import SmsSystemList from "../container/SmsSystem";
import SmsSystemDetail from "../container/SmsSystem/Detail";
import SmsOrgList from "../container/SmsOrg";
import SmsOrgDetail from "../container/SmsOrg/Detail";
import ExList from "../container/Exercise/ExDetail/ExList";
import ScoreList from "../container/Score";
import ScoreDetail from "../container/Score/Detail";
import BranchList from "../container/Branch";
import BranchDetail from "../container/Branch/Detail";
import PublicTaskList from "../container/PublicTask";
import PublicTaskDetail from "../container/PublicTask/Detail";
import PublicActivityList from "../container/PublicActivity";
import PublicActivityDetail from "../container/PublicActivity/Detail";
import PublicArticleList from "../container/PublicArticle";
import PublicArticleDetail from "../container/PublicArticle/Detail";
import AuditList from "../container/Audit";
import PartyMemberReport from "../container/Report/PartyMember";
import ActivityReport from "../container/Report/Activity";
import TaskReport from "../container/Report/Task";
import ArticleReport from "../container/Report/Article";
import OrgOperator from "../container/OrgOper";
import OrgOperatorDetail from "../container/OrgOper/Detail";
import UserLoginList from "../container/UserLogin";
import NewsOrgList from "../container/NewsOrg";
import NewsOrgDetail from "../container/NewsOrg/Detail";


class RouteMap extends Component {
  render() {
    return (
      <Router history={hashHistory}>
        {/*pc端*/}
        <Route path="/" component={PcApp}>
          <route path="/login" component={Login} />
          {/* 首页 */}
          <route path="/home" component={HomePage} />
          {/* 习题 */}
          <route path="/questionlist" component={Exercise} />
          <route path="/question/detail(/:id)" component={QuestionDetail} />
          <route path="/exerciselist" component={ExList} />
          <route path="/exercise/detail(/:id)" component={ExerciseDetail} />
          {/* 党员 */}
          <route path="/partymemberlist" component={PartyMemberList} />
          <route
            path="/partymember/detail(/:id)"
            component={PartyMemberDetail}
          />
          
          <route
            path="/partymember/setnext(/:id/:name)"
            component={SetNextPartyMember}
          />
          <route path="/partymember/import" component={ImportMember} />
          {/* 游客登录 */}
          <route path="/userloginlist" component={UserLoginList} />
          {/* 积分配置 */}
          <route path="/intergrallist" component={ScoreList} />
          <route path="/intergral/detail(/:id)" component={ScoreDetail} />
          {/* 商品 */}
          <route path="/goodslist" component={GoodsList} />
          <route path="/goods/detail(/:id)" component={GoodsDetail} />
          {/* 发货 */}
          <route path="/sendlist" component={SendList} />
          <route path="/send/detail(/:id)" component={SendDetail} />
          {/* 系统消息 */}
          <route path="/systemlist" component={SystemList} />
          <route path="/system/detail(/:id)" component={SystemDetail} />
          {/* 积分排行 */}
          <route path="/ranklist" component={RankList} />
          <route
            path="/rank/detail(/:id/:name/:score)"
            component={RankDetail}
          />
          {/* 操作员 */}
          <route path="/operatorlist" component={OperatorList} />
          <route path="/operator/detail(/:id)" component={OperatorDetail} />
          <route path="/orgoperatorlist" component={OrgOperator} />
          <route path="/orgoperator/detail(/:id)" component={OrgOperatorDetail} />
          {/* 组织管理 */}
          <route path="/orglist" component={OrgList} />
          <route path="/org/detail(/:id)" component={OrgDetail} />
          {/* 支部管理 */}
          <route path="/branchlist" component={BranchList} />
          <route path="/branch/detail(/:id)" component={BranchDetail} />
          {/* 滚动图片管理 */}
          <route path="/imagelist" component={ImageList} />
          <route path="/image/detail(/:id)" component={ImageDetail} />
          {/* 菜单设置 */}
          <route path="/menulist" component={MenuList} />
          <route path="/menuone/detail(/:id)" component={MenuDetailOne} />
          <route path="/menutwo/detail(/:id)" component={MenuDetailTwo} />
          {/* 菜单设置 */}
          <route path="/meetlist" component={MeetList} />
          <route path="/importmember(/:id/:name/:isDetail)" component={ImportMeetMember} />
          {/* <route path="/meet/detail(/:id)" component={MeetDetail} /> */}
          {/* 栏目信息 */}
          <route path="/programalist" component={ProgramaList} />
          <route path="/programa/detail(/:id)" component={ProgramaDetail} />
          {/* 活动信息 */}
          <route path="/activitylist" component={ActivityList} />
          <route path="/activity/detail(/:id)" component={ActivityDetail} />
          {/* 新闻信息 */}
          <route path="/newslist" component={NewsList} />
          <route path="/news/detail(/:id)" component={NewsDetail} />
          <route path="/news/import" component={ImportNews} />
          {/* 公开到组织的新闻 */}
          <route path="/newsorglist" component={NewsOrgList} />
          <route path="/newsorg/detail/:id" component={NewsOrgDetail} />
          
          {/* 权限信息 */}
          <route path="/permissionlist" component={PermissionList} />
          <route path="/permission/detail(/:id)" component={PermissionDetail} />
          <route path="/permission/manage" component={PermissionManage} />
          {/* 系统短信配置 */}
          <route path="/smssystemlist" component={SmsSystemList} />
          <route path="/smssystem/detail(/:id)" component={SmsSystemDetail} />
          {/* 组织短信配置 */}
          <route path="/smsorglist" component={SmsOrgList} />
          <route
            path="/smsorg/detail(/:org/:vendor)"
            component={SmsOrgDetail}
          />
          {/* 任务公开 */}
          <route
            path="/taskactivitypubliclist(/:type)"
            component={PublicTaskList}
          />
          <route
            path="/taskpublic/detail(/:id/:audit/:type)"
            component={PublicTaskDetail}
          />
          {/* 活动公开 */}
          <route
            path="/activitypubliclist(/:type)"
            component={PublicActivityList}
          />
          <route
            path="/activitypublic/detail(/:id/:audit/:type)"
            component={PublicActivityDetail}
          />
          {/* 用户文章 */}
          <route path="/articlelist(/:type)" component={PublicArticleList} />
          <route
            path="/article/detail(/:id/:audit/:type)"
            component={PublicArticleDetail}
          />
          <route path="/auditlist" component={AuditList}/>


          <route path="/partymemberreport" component={PartyMemberReport}/>
          <route path="/activityreport" component={ActivityReport}/>
          <route path="/taskreport" component={TaskReport}/>
          <route path="/articlereport" component={ArticleReport}/>
        </Route>
      </Router>
    );
  }
}

export default RouteMap;
