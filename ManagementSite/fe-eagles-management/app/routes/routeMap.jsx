import React, { Component } from "react";
import { Route, IndexRoute, hashHistory, Router } from "react-router";

// -------------------------------------------------  pc端路由  -------------------------------------------------
import PcApp from "../container/PC/App";
import Login from "../container/PC/Login/";

import Exercise from "../container/PC/Exercise/"; // 习题问卷列表
import ExerciseDetail from "../container/PC/Exercise/ExDetail"; // 习题问卷详情
import QuestionDetail from "../container/PC/Exercise/QuDetail"; // 习题详情
import HomePage from "../container/PC/Home";
import PartyMemberList from "../container/PC/PartyMember";
import PartyMemberDetail from "../container/PC/PartyMember/Detail";
import SetNextPartyMember from "../container/PC/PartyMember/Next";
import ImportMember from "../container/PC/PartyMember/ImportMember";
import IntergralList from "../container/PC/Intergral";
import IntergralDetail from "../container/PC/Intergral/Detail";
import GoodsList from "../container/PC/Goods";
import GoodsDetail from "../container/PC/Goods/Detail";
import SendList from "../container/PC/Send";
import SendDetail from "../container/PC/Send/Detail";
import CheckList from "../container/PC/Check";
import SystemList from "../container/PC/System";
import SystemDetail from "../container/PC/System/Detail";
import RankList from "../container/PC/Rank";
import RankDetail from "../container/PC/Rank/Detail";
import OperatorList from "../container/PC/Operator";
import OperatorDetail from "../container/PC/Operator/Detail";
import OrgList from "../container/PC/Org";
import OrgDetail from "../container/PC/Org/Detail";
import ImageList from "../container/PC/Image";
import ImageDetail from "../container/PC/Image/Detail";
import MenuList from "../container/PC/Menu";
import MenuDetailOne from "../container/PC/Menu/DetailOne";
import MenuDetailTwo from "../container/PC/Menu/DetailTwo";
import MeetList from "../container/PC/Meet";
import MeetDetail from "../container/PC/Meet/Detail";
import ImportMeetMember from "../container/PC/Meet/ImportMeetMember";
import ProgramaList from "../container/PC/Programa";
import ProgramaDetail from "../container/PC/Programa/Detail";
import ActivityList from "../container/PC/Activity";
import ActivityDetail from "../container/PC/Activity/Detail";
import NewsList from "../container/PC/News";
import NewsDetail from "../container/PC/News/Detail";
import PermissionList from "../container/PC/Permission";
import PermissionDetail from "../container/PC/Permission/Detail";
import PermissionManage from "../container/PC/Permission/Manage";

class RouteMap extends Component {
  render() {
    return (
      <Router history={hashHistory}>
        {/*pc端*/}
        <Route path="/" component={PcApp}>
          <IndexRoute component={Login} />
          {/* 首页 */}
          <route path="/home" component={HomePage} />
          {/* 习题 */}
          <route path="/questionlist" component={Exercise} />
          <route path="/question/detail(/:id)" component={QuestionDetail} />
          <route path="/exercise/detail(/:id)" component={ExerciseDetail} />
          {/* 党员 */}
          <route path="/partymemberlist" component={PartyMemberList} />
          <route
            path="/partymember/detail(/:id)"
            component={PartyMemberDetail}
          />
          <route
            path="/partymember/setnext/:id/:name"
            component={SetNextPartyMember}
          />
          <route path="/partymember/import" component={ImportMember} />
          {/* 积分配置 */}
          <route path="/intergrallist" component={IntergralList} />
          <route path="/intergral/detail(/:id)" component={IntergralDetail} />
          {/* 商品 */}
          <route path="/goodslist" component={GoodsList} />
          <route path="/goods/detail(/:id)" component={GoodsDetail} />
          {/* 发货 */}
          <route path="/sendlist" component={SendList} />
          <route path="/send/detail(/:id)" component={SendDetail} />
          {/* 审核 */}
          <route path="/checklist" component={CheckList} />
          {/* 系统消息 */}
          <route path="/systemlist" component={SystemList} />
          <route path="/system/detail(/:id)" component={SystemDetail} />
          {/* 积分排行 */}
          <route path="/ranklist" component={RankList} />
          <route path="/rank/detail(/:id)" component={RankDetail} />
          {/* 操作员 */}
          <route path="/operatorlist" component={OperatorList} />
          <route path="/operator/detail(/:id)" component={OperatorDetail} />
          {/* 机构管理 */}
          <route path="/orglist" component={OrgList} />
          <route path="/org/detail(/:id)" component={OrgDetail} />
          {/* 滚动图片管理 */}
          <route path="/imagelist" component={ImageList} />
          <route path="/image/detail(/:id)" component={ImageDetail} />
          {/* 菜单设置 */}
          <route path="/menulist" component={MenuList} />
          <route path="/menuone/detail(/:id)" component={MenuDetailOne} />
          <route path="/menutwo/detail(/:id)" component={MenuDetailTwo} />
          {/* 菜单设置 */}
          <route path="/meetlist" component={MeetList} />
          <route path="/importmember/:id" component={ImportMeetMember} />
          <route path="/meet/detail(/:id)" component={MeetDetail} />
          {/* 栏目信息 */}
          <route path="/programalist" component={ProgramaList} />
          <route path="/programa/detail(/:id)" component={ProgramaDetail} />
          {/* 活动信息 */}
          <route path="/activitylist" component={ActivityList} />
          <route path="/activity/detail(/:id)" component={ActivityDetail} />
          {/* 新闻信息 */}
          <route path="/newslist" component={NewsList} />
          <route path="/news/detail(/:id)" component={NewsDetail} />
          {/* 权限信息 */}
          <route path="/permissionlist" component={PermissionList} />
          <route path="/permission/detail(/:id)" component={PermissionDetail} />
          <route path="/permission/manage" component={PermissionManage} />
        </Route>
      </Router>
    );
  }
}

export default RouteMap;
