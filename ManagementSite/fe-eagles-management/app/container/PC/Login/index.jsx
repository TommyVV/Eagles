import React from "react";
import { Form, Icon, Input, Button } from "antd";
import {  hashHistory } from 'react-router';
import "./login.less";

const FormItem = Form.Item;
class LoginForm extends React.Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      errMsg: "",
      loading: false
    };
  }
  handleSubmit = e => {
    e.preventDefault();
    let { getFieldValue, setFields, getFieldsValue } = this.props.form;
    this.props.form.validateFields((err, values) => {
      if (!err) {
        let mobile = getFieldValue("mobile").substr(0, 11);
        let password = getFieldValue("password");
		console.log(mobile, password);
		hashHistory.replace('/home');
      }
    });
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { errMsg } = this.state;
    return (
      <div className="login-wrap">
        <div className="login-panel">
          <h1>轻推运营平台</h1>
          <div className="login-title">登录</div>
          <div className={errMsg ? "err-msg-wrap has-error" : "err-msg-wrap"}>
            <p className="err-msg">{errMsg}</p>
          </div>
          <Form onSubmit={this.handleSubmit} className="login-form">
            <FormItem
              // label="手机号"
              colon={false}
            >
              {getFieldDecorator("mobile", {
                rules: [
                  {
                    required: true,
                    pattern: /^1(3|4|5|7|8)\d{9}$/,
                    message: "手机号格式不正确!"
                  }
                ]
              })(<Input placeholder="输入帐号" />)}
            </FormItem>
            <FormItem colon={false}>
              {getFieldDecorator("password", {
                rules: [
                  {
                    required: true,
                    message: "请输入密码"
                    //pattern: /^(?![\d]+$)(?![a-zA-Z]+$)(?![^\da-zA-Z]+$).{6,20}$/,
                    //message: '请输入密码，至少包含字母数字特殊字符的2种，长度不少于6位!'
                  }
                ]
              })(<Input type="password" placeholder="确认密码" />)}
            </FormItem>
            <FormItem colon={false}>
              <Button
                type="primary"
                htmlType="submit"
                loading={this.state.loading}
                className="login-form-button"
              >
                确定
              </Button>
            </FormItem>
          </Form>
        </div>
        <div className="copyright">
          <p>中冶赛迪重庆信息技术有限公司 版权所有</p>
          <p>Copyright © 2014-2018 CISDI Info. All Rights Reserved.</p>
        </div>
      </div>
    );
  }
}

const Login = Form.create()(LoginForm);
export default Login;
