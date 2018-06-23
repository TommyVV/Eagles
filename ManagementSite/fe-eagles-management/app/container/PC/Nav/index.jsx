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
    this.setState({
      current: obj.key,
      sub: obj.sub
    });
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
        <Sider className="pc_nav">
          <div className="nav__logo">
            睿穗党建云
            <img src={baixian} alt="" className="baixian" />
          </div>
          <Menu
            theme="dark"
            // onClick={this.handleClick}
            style={{ width: 256 }}
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
              <Menu.Item key="5">党员导入</Menu.Item>
            </SubMenu>
            <SubMenu
              key="sub4"
              title={
                <span>
                  <Icon type="setting" />
                  <span>Navigation Three</span>
                </span>
              }
            >
              <Menu.Item key="9">Option 9</Menu.Item>
              <Menu.Item key="10">Option 10</Menu.Item>
              <Menu.Item key="11">Option 11</Menu.Item>
              <Menu.Item key="12">Option 12</Menu.Item>
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
