import React, { Component } from 'react';
import { serverConfig } from '../../../constants/ServerConfigure';
import * as GlobalActions from '../../../actions/mobile/globalAction';
import FileType from '../../../components/Mobile/File/FileType';
import qt from '../../../../static/lib/qingtui_jssdk-2.1';
import "./style.less";

class FileList extends Component {

  static defaultProps = {
    attachment: [],
  }

  constructor(props) {
    super(props);
    this.state = {
      isBuy: false,
    }
  }

  componentWillMount() {
    const {price, isBuy, op, isMy} = this.props;
    // console.log(this.props.isBuy);
    console.log('op - ', this.props.op);
    let buy =  false;
    if (isMy || op || price == 0){
      buy = true;
    } else {
      buy = isBuy == 1 ? true : false;
    }
    this.setState({
      isBuy: buy
    });
  }

  componentWillReceiveProps(nextProps) {
    console.log(nextProps.isBuy);
    if (nextProps.isBuy !== this.props.isBuy) {
       const {isMy, op, price, isBuy} = nextProps;
      let buy = false;
      if (isMy || op || price == 0) {
        buy = true;
      } else {
        buy = isBuy == 1 ? true : false;
      }
      this.setState({
        isBuy: buy
      })
    }
  }


  //下载
  download(fileId) {
    const { token } = JSON.parse(localStorage.info);
    const clientInfo = qt.getClientInfo();
    // //在点击的时候再判断必须是已买的才可以点链接
    if (this.state.isBuy) {
      if (this.props.op) {
        window.location.href = serverConfig.API_SERVER + "/file/only/download?fileId=" + fileId +  "&token=" + token + "&clientType=" + clientInfo.type;  
      } else {
        window.location.href = serverConfig.API_SERVER + "/file/download?fileId=" + fileId + "&sessionKey=" + Math.random() + "&token=" + token + "&clientType=" + clientInfo.type;
      }
    }

  }

  //购买附件
  payShare() {
    this.props.payShare();
  }

  //渲染文件类型
  renderFileType(fileType){
    let item = '';
    if (/doc/i.test(fileType)) {
      item = <span className='download--filetype doc-bg'>{fileType}</span>
    } else if (/xls/i.test(fileType)) {
      item = <span className='download--filetype xls-bg'>{fileType}</span>
    } else if (/pdf/i.test(fileType)) {
      item = <span className='download--filetype pdf-bg'>{fileType}</span>
    } else {
      item = <span className='download--filetype file-bg'>{fileType}</span>
    }
    return item;
  }

  //列表渲染
  getAttachmentList(attachment) {
    let list = [];
    attachment && attachment.map((item, index) => {
      list.push(
        <div className="download--filename" key={index} onClick={this.download.bind(this, item.fileId)}>
          {this.renderFileType(item.fileType)}
          {/* <span className='download--filetype'>{item.fileType}</span> */}
          {item.fileName}
        </div>
      )
    });
    return list;
  }


  render() {
    const { isBuy} = this.state;
    const { attachment, price} = this.props;
    return (
      <div className="download-bg">
        <div className={isBuy ? 'download__container' : 'download__container boughtBottom'}>
          <div className="attach_list">
            {this.getAttachmentList(attachment)}
            {!isBuy ?
              <div>
                <div className="attach-mask"></div>
                <div className="download--btn" onClick={this.payShare.bind(this)}>￥{price}购买</div>
              </div> : ''}
          </div>
        </div>
        {/* <div className={isShowLoading ? 'pop-tip is-show' : 'pop-tip'}>正在下载中...</div> */}
      </div>
    );
  }
}

export default FileList;