import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import { connect } from 'react-redux';
import { Tab, NavBar, NavBarItem, Article } from 'react-qtui';
import { saveAgencyTab } from '../../../../actions/mobile/agencyAction';
import RenderImage from "../../RenderImage";
import QtImg from "../../RenderImage/QtImage";
import './style.less';

@connect(
  state => {
    return {
      tab: state.mobileAgencyReducer.tab
    }
  },
  { saveAgencyTab }
)
class Container extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tab: this.props.tab
    }
  }

  //切换tab
  switchTab(tab) {
    //保存tab
    this.props.saveAgencyTab(tab);
    this.setState({ tab });
  }

  render() {
    const { tab } = this.state;
    const { agency, op } = this.props;
    const imgList = agency.serviceContentImgList && [...agency.serviceContentImgList, ...agency.capacityProtectionImgList, ...agency.outstandingPerformanceImgList, ...agency.typicalCaseImgList, ...agency.otherInformationImgList] || [];
    const srcs = [];
    imgList.forEach(img => srcs.push(img.fileUrl));
    console.log('agency - srcs ', srcs)
    return (
      <div>
        {!op ? <Article >
          <Tab className='agency__tab'>
            <NavBar>
              <NavBarItem active={tab === 0} onClick={this.switchTab.bind(this, 0)}>
                机构介绍
            </NavBarItem>
              <NavBarItem active={tab === 1} onClick={this.switchTab.bind(this, 1)}>
                知识分享
            </NavBarItem>
            </NavBar>
          </Tab>
        </Article> : null}
        {
          tab === 0 ?
            <Article className={op ? null : "agency-info-wrapper"}>
              <div className='agency__item'>
                <p className='agency__item--title'>公司资质</p>
                <p className='agency__item--content' dangerouslySetInnerHTML={{ __html: agency.qualification && agency.qualification.replace(/\n|\r\n/g, '<br />') }}></p>
              </div>
              <div className='agency__item'>
                <p className='agency__item--title'>业务名称</p>
                <p className='agency__item--content'>{agency.businessName}</p>
              </div>
              <div className='agency__item'>
                <p className='agency__item--title'>服务内容</p>
                <p className='agency__item--content' dangerouslySetInnerHTML={{ __html: agency.serviceContent && agency.serviceContent.replace(/\n|\r\n/g, '<br />') }}></p>
                {/* <RenderImage imgList={agency.serviceContentImgList}/> */}
                {
                  agency.serviceContentImgList && agency.serviceContentImgList.map((img, index) => {
                    return <QtImg src={img.fileUrl} srcs={srcs} key={img.fileId} className='details_image' />;
                  })
                }
              </div>
              <div className='agency__item'>
                <p className='agency__item--title'>能力保障</p>
                <p className='agency__item--content' dangerouslySetInnerHTML={{ __html: agency.capacityProtection && agency.capacityProtection.replace(/\n|\r\n/g, '<br />') }}></p>
                {/* <RenderImage imgList={agency.capacityProtectionImgList}/> */}
                {
                  agency.capacityProtectionImgList && agency.capacityProtectionImgList.map((img, index) => {
                    return <QtImg src={img.fileUrl} srcs={srcs} key={img.fileId} className='details_image' />;
                  })
                }
              </div>
              <div className='agency__item'>
                <p className='agency__item--title'>突出业绩</p>
                <p className='agency__item--content' dangerouslySetInnerHTML={{ __html: agency.outstandingPerformance && agency.outstandingPerformance.replace(/\n|\r\n/g, '<br />') }}></p>
                {/* <RenderImage imgList={agency.outstandingPerformanceImgList}/> */}
                {
                  agency.outstandingPerformanceImgList && agency.outstandingPerformanceImgList.map((img, index) => {
                    return <QtImg src={img.fileUrl} srcs={srcs} key={img.fileId} className='details_image' />;
                  })
                }
              </div>
              <div className='agency__item'>
                <p className='agency__item--title'>典型案例</p>
                <p className='agency__item--content' dangerouslySetInnerHTML={{ __html: agency.typicalCase && agency.typicalCase.replace(/\n|\r\n/g, '<br />') }}></p>
                {/* <RenderImage imgList={agency.typicalCaseImgList}/> */}
                {
                  agency.typicalCaseImgList && agency.typicalCaseImgList.map((img, index) => {
                    return <QtImg src={img.fileUrl} srcs={srcs} key={img.fileId} className='details_image' />;
                  })
                }
              </div>
              <div className='agency__item'>
                <p className='agency__item--title'>其他信息</p>
                <p className='agency__item--content' dangerouslySetInnerHTML={{ __html: agency.otherInformation && agency.otherInformation.replace(/\n|\r\n/g, '<br />') }}></p>
                {/* <RenderImage imgList={agency.otherInformationImgList} /> */}
                {
                  agency.otherInformationImgList && agency.otherInformationImgList.map((img, index) => {
                    return <QtImg src={img.fileUrl} srcs={srcs} key={img.fileId} className='details_image' />;
                  })
                }
              </div>
            </Article>
            : this.props.children

        }
      </div>
    );
  }
}

export default Container;