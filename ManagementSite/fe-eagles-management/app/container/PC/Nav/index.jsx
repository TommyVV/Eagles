import React from "react";
import { Layout, Menu, Avatar, Row, Col, Icon } from "antd";
import { connect } from "react-redux";
import { hashHistory } from "react-router";
import navMap from "./navMap.js";
import FzSearch from "../../../components/PC/FzSearch";
import "./nav.less";

const { Sider, Content, Header } = Layout;
const MenuItemGroup = Menu.ItemGroup;
const SubMenu = Menu.SubMenu;
const pcLogo = require("../../../../static/image/png/pcLogo.png");
const baixian = require("../../../../static/image/png/baixian.png");
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
      current: ""
    };
  }
  componentWillMount() {
    let { hash } = window.location;
    let index = hash.indexOf("?");
    const current = hash.slice(0, index);
    const obj = navMap.find(item => current.indexOf(item.pathname) > -1);
    if (obj) {
      this.setState({
        current: obj.key,
        sub: obj.sub
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
    const { fetchList, search } = this.props;
    let { current, sub } = this.state;
    console.log(this.state.current);
    return (
      <Layout>
        <Sider className="pc_nav" style={{ overflow: 'auto', height: '100vh', position: 'fixed', left: 0 }}>
          <div className="nav__logo">
            睿穗党建云
            <img src={baixian} alt="" className="baixian" />
          </div>
          <Menu
            theme="dark"
            // onClick={this.handleClick}
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
              key="sub2"
              title={
                <span>
                  <Icon type="appstore" />
                  <span>党员信息</span>
                </span>
              }
            >
              <Menu.Item
                key="3"
                onClick={e => hashHistory.replace("/partymemberlist")}
              >
                党员列表
              </Menu.Item>
              <Menu.Item
                key="4"
                onClick={e => hashHistory.replace("/partymember/detail")}
              >
                党员详情
              </Menu.Item>
              <Menu.Item
                key="5"
                onClick={e => hashHistory.replace("/partymember/import")}
              >
                党员导入
              </Menu.Item>
            </SubMenu>
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
            <SubMenu
              key="sub4"
              title={
                <span>
                  <Icon type="setting" />
                  <span>商品信息</span>
                </span>
              }
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
            >
              <Menu.Item
                key="10"
                onClick={e => hashHistory.replace("/sendlist")}
              >
                商品发货列表
              </Menu.Item>
              <Menu.Item
                key="11"
                onClick={e => hashHistory.replace("/send/detail")}
              >
                商品发货详情
              </Menu.Item>
            </SubMenu>
            <SubMenu
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
            </SubMenu>
            <SubMenu
              key="sub7"
              title={
                <span>
                  <Icon type="setting" />
                  <span>系统信息</span>
                </span>
              }
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
            >
              <Menu.Item
                key="15"
                onClick={e => hashHistory.replace("/ranklist")}
              >
                积分排行列表
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
            </SubMenu>
            <SubMenu
              key="sub11"
              title={
                <span>
                  <Icon type="setting" />
                  <span>滚动图片信息</span>
                </span>
              }
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
            >
              <Menu.Item
                key="25"
                onClick={e => hashHistory.replace("/meetlist")}
              >
                会议列表
              </Menu.Item>
              <Menu.Item
                key="26"
                onClick={e => hashHistory.replace("/meet/detail")}
              >
                会议信息
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
                onClick={() => hashHistory.goBack()}
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
