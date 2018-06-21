import React from "react";
import { Layout, Menu, Avatar, Row, Col } from "antd";
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
      key: 1
    };
  }
  componentWillMount() {
    // let { hash } = window.location;
    // let index = hash.indexOf("?");
    // hash = hash.slice(0, index);
    // let arr = hash.split("/");
    // let current = arr[1];
    // let key = navMap.find(item => item.pathname === current).key;
    // this.setState({ key });
  }
  render() {
    const { fetchList, search } = this.props;
    let { key } = this.state;
    return (
      <Layout>
        <Sider className="pc_nav">
          <div className="nav__logo">
            睿穗党建云
            <img src={baixian} alt="" className="baixian" />
          </div>
          <Menu
            theme="dark"
            mode="inline"
            defaultSelectedKeys={["1"]}
            defaultOpenKeys={["sub1"]}
          >
            <SubMenu
              key="sub1"
              title={
                <span>
                  <span>习题问卷</span>
                </span>
              }
            >
              <Menu.Item key="1" onClick={() => hashHistory.replace('/exercise')}>
                习题问卷列表
              </Menu.Item>
              <Menu.Item key="2" onClick={() => hashHistory.replace('/exercise/create')}>习题问卷详情</Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub2"
              title={
                <span>
                  <span>党员信息</span>
                </span>
              }
            >
              <Menu.Item key="3">
                党员列表
              </Menu.Item>
              <Menu.Item key="4">党员详情</Menu.Item>
            </SubMenu>
            {/* <MenuItemGroup key="g1" title="习题问卷">
              <Menu.Item key="1">
                <a onClick={() => hashHistory.replace('/sharemanage/published')}>
                  <i className="iconfont icon-fenxiang"></i>
                  习题问卷列表
                </a>
              </Menu.Item>
              <Menu.Item key="2">
                <a onClick={() => hashHistory.replace('/sharepublished')}>
                  <i className="iconfont icon-tianjia"></i>
                  习题问卷详情
                </a>
              </Menu.Item>
            </MenuItemGroup>
            <MenuItemGroup key="g2" title="党员信息">
            <Menu.Item key="3">
                <a onClick={() => hashHistory.replace('/sharemanage/published')}>
                  <i className="iconfont icon-fenxiang"></i>
                  党员列表
                </a>
              </Menu.Item>
              <Menu.Item key="4">
                <a onClick={() => hashHistory.replace('/agency')}>
                  <i className="iconfont icon-tianjia"></i>
                  党员详情
                </a>
              </Menu.Item>
            </MenuItemGroup>
            <MenuItemGroup key="g3" title="积分信息">
              <Menu.Item key="5">
                <a onClick={() => hashHistory.replace('/demandmanage/published')}>
                  <i className="iconfont icon-fabuxuqiu"></i>
                  积分配置
                </a>
              </Menu.Item>
              <Menu.Item key="6">
                <a onClick={() => hashHistory.replace('/demand')}>
                  <i className="iconfont icon-tianjia"></i>
                  积分详情
              </a>
              </Menu.Item>
            </MenuItemGroup>
            <MenuItemGroup key="g4" title="商品信息">
              <Menu.Item key="7">
                <a onClick={() => hashHistory.replace('/project')}>
                  <i className="iconfont icon-xiangmu"></i>
                  商品维护
                </a>
              </Menu.Item>
              <Menu.Item key="8">
                <a onClick={() => hashHistory.replace('/demand')}>
                  <i className="iconfont icon-tianjia"></i>
                  商品详情
              </a>
              </Menu.Item>
            </MenuItemGroup> */}
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
