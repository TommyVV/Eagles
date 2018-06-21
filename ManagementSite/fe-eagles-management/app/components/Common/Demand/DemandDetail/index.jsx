import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import { Article } from 'react-qtui';
import { List, Flex } from 'antd-mobile';
import util from "../../../../utils/util";
import { getUserInfoByPhone } from "../../../../services/loginService";
import qt from '../../../../../static/lib/qingtui_jssdk-2.1';
import './style.less';

const Item = List.Item;

class DemandDetailContent extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      isQtUser: false,
      openId: ''
    };
  }
  componentWillMount() {
    window.scrollTo(0, 0)
    const { detail } = this.props;
  }
  componentWillReceiveProps(pre, next) {
    if (pre.isLoaded && pre.detail.mobilePhone) {
      this.getUserInfoFn(pre.detail.mobilePhone);
    }
  }
  getUserInfoFn = async (phone) => {
    try {
      const res = await getUserInfoByPhone({ mobile: phone });
      this.setState({
        isQtUser: true,
        openId: res.data.openId
      });
    } catch (e) {
      console.log(e)
    }
  }
  jumpToDetail = () => {
    const { op, detail } = this.props;
    // 不是运营的分享详情页面，才可以点击发布者的详情
    if (!op) {
      hashHistory.push(`hisdetail/${detail.creatorId}`);
    }
  }
  openChat = (openId) => {
    qt.openUserDetail({
      id: openId
    });
  }
  render() {
    const { detail } = this.props;
    const { isQtUser, openId } = this.state;
    return (
      <Article>
        <h1 className='demand__title'>{detail.name}</h1>
        <div className='demand__author'>
          <img src={detail.creatorAvatar} alt="" className='demand__author--avatar' />
          <span className='demand__author--name' onClick={() => this.jumpToDetail()}>{detail.creatorName}</span>
          <span className='demand__author--time'> {util.timeStampConvent(detail.updateTime)}</span>
          {this.props.children}
        </div>
        <div className='demand__detail'>
          <Flex>
            <Flex.Item className='demand__item--title'>
              发布公司:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {detail.company}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              项目地点:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {detail.address}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              项目周期:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {detail.period}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              项目规模:
            </Flex.Item>
            <Flex.Item className='demand__item--content' dangerouslySetInnerHTML={{ __html: detail.scope && detail.scope.replace(/\n|\r\n/g, '<br />') }}>
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              质量标准:
            </Flex.Item>
            <Flex.Item className='demand__item--content' dangerouslySetInnerHTML={{ __html: detail.standard ? detail.standard.replace(/\n|\r\n/g, '<br />') : '暂无质量标准' }}>
              {/* {detail.standard ? detail.standard : '暂无质量标准' } */}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              有效时间:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {detail.effectiveTime}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              资金来源:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {detail.sourcesFunds ? detail.sourcesFunds : '暂无项目资金来源'}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              资格要求:
            </Flex.Item>
            <Flex.Item className='demand__item--content' dangerouslySetInnerHTML={{ __html: detail.serviceEligibility ? detail.serviceEligibility.replace(/\n|\r\n/g, '<br />') : '暂无服务方资格要求' }}>
              {/* {detail.serviceEligibility ? detail.serviceEligibility : '暂无服务方资格要求'} */}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              项目描述:
            </Flex.Item>
            <Flex.Item className='demand__item--content' dangerouslySetInnerHTML={{ __html: detail.description ? detail.description.replace(/\n|\r\n/g, '<br />') : '暂无项目描述' }}>
              {/* {detail.description ? detail.description : '暂无项目描述'} */}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              联系人:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {detail.contacts}
            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              移动电话:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {
                isQtUser ?
                  <span className='qt-user' onClick={() => this.openChat(openId)}>
                    {detail.mobilePhone}
                  </span> :
                  <span >
                    {detail.mobilePhone ? detail.mobilePhone : '暂无移动电话'}
                  </span>
              }

            </Flex.Item>
          </Flex>
          <Flex>
            <Flex.Item className='demand__item--title'>
              办公电话:
            </Flex.Item>
            <Flex.Item className='demand__item--content'>
              {detail.officePhone ? detail.officePhone : '暂无办公电话'}
            </Flex.Item>
          </Flex>
        </div>
      </Article>
    );
  }
}
DemandDetailContent.defaultProps = {
  detail: {}
}
export default DemandDetailContent;