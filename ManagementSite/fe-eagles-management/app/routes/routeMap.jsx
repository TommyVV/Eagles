import React, { Component } from 'react';
import { Route, IndexRoute, hashHistory, Router } from 'react-router';


import PcApp from '../container/PC/App';

// -------------------------------------------------  pc端路由  -------------------------------------------------
import Login from '../container/PC/Login/';

import Exercise from '../container/PC/Exercise/'; // 需求发布
import ExerciseCreate from "../container/PC/Exercise/Create";  // 新建项目
// //  ---------------------  分享  ---------------------
// import SharePublished from '../container/PC/Share/Published/';  // 已发布
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
import OtherPage from "../container/PC/Other";  // 新建项目


class RouteMap extends Component {
  render() {
    return (
      <Router history={hashHistory}>
        {/*pc端*/}
        <Route path="/" component={PcApp}>
          <route path="/login" component={Login} />

          {/* 习题 */}
          <route path="/exercise" component={Exercise} />
          <route path="/exercise/create(/:id)" component={ExerciseCreate} />
          

          {/*分享*/}
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