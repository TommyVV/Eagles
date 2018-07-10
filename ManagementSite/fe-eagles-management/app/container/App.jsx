import React, { Component } from "react";
import { connect } from "react-redux";
import "./App.less";
import { login } from "../services/loginService";
import { saveUserInfo } from "../actions/userAction";
import Util from "../utils/util";

/*
 * 初始化`APP`组件
 */

class PcApp extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      initDone: false
    };
  }

  componentWillMount() {
    this.login()
    this.setState({
      initDone: true
    });
  }

  login = async () => {
    // const res = await login();
    // Util.setLoaclStorage(res.Token); // 保存token和refreshToken
    // 去掉正在加载的菊花
    this.setState({
      initDone: true
    });
  };

  entryIndex() {
    this.setState({
      initDone: true
    });
  }
  render() {
    return <div>{this.state.initDone ? this.props.children : <div />}</div>;
  }
}

export default PcApp;
