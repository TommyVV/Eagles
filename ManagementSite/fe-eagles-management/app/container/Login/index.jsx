import React from "react";
import { Form, Icon, Input, Button } from "antd";
import { hashHistory } from "react-router";
import { login } from "../../services/loginService";
import md5 from "blueimp-md5";
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
    this.props.form.validateFields((err, values) => {
      if (!err) {
        this.submitLogin(values);
      }
    });
  };
  //发送请求
  submitLogin = async values => {
    this.setState({ loading: true });
    const { Result, Code, Message } = await login({
      Account: values.Account,
      Password: md5(values.Password)
    });
    this.setState({ loading: false });
    if (Code == "00") {
      //保存用户相关信息
      let { Token, IsVerificationCode } = Result;
      // 密码错误次数超限，出现验证码
      if (IsVerificationCode) {
      } else {
        // todo 把用户信息这两个字段写死
        let info = {
          Token,
          hasLogin: true,
          OrgId: "0",
          BranchId: "0"
        };
        localStorage.info = JSON.stringify({
          ...info
        });
        hashHistory.replace("/home");
      }
    } else {
      this.setState({ errMsg: Message });
    }
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { errMsg } = this.state;
    return (
      <div className="login-wrap">
        <div className="login-panel">
          <h1>睿穗党建云</h1>
          <div className="login-title">登录</div>
          <div className={errMsg ? "err-msg-wrap has-error" : "err-msg-wrap"}>
            <p className="err-msg">{errMsg}</p>
          </div>
          <Form onSubmit={this.handleSubmit} className="login-form">
            <FormItem
              // label="手机号"
              colon={false}
            >
              {getFieldDecorator("Account", {
                rules: [
                  {
                    required: true,
                    message: "请输入帐号"
                  }
                ]
              })(<Input placeholder="请输入帐号" />)}
            </FormItem>
            <FormItem colon={false}>
              {getFieldDecorator("Password", {
                rules: [
                  {
                    required: true,
                    message: "请输入密码"
                    //pattern: /^(?![\d]+$)(?![a-zA-Z]+$)(?![^\da-zA-Z]+$).{6,20}$/,
                    // message: '请输入密码，至少包含字母数字特殊字符的2种，长度不少于6位!'
                  }
                ]
              })(<Input type="password" placeholder="请输入密码" />)}
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
          {/* <p>中冶赛迪重庆信息技术有限公司 版权所有</p>
          <p>Copyright © 2014-2018 CISDI Info. All Rights Reserved.</p> */}
        </div>
      </div>
    );
  }
}

const Login = Form.create()(LoginForm);
export default Login;
