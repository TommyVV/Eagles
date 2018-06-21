import React, { Component } from 'react';
import { connect } from 'react-redux';
import './App.less';
import { Toast } from 'react-qtui';
import { loginHci } from "../../services/loginService";
import { saveUserInfo } from "../../actions/PC/userAction";
import Util from "../../utils/util";

/*
 * @class App `APP`组件
 */
@connect(
  state => {
    return {
      app: state.pcAppReducer
    }
  },
  { saveUserInfo }
)
class PcApp extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      initDone: false
    };
  }


  componentWillMount() {
    // this.login()
    this.setState({
      initDone: true
    });
  }

  componentDidMount() {
  }

  login = async () => {
    const param = Util.getParams();
    const paramToServer = {
      qtCode: param.qt_code,
      appId: param.appId
    }
    // const res = await login(paramToServer);

    // 6083390271bd4370bb7275eac8588dc0 我
    // 04fd1cd1de4b424d9a50540a3d93959c  家家
    // 19a296930f1a44d0a7f285903b944f94
    // 8G   0F0D1EF99E28471EB7CF920985F33F83
    // const res = await loginHci({ qtCode: '6083390271bd4370bb7275eac8588dc0', appId: '5718554488' });
    const res = await loginHci(paramToServer);

    this.props.saveUserInfo(res.userData)
    this.props.saveUserInfo({ appDataList: res.appDataList})
    Util.setLoaclStorage(res.token, res.refreshToken); // 保存token和refreshToken
    // 去掉正在加载的菊花
    this.setState({
      initDone: true
    });
  }

  entryIndex() {
    this.setState({
      initDone: true
    });
  }
  render() {
    return (
      <div>
        {this.state.initDone ? (
          this.props.children
        ) : (
            <Toast icon="loading" show={!this.state.initDone}>
              正在加载中
            </Toast>
          )}
        {
          this.props.app.show ? <div className='modal_disables'></div> : null
        }
      </div>
    );
  }
}

export default PcApp;
