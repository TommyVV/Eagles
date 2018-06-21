import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import { Article } from 'react-qtui';
import RenderImage from "../../RenderImage";
import util from "../../../../utils/util";
import FileList from "../../FileList";
import QtImg from "../../RenderImage/QtImage";
import { connect } from 'react-redux';
import {saveTab} from '../../../../actions/mobile/globalAction';
import './style.less';

@connect(
  state => {
    return {}
  },
  { saveTab }
)
class ShareDetailCommon extends Component {

  static defaults = {
    detail: {}
  }

  constructor(props, context) {
    super(props, context);
  }

  jumpToDetail = () => {
    this.props.saveTab(0);
    const { op, detail } = this.props;
    // 不是运营的分享详情页面，才可以点击发布者的详情
    if (!op) {
      if (detail.identityType == '0') { // 0代表个人，1代表机构
        hashHistory.push(`hisdetail/${detail.identityId}`);
      } else {
        hashHistory.push(`agencydetail/${detail.identityId}`);
      }
    }
  }
  render() {
    const { detail, isMy } = this.props;
    let srcs = []
    detail.imgList && detail.imgList.forEach(img => srcs.push(img.fileUrl));
    return (
      <Article>
        <div className="title-abc">{detail.title}</div>
        <div className="share__people--wrapper">
          <img className="share__people--img" src={detail.creatorAvatar} />
          <span className="share__people--name" onClick={() => this.jumpToDetail()}>{detail.identityName}</span>
          <span className="share__people--time"> {util.timeStampConvent(detail.updateTime, 'yyyy-MM-dd')}</span>
          {this.props.children}
        </div>
        <section>
          <p dangerouslySetInnerHTML={{ __html: detail.introduction && detail.introduction.replace(/\n|\r\n/g, '<br />') }}></p>
          {
            detail.imgList && detail.imgList.map((img, index) => {
              return <QtImg src={img.fileUrl} srcs={srcs} key={img.fileId} className='details_image' />;
            })
          }
          {/* <RenderImage imgList={detail.imgList} /> */}
        </section>
        {detail.attachmentList && detail.attachmentList.length > 0 ? 
        <FileList attachment={detail.attachmentList} 
              isBuy={detail.isBuy} 
              price={detail.price}
              op={this.props.op}
              isMy={this.props.isMy} 
              payShare={this.props.payShare}
              /> 
        : null}
      </Article>
    );
  }
}

export default ShareDetailCommon;