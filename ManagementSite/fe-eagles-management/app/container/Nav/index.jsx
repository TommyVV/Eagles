import React from "react";
import { Layout, Menu, Avatar, Row, Col, Icon } from "antd";
import { connect } from "react-redux";
import { hashHistory } from "react-router";
import navMap from "../../constants/config/navMap";
import "./nav.less";

const { Sider, Content, Header } = Layout;
const MenuItemGroup = Menu.ItemGroup;
const SubMenu = Menu.SubMenu;
const pcLogo = require("../../../static/image/png/pcLogo.png");
const baixian = require("../../../static/image/png/baixian.png");
@connect(
  state => {
    return {
      user: state.userReducer
    };
  },
  null
)
export default class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      current: "",
      authMap: new Map()
    };
  }
  componentWillMount() {
    let { hash } = window.location;
    let index = hash.indexOf("?");
    const current = index > -1 ? hash.slice(0, index) : hash;
    const obj = navMap.find(item => current.indexOf(item.pathname) > -1);
    const auth = JSON.parse(localStorage.info).authList;
    if (obj && auth) {
      const authMap = new Map();
      auth.map((a, i) => {
        authMap.set(a.FunCode, a.FunCode);
      });
      this.setState({
        current: obj.key,
        sub: obj.sub,
        authMap
      });
    }
  }
  handleClick = e => {
    console.log("click ", e);
    this.setState({
      current: e.key,
      sub: ""
    });
  };
  render() {
    let { current, sub, authMap } = this.state;
    let Info = JSON.parse(localStorage.info);
    return (
      <Layout>
        <Sider
          className="pc_nav"
          style={{
            overflow: "auto",
            height: "100vh",
            position: "fixed",
            left: 0
          }}
        >
          <div
            className="nav__logo"
            onClick={() => hashHistory.replace("/home")}
            style={{ cursor: "pointer" }}
          >
            睿穗党建云
            <img src={baixian} alt="" className="baixian" />
          </div>

          <Menu
            theme="dark"
            style={{ minWidth: 224 }}
            defaultOpenKeys={[sub]}
            selectedKeys={[current]}
            mode="inline"
          >
            <SubMenu
              key="sub1"
              title={
                <span>
                  <Icon type="mail" />
                  <span>党员管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("User0001") ||
                    authMap.get("User0002") ||
                    authMap.get("User0004") ||
                    authMap.get("User0005")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub11"
                onClick={e => hashHistory.replace("/partymemberlist")}
                style={{ display: authMap.get("User0001") ? null : "none" }}
              >
                党员维护
              </Menu.Item>
              <Menu.Item
                key="sub13"
                onClick={e => hashHistory.replace("/partymember/import")}
                style={{ display: authMap.get("User0002") ? null : "none" }}
              >
                党员导入
              </Menu.Item>
              {/* <Menu.Item
                key="sub14"
                onClick={e => hashHistory.replace("/partymember/setnext")}
                style={{ display: authMap.get("User0003") ? null : "none" }}
              >
                党员上下级关系维护
              </Menu.Item> */}
              <Menu.Item
                key="sub15"
                onClick={e => hashHistory.replace("/ranklist")}
                style={{ display: authMap.get("User0004") ? null : "none" }}
              >
                积分排行
              </Menu.Item>
              <Menu.Item
                key="sub16"
                onClick={e => {
                  if (authMap.get("open0003")) {
                    hashHistory.replace(`/articlelist/2`);
                  } else if (authMap.get("open0006")) {
                    hashHistory.replace(`/articlelist/1`);
                  }
                }}
                style={{ display: authMap.get("User0005") ? null : "none" }}
              >
                用户文章维护
              </Menu.Item>
              <Menu.Item
                key="sub17"
                onClick={e => hashHistory.replace("/userloginlist")}
                style={{ display: authMap.get("User0006") ? null : "none" }}
              >
                游客登录列表
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub2"
              title={
                <span>
                  <Icon type="mail" />
                  <span>商品管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("Prod0001") || authMap.get("Prod0002")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub21"
                onClick={e => hashHistory.replace("/goodslist")}
                style={{ display: authMap.get("Prod0001") ? null : "none" }}
              >
                商品维护
              </Menu.Item>
              <Menu.Item
                key="sub23"
                onClick={e => hashHistory.replace("/sendlist")}
                style={{ display: authMap.get("Prod0002") ? null : "none" }}
              >
                商品发货维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub3"
              title={
                <span>
                  <Icon type="mail" />
                  <span>新闻管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("News0001") ||
                    authMap.get("News0002") ||
                    authMap.get("News0003") ||
                    authMap.get("News0004") ||
                    authMap.get("News0006")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub31"
                onClick={e => hashHistory.replace("/newslist")}
                style={{ display: authMap.get("News0001") ? null : "none" }}
              >
                新闻维护
              </Menu.Item>
              <Menu.Item
                key="sub33"
                onClick={e => hashHistory.replace("/news/import")}
                style={{ display: authMap.get("News0001") ? null : "none" }}
              >
                新闻导入
              </Menu.Item>
              <Menu.Item
                key="sub34"
                onClick={e => hashHistory.replace("/questionlist")}
                style={{ display: authMap.get("News0002") ? null : "none" }}
              >
                试卷维护
              </Menu.Item>
              <Menu.Item
                key="sub36"
                onClick={e => hashHistory.replace("/exerciselist")}
                style={{ display: authMap.get("News0003") ? null : "none" }}
              >
                习题维护
              </Menu.Item>
              <Menu.Item
                key="sub38"
                onClick={e => hashHistory.replace("/meetlist")}
                style={{ display: authMap.get("News0004") ? null : "none" }}
              >
                会议维护
              </Menu.Item>
              <Menu.Item
                key="sub310"
                onClick={e => hashHistory.replace("/intergrallist")}
                style={{ display: authMap.get("News0006") ? null : "none" }}
              >
                积分配置维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub4"
              title={
                <span>
                  <Icon type="mail" />
                  <span>组织管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("orgs0001") ||
                    authMap.get("orgs0002") ||
                    authMap.get("orgs0003")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub41"
                onClick={e => hashHistory.replace("/orglist")}
                style={{ display: authMap.get("orgs0001") ? null : "none" }}
              >
                组织维护
              </Menu.Item>
              <Menu.Item
                key="sub43"
                onClick={e => hashHistory.replace("/smsorglist")}
                style={{ display: authMap.get("orgs0002") ? null : "none" }}
              >
                组织短信维护
              </Menu.Item>
              <Menu.Item
                key="sub45"
                onClick={e => hashHistory.replace("/branchlist")}
                style={{ display: authMap.get("orgs0003") ? null : "none" }}
              >
                支部维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub5"
              title={
                <span>
                  <Icon type="mail" />
                  <span>应用管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("menu0001") ||
                    authMap.get("menu0002") ||
                    authMap.get("menu0003")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub51"
                onClick={e => hashHistory.replace("/menulist")}
                style={{ display: authMap.get("menu0001") ? null : "none" }}
              >
                菜单维护
              </Menu.Item>
              {/* <Menu.Item
                key="sub52"
                onClick={e => hashHistory.replace("/menuone/detail")}
                style={{ display: authMap.get("menu0001") ? null : "none" }}
              >
                一级菜单维护
              </Menu.Item>
              <Menu.Item
                key="sub53"
                onClick={e => hashHistory.replace("/menutwo/detail")}
                style={{ display: authMap.get("menu0001") ? null : "none" }}
              >
                二级菜单维护
              </Menu.Item> */}
              <Menu.Item
                key="sub54"
                onClick={e => hashHistory.replace("/imagelist")}
                style={{ display: authMap.get("menu0002") ? null : "none" }}
              >
                滚动图片维护
              </Menu.Item>
              <Menu.Item
                key="sub56"
                onClick={e => hashHistory.replace("/programalist")}
                style={{ display: authMap.get("menu0003") ? null : "none" }}
              >
                栏目维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub6"
              title={
                <span>
                  <Icon type="mail" />
                  <span>活动管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("actv0001") || authMap.get("actv0002")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub61"
                onClick={e => hashHistory.replace("/activitylist")}
                style={{ display: authMap.get("actv0001") ? null : "none" }}
              >
                活动维护
              </Menu.Item>
              <Menu.Item
                key="sub63"
                onClick={e => {
                  if (authMap.get("open0002")) {
                    hashHistory.replace(`/activitypubliclist/2`);
                  } else if (authMap.get("open0005")) {
                    hashHistory.replace(`/activitypubliclist/1`);
                  }
                }}
                style={{ display: authMap.get("actv0002") ? null : "none" }}
              >
                活动公开维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub7"
              title={
                <span>
                  <Icon type="mail" />
                  <span>任务管理</span>
                </span>
              }
              style={{ display: authMap.get("task0002") ? null : "none" }}
            >
              <Menu.Item
                key="sub71"
                onClick={e => {
                  if (authMap.get("open0001")) {
                    hashHistory.replace(`/taskactivitypubliclist/2`);
                  } else if (authMap.get("open0004")) {
                    hashHistory.replace(`/taskactivitypubliclist/1`);
                  }
                }}
                style={{ display: authMap.get("task0002") ? null : "none" }}
              >
                任务公开维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub8"
              title={
                <span>
                  <Icon type="mail" />
                  <span>系统管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("snew0001") || authMap.get("ssms0001")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub81"
                onClick={e => hashHistory.replace("/smssystemlist")}
                style={{ display: authMap.get("ssms0001") ? null : "none" }}
              >
                短信维护
              </Menu.Item>
              <Menu.Item
                key="sub83"
                onClick={e => hashHistory.replace("/systemlist")}
                style={{ display: authMap.get("snew0001") ? null : "none" }}
              >
                系统信息维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub9"
              title={
                <span>
                  <Icon type="mail" />
                  <span>操作员管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("oper0001") ||                  
                    authMap.get("oper0002") ||
                    authMap.get("oper0003")||
                    authMap.get("oper0004")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="sub91"
                onClick={e => hashHistory.replace("/operatorlist")}
                style={{ display: authMap.get("oper0001") ? null : "none" }}
              >
                支部操作员维护
              </Menu.Item>
              <Menu.Item
                key="sub92"
                onClick={e => hashHistory.replace("/orgoperatorlist")}
                style={{ display: authMap.get("oper0004") ? null : "none" }}
              >
                组织操作员维护
              </Menu.Item>
              <Menu.Item
                key="sub93"
                onClick={e => hashHistory.replace("/permissionlist")}
                style={{ display: authMap.get("oper0002") ? null : "none" }}
              >
                权限组维护
              </Menu.Item>
              <Menu.Item
                key="sub95"
                onClick={e => hashHistory.replace("/permission/manage")}
                style={{ display: authMap.get("oper0003") ? null : "none" }}
              >
                权限管理
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub10"
              title={
                <span>
                  <Icon type="mail" />
                  <span>审核流水</span>
                </span>
              }
              style={{
                display: authMap.get("Audit002") ? null : "none"
              }}
            >
              <Menu.Item
                key="sub101"
                onClick={e => hashHistory.replace("/auditlist")}
              // style={{ display: authMap.get("Audit001") ? null : "none" }}
              >
                审核流水记录
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub11"
              title={
                <span>
                  <Icon type="mail" />
                  <span>数据统计</span>
                </span>
              }
              style={{
                display: authMap.get("rept001") || authMap.get("rept002") || authMap.get("rept003") || authMap.get("rept004") ? null : "none"
              }}
            >
              <Menu.Item
                key="sub111"
                onClick={e => hashHistory.replace("/partymemberreport")}
              style={{ display: authMap.get("rept001") ? null : "none" }}
              >
                党员报表
              </Menu.Item>
              <Menu.Item
                key="sub112"
                onClick={e => hashHistory.replace("/activityreport")}
              style={{ display: authMap.get("rept001") ? null : "none" }}
              >
                活动报表
              </Menu.Item>
              <Menu.Item
                key="sub113"
                onClick={e => hashHistory.replace("/taskreport")}
              style={{ display: authMap.get("rept001") ? null : "none" }}
              >
                任务报表
              </Menu.Item>
              <Menu.Item
                key="sub114"
                onClick={e => hashHistory.replace("/articlereport")}
              style={{ display: authMap.get("rept001") ? null : "none" }}
              >
                文章统计报表
              </Menu.Item>
            </SubMenu>
          </Menu>
        </Sider>
        <Layout style={{ marginLeft: 260, minHeight: "100vh" }}>
          <Header style={{ background: "#fff", padding: 0 }}>
            <div className="header">
              <span className="header-username">
                {Info.Account}
                ，欢迎您。
              </span>
              <span
                className="header-back"
                onClick={() => hashHistory.replace("/login")}
              >
                退出
              </span>
            </div>
          </Header>
          <Content
            style={{
              margin: "16px 16px 16px 0",
              padding: 24,
              background: "#fff"
            }}
          >
            {this.props.children}
          </Content>
        </Layout>
      </Layout>
    );
  }
}
