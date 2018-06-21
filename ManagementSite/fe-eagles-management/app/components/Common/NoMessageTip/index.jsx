import React, { Component } from 'react';
import {Msg} from 'react-qtui';
export default class NoMessageTip extends Component {
    constructor(props, context) {
        super(props, context);
        this.state = {
        };
    }

    //渲染内容
    renderMessage() {
        let msg = '';
        const { pageSize, listLen, title, description, loading } = this.props;
        if (loading){
            msg = "加载中";
        } else if (listLen == 0 ) {
            msg = <Msg type="info"
                title={title}
                description={description} />
        } else if (listLen >= pageSize ) {
            msg = "没有更多了";
        } else {
            msg = '';
        }
        return (
            <div style={{ textAlign: 'center' }}>{msg}</div>
        )
    }


    render() {
        return (
            <div>{this.renderMessage()}</div>
        );
    }
}