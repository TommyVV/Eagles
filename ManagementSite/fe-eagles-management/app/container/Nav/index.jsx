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
    const current = hash.slice(0, index);
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
          <div className="nav__logo">
            睿穗党建云
            <img src={baixian} alt="" className="baixian" />
          </div>
          <Menu
            theme="dark" // onClick={this.handleClick}
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
                  <span>习题问卷</span>
                </span>
              }
              style={{ display: authMap.get("News0002") ? null : "none" }}
            >
              <Menu.Item
                key="1"
                onClick={e => hashHistory.replace("/questionlist")}
              >
                习题问卷列表
              </Menu.Item>
              <Menu.Item
                key="2"
                onClick={e => hashHistory.replace("/question/detail")}
              >
                习题问卷详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sube1"
              title={
                <span>
                  <Icon type="mail" />
                  <span>习题信息</span>
                </span>
              }
              style={{ display: authMap.get("News0003") ? null : "none" }}
            >
              <Menu.Item
                key="e1"
                onClick={e => hashHistory.replace("/exerciselist")}
              >
                习题列表
              </Menu.Item>
              <Menu.Item
                key="e2"
                onClick={e => hashHistory.replace("/exercise/detail")}
              >
                习题详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub2"
              title={
                <span>
                  <Icon type="appstore" />
                  <span>党员信息</span>
                </span>
              }
              style={{
                display:
                  authMap.get("User0001") || authMap.get("User0002")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="3"
                onClick={e => hashHistory.replace("/partymemberlist")}
                style={{ display: authMap.get("User0001") ? null : "none" }}
              >
                党员列表
              </Menu.Item>
              <Menu.Item
                key="4"
                onClick={e => hashHistory.replace("/partymember/detail")}
                style={{ display: authMap.get("User0001") ? null : "none" }}
              >
                党员详情
              </Menu.Item>
              <Menu.Item
                key="5"
                onClick={e => hashHistory.replace("/partymember/import")}
                style={{ display: authMap.get("User0002") ? null : "none" }}
              >
                党员导入
              </Menu.Item>
            </SubMenu>
            {/* {authMap.get("User0004") ? ( */}
            <SubMenu
              key="sub3"
              title={
                <span>
                  <Icon type="setting" />
                  <span>积分信息</span>
                </span>
              }
            >
              <Menu.Item
                key="6"
                onClick={e => hashHistory.replace("/intergrallist")}
              >
                积分配置列表
              </Menu.Item>
              <Menu.Item
                key="7"
                onClick={e => hashHistory.replace("/intergral/detail")}
              >
                积分配置详情
              </Menu.Item>
            </SubMenu>
            {/* ) : null} */}
            <SubMenu
              key="sub4"
              title={
                <span>
                  <Icon type="setting" />
                  <span>商品信息</span>
                </span>
              }
              style={{ display: authMap.get("Prod0001") ? null : "none" }}
            >
              <Menu.Item
                key="8"
                onClick={e => hashHistory.replace("/goodslist")}
              >
                商品列表
              </Menu.Item>
              <Menu.Item
                key="9"
                onClick={e => hashHistory.replace("/goods/detail")}
              >
                商品详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub5"
              title={
                <span>
                  <Icon type="setting" />
                  <span>商品发货信息</span>
                </span>
              }
              style={{ display: authMap.get("Prod0002") ? null : "none" }}
            >
              <Menu.Item
                key="10"
                onClick={e => hashHistory.replace("/sendlist")}
              >
                商品发货列表
              </Menu.Item>
              {/* <Menu.Item
                key="11"
                onClick={e => hashHistory.replace("/send/detail")}
              >
                商品发货详情
              </Menu.Item> */}
            </SubMenu>

            {/* <SubMenu
              key="sub6"
              title={
                <span>
                  <Icon type="setting" />
                  <span>审核信息</span>
                </span>
              }
            >
              <Menu.Item
                key="12"
                onClick={e => hashHistory.replace("/checklist")}
              >
                审核列表
              </Menu.Item>
            </SubMenu> */}

            <SubMenu
              key="sub7"
              title={
                <span>
                  <Icon type="setting" />
                  <span>系统信息</span>
                </span>
              }
              style={{ display: authMap.get("snew0001") ? null : "none" }}
            >
              <Menu.Item
                key="13"
                onClick={e => hashHistory.replace("/systemlist")}
              >
                系统信息列表
              </Menu.Item>
              <Menu.Item
                key="14"
                onClick={e => hashHistory.replace("/system/detail")}
              >
                系统信息详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub8"
              title={
                <span>
                  <Icon type="setting" />
                  <span>积分排行信息</span>
                </span>
              }
              style={{ display: authMap.get("User0004") ? null : "none" }}
            >
              <Menu.Item
                key="15"
                onClick={e => hashHistory.replace("/ranklist")}
              >
                积分排行列表
              </Menu.Item>
              <Menu.Item
                key="155"
                onClick={e => hashHistory.replace("/rank/detail")}
              >
                积分排行详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub9"
              title={
                <span>
                  <Icon type="setting" />
                  <span>操作员信息</span>
                </span>
              }
              style={{ display: authMap.get("oper0001") ? null : "none" }}
            >
              <Menu.Item
                key="16"
                onClick={e => hashHistory.replace("/operatorlist")}
              >
                操作员列表
              </Menu.Item>
              <Menu.Item
                key="17"
                onClick={e => hashHistory.replace("/operator/detail")}
              >
                操作员详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub10"
              title={
                <span>
                  <Icon type="setting" />
                  <span>机构信息</span>
                </span>
              }
              style={{ display: authMap.get("orgs0001") ? null : "none" }}
            >
              <Menu.Item
                key="18"
                onClick={e => hashHistory.replace("/orglist")}
              >
                机构列表
              </Menu.Item>
              <Menu.Item
                key="19"
                onClick={e => hashHistory.replace("/org/detail")}
              >
                机构详情
              </Menu.Item>
              <Menu.Item
                key="188"
                onClick={e => hashHistory.replace("/branchlist")}
              >
                支部列表
              </Menu.Item>
              <Menu.Item
                key="199"
                onClick={e => hashHistory.replace("/branch/detail")}
              >
                支部详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub11"
              title={
                <span>
                  <Icon type="setting" />
                  <span>滚动图片信息</span>
                </span>
              }
              style={{ display: authMap.get("menu0002") ? null : "none" }}
            >
              <Menu.Item
                key="20"
                onClick={e => hashHistory.replace("/imagelist")}
              >
                滚动图片列表
              </Menu.Item>
              <Menu.Item
                key="21"
                onClick={e => hashHistory.replace("/image/detail")}
              >
                滚动图片详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub12"
              title={
                <span>
                  <Icon type="setting" />
                  <span>菜单配置</span>
                </span>
              }
              style={{ display: authMap.get("menu0001") ? null : "none" }}
            >
              <Menu.Item
                key="22"
                onClick={e => hashHistory.replace("/menulist")}
              >
                菜单列表
              </Menu.Item>
              <Menu.Item
                key="23"
                onClick={e => hashHistory.replace("/menuone/detail")}
              >
                一级菜单维护
              </Menu.Item>
              <Menu.Item
                key="24"
                onClick={e => hashHistory.replace("/menutwo/detail")}
              >
                二级菜单维护
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub13"
              title={
                <span>
                  <Icon type="setting" />
                  <span>会议信息</span>
                </span>
              }
              style={{
                display:
                  authMap.get("News0004") || authMap.get("News0005")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="25"
                onClick={e => hashHistory.replace("/meetlist")}
                style={{ display: authMap.get("News0004") ? null : "none" }}
              >
                会议列表
              </Menu.Item>
              <Menu.Item
                key="26"
                onClick={e => hashHistory.replace("/meet/detail")}
                style={{ display: authMap.get("News0005") ? null : "none" }}
              >
                会议信息
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub14"
              title={
                <span>
                  <Icon type="setting" />
                  <span>栏目信息</span>
                </span>
              }
              style={{ display: authMap.get("menu0003") ? null : "none" }}
            >
              <Menu.Item
                key="27"
                onClick={e => hashHistory.replace("/programalist")}
              >
                栏目列表
              </Menu.Item>
              <Menu.Item
                key="28"
                onClick={e => hashHistory.replace("/programa/detail")}
              >
                栏目详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub15"
              title={
                <span>
                  <Icon type="setting" />
                  <span>活动信息</span>
                </span>
              }
              style={{ display: authMap.get("actv0001") ? null : "none" }}
            >
              <Menu.Item
                key="29"
                onClick={e => hashHistory.replace("/activitylist")}
              >
                活动列表
              </Menu.Item>
              <Menu.Item
                key="30"
                onClick={e => hashHistory.replace("/activity/detail")}
              >
                活动详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub16"
              title={
                <span>
                  <Icon type="setting" />
                  <span>新闻信息</span>
                </span>
              }
              style={{ display: authMap.get("News0001") ? null : "none" }}
            >
              <Menu.Item
                key="31"
                onClick={e => hashHistory.replace("/newslist")}
              >
                新闻列表
              </Menu.Item>
              <Menu.Item
                key="32"
                onClick={e => hashHistory.replace("/news/detail")}
              >
                新闻详情
              </Menu.Item>
              <Menu.Item
                key="321"
                onClick={e => hashHistory.replace("/news/import")}
              >
                新闻导入
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub17"
              title={
                <span>
                  <Icon type="setting" />
                  <span>权限管理</span>
                </span>
              }
              style={{
                display:
                  authMap.get("oper0002") || authMap.get("oper0003")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="33"
                onClick={e => hashHistory.replace("/permissionlist")}
                style={{ display: authMap.get("oper0002") ? null : "none" }}
              >
                权限组列表
              </Menu.Item>
              <Menu.Item
                key="34"
                onClick={e => hashHistory.replace("/permission/detail")}
                style={{ display: authMap.get("oper0002") ? null : "none" }}
              >
                权限组详情
              </Menu.Item>
              <Menu.Item
                key="35"
                onClick={e => hashHistory.replace("/permission/manage")}
                style={{ display: authMap.get("oper0003") ? null : "none" }}
              >
                权限管理
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub18"
              title={
                <span>
                  <Icon type="setting" />
                  <span>系统短信配置</span>
                </span>
              }
              style={{ display: authMap.get("ssms0001") ? null : "none" }}
            >
              <Menu.Item
                key="36"
                onClick={e => hashHistory.replace("/smssystemlist")}
              >
                短信配置列表
              </Menu.Item>
              <Menu.Item
                key="37"
                onClick={e => hashHistory.replace("/smssystem/detail")}
              >
                短信配置详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub19"
              title={
                <span>
                  <Icon type="setting" />
                  <span>机构短信配置</span>
                </span>
              }
              style={{ display: authMap.get("orgs0002") ? null : "none" }}
            >
              <Menu.Item
                key="38"
                onClick={e => hashHistory.replace("/smsorglist")}
              >
                机构配置列表
              </Menu.Item>
              <Menu.Item
                key="39"
                onClick={e => hashHistory.replace("/smsorg/detail")}
              >
                机构配置详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub20"
              title={
                <span>
                  <Icon type="setting" />
                  <span>任务公开信息</span>
                </span>
              }
              style={{
                display:
                  authMap.get("open0001") || authMap.get("open0004")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="40"
                onClick={e => {
                  if (authMap.get("open0001")) {
                    hashHistory.replace(`/taskactivitypubliclist/2`);
                  } else if (authMap.get("open0004")) {
                    hashHistory.replace(`/taskactivitypubliclist/1`);
                  }
                }}
              >
                任务公开列表
              </Menu.Item>
              <Menu.Item
                key="41"
                onClick={e => hashHistory.replace("/taskpublic/detail")}
              >
                任务公开详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub21"
              title={
                <span>
                  <Icon type="setting" />
                  <span>活动公开信息</span>
                </span>
              }
              style={{
                display:
                  authMap.get("open0002") || authMap.get("open0005")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="42"
                onClick={e => {
                  if (authMap.get("open0002")) {
                    hashHistory.replace(`/activitypubliclist/2`);
                  } else if (authMap.get("open0005")) {
                    hashHistory.replace(`/activitypubliclist/1`);
                  }
                }}
              >
                活动公开列表
              </Menu.Item>
              <Menu.Item
                key="43"
                onClick={e => hashHistory.replace("/activitypublic/detail")}
              >
                活动公开详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub22"
              title={
                <span>
                  <Icon type="setting" />
                  <span>用户文章信息</span>
                </span>
              }
              style={{
                display:
                  authMap.get("open0003") || authMap.get("open0006")
                    ? null
                    : "none"
              }}
            >
              <Menu.Item
                key="44"
                onClick={e => {
                  if (authMap.get("open0003")) {
                    hashHistory.replace(`/articlelist/2`);
                  } else if (authMap.get("open0006")) {
                    hashHistory.replace(`/articlelist/1`);
                  }
                }}
              >
                用户文章列表
              </Menu.Item>
              <Menu.Item
                key="45"
                onClick={e => hashHistory.replace("/article/detail")}
              >
                用户文章详情
              </Menu.Item>
            </SubMenu>
          </Menu>
        </Sider>
        <Layout style={{ marginLeft: 260, minHeight: "100vh" }}>
          <Header style={{ background: "#fff", padding: 0 }}>
            <div className="header">
              <Avatar src={this.props.user.avatar} className="header-avatar" />
              <span className="header-username">
                {this.props.user.userName}
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
