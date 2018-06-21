import React, { Component } from 'react';
import './style.less';

export default class FileType extends Component {
    constructor(props, context) {
        super(props, context);
    }

    //检查状态， word, excel, pdf, ppt
    checkFileType(type) {
        let statusMsg = '';
        if (/doc/i.test(type)){
            statusMsg = <i className="iconfont icon-DOCx doc-bg"/>
        } else if (/xls/i.test(type)){
            statusMsg = <i className="iconfont icon-XLSx xls-bg" />
        } else if (/pdf/i.test(type)){
            statusMsg = <i className="iconfont icon-PDFx pdf-bg"/>
        } else {
            statusMsg = <i className="iconfont icon-FLILEx file-bg" />
        }
        return statusMsg;
    }


    render() {
        const { fileType } = this.props;
        return (
            <div className="file-img">
                {this.checkFileType(fileType)}
            </div>
        );
    }
}
