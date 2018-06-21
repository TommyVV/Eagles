import React, { Component } from 'react';
import Utils from '../../../utils/util';
import FileType from '../../../components/Mobile/File/FileType';
import Dotdotdot from 'react-dotdotdot';
import './style.less';

class FileItem extends Component {
  constructor(props, context) {
    super(props, context);
  }

  deleteFile(e) {
    e.stopPropagation();
    this.props.handleDelete();
  }

  downloadFile() {
    this.props.handleDownload();
  }


  render() {
    const { file, isEdit } = this.props;
    return (
      <div className="file-item" onClick={this.downloadFile.bind(this)}>
        <FileType fileType={file.fileType} />
        <div className="file-info">
          <Dotdotdot clamp={2}>
            <div className="file-content">
              {file.fileName}
            </div>
          </Dotdotdot>
          <div className="bottom-info">
            <div className="date">{file.createTime ? Utils.formatTime(file.createTime, 'yyyy-MM-dd') : ''}</div>
            {isEdit ?
              <div>
                <div className="del" onClick={this.deleteFile.bind(this)}>删除</div>
              </div> : ''
            }
          </div>
        </div>
      </div>
    );
  }
}

export default FileItem;
