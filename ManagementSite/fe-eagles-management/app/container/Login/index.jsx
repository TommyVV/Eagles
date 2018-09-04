import React from "react";
import { Form, Icon, Input, Button } from "antd";
import { hashHistory } from "react-router";
import { login, getAuth } from "../../services/loginService";
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
      // Password: md5(values.Password)
      Password: values.Password,
      OrgId:values.OrgId
    });
    if (Code == "00") {
      //保存用户相关信息
      let { Token, IsVerificationCode } = Result;
      // 密码错误次数超限，出现验证码 todo
      if (IsVerificationCode) {
      } else {
        let info = {
          Token,
          Account: values.Account
        };
        let Info = localStorage.info ? JSON.parse(localStorage.info) : {}; // 先去取上次返回的url
        localStorage.clear(); // 再清空缓存
        localStorage.info = JSON.stringify({ ...info }); // 再设置当前的登录信息，方便拿权限
        // 拿权限
        const { Result } = await getAuth();
        const { List } = Result;
        let { returnUrl } = Info;
        this.setState({ loading: false });
        if (returnUrl && returnUrl.indexOf("login") == -1) {
          localStorage.info = JSON.stringify({ ...info, authList: List });
          hashHistory.replace(returnUrl);
        } else {
          localStorage.info = JSON.stringify({ ...info, authList: List });
          hashHistory.replace("/home");
        }
      }
    } else {
      this.setState({ errMsg: Message, loading: false });
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
              {getFieldDecorator("OrgId", {
                rules: [
                  {
                    required: true,
                    message: "请输入组织号"
                  }
                ]
              })(<Input placeholder="请输入组织号" />)}
            </FormItem>
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
          <p>广州睿穗科技有限公司 版权所有</p>
          <p>Copyright © 2014-2018 CISDI Info. All Rights Reserved.</p>
        </div>
      </div>
    );
  }
}

const Login = Form.create()(LoginForm);
export default Login;
