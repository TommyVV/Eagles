import React, { Component } from 'react';
import './style.less';

const myIcon = require('../../../image/my_icon.png');

class DownloadItem extends Component {
  constructor(props, context) {
    super(props, context);
  }
  render() {
    let isDownloading = this.props.isDownloading;
    return (
      <div className="download-item">
        <div className="download-img-div">
          <img src={myIcon} />
        </div>
        <div className="download-info">
          <div className="name">
            中冶赛迪
                  </div>
          <div className="detail">
            央企冶金行业工程总承包
                  </div>
        </div>
        <div className="download-div" >
          <span className="progress" style={{ display: isDownloading ? null : 'none' }}>96%</span>
        </div>
      </div >
    );
  }
}

export default DownloadItem;
