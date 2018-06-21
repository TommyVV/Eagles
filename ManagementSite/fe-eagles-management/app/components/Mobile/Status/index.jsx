import React, { Component } from 'react';
import './style.less';

export default class Status extends Component {
    constructor(props, context) {
        super(props, context);
    }

    //检查发布状态  0：未发布(草稿)； 1：已发布； 2：审核中; 3未通过审核; 4已关闭
    checkStatus(status) {
        console.log(status);
        let statusMsg = '';
        switch (status) {
            case 0:
                statusMsg = <span className="share-status unpublished"><i className="iconfont icon-unpublished" />未发布</span>
                break;
            case 1:
                statusMsg = <span className="share-status pass"><i className="iconfont icon-pass" />已发布</span>
                break;
            case 2:
                statusMsg = <span className="share-status wait"><i className="iconfont icon-wait" />审核中</span>
                break;
            case 3:
                statusMsg = <span className="share-status refuse"><i className="iconfont icon-refuse" />未通过</span>
                break;
            case 4:
                statusMsg = <span className="share-status unpublished"><i className="iconfont icon-pass" />已关闭</span>
                break;
            default:
                statusMsg = '';
        }
        return statusMsg;
    }


    render() {
        const { publishItem } = this.props;
        let statusMsg = this.checkStatus(publishItem.approvalStatus);
        return (
            <div className="share-status--wrapper">
                {statusMsg}
                <span className="status-reason">{publishItem.rejectReason}</span>
            </div>
        );
    }
}
