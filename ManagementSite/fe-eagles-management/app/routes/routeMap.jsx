import React, { Component } from "react";
import { Route, IndexRoute, hashHistory, Router } from "react-router";

// //  ---------------------  分享  ---------------------
import SharePublished from "../container/PC/Share/Published/"; // 已发布
// import ShareUnpublished from '../container/PC/Share/Unpublished/';  // 未发布
// import ShareAudit from '../container/PC/Share/Audit/';  // 审核中
// import ShareUnAudit from '../container/PC/Share/UnAudit/';  // 未审核
// import ShareAccount from '../container/PC/Share/Account/';  // 我的账户
// import Share from '../container/PC/Share/Share/';  // 分享发布
// //  ---------------------  咨询机构  ---------------------
// import Agency from '../container/PC/Agency';
// //  ---------------------  项目需求  ---------------------
// import DemandPublished from '../container/PC/Demand/Published/';  // 已发布
// import DemandUnpublished from '../container/PC/Demand/Unpublished/';  // 未发布
// import DemandAudit from '../container/PC/Demand/Audit/';  // 审核中
// import DemandUnAudit from '../container/PC/Demand/UnAudit/';  // 未审核
// import Demand from '../container/PC/Demand/Demand/'; // 需求发布
// import DemandClosed from '../container/PC/Demand/Closed/'; // 需求发布
//  ---------------------  项目管理  ---------------------

// import Detail from "../container/PC/Project/Detail";  // 项目详情
// import ExerciseCreate from "../container/PC/Exercise/ExerciseCreate";  // 新建项目
//  ---------------------  第三方页面  ---------------------

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
          {/*分享*/}
          {/* <route path="/sharemanage/published" component={SharePublished} /> */}
          {/* <route path="/sharemanage/published" component={SharePublished} />
          <route path="/sharemanage/unpublished" component={ShareUnpublished} />
          <route path="/sharemanage/audit" component={ShareAudit} />
          <route path="/sharemanage/unaudit" component={ShareUnAudit} />
          <route path="/sharemanage/account" component={ShareAccount} />
          <route path="/sharepublished" component={Share} />
          <route path="/sharepublished/:shareId" component={Share} /> */}
          {/* 咨询机构 */}
          {/* <route path="/agency" component={Agency} /> */}
          {/* 需求管理 */}
          {/* <route path="/demandmanage/published" component={DemandPublished} />
          <route path="/demandmanage/unpublished" component={DemandUnpublished} />
          <route path="/demandmanage/audit" component={DemandAudit} />
          <route path="/demandmanage/unaudit" component={DemandUnAudit} />
          <route path="/demandmanage/closed" component={DemandClosed} />
          <route path="/demand" component={Demand} />
          <route path="/demand/create/requireId" component={Create} />
          <route path="/demand/:requireId" component={Demand} /> */}
          {/* 项目管理 */}
          {/* <route path="/project" component={Project} /> */}
          {/* <route path="/project/detail/:projectId" component={Detail} />
          <route path="/project/create" component={Create} />
          <route path="/project/create/:projectId" component={Create} /> */}
          {/*第三方应用*/}
          {/* <route path="/other/:appId" component={OtherPage} /> */}
        </Route>
      </Router>
    );
  }
}

export default RouteMap;
