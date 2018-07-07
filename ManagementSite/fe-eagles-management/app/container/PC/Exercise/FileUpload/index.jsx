import React from 'react';
import { Upload, message, Button } from 'antd';
import { serverConfig } from "../../../../constants/ServerConfigure";

let isType = true;
let tips = true;

export default class ImageUpload extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      previewVisible: false,
      previewImage: '',
      fileList: [],//附件列表
      token: '',
      projectId: '',
    };
  }

  componentWillMount() {
    let { projectId } = this.props;
    const { token } = JSON.parse(localStorage.info);
    this.setState({ token, projectId });
  }

  // 点击文件链接或预览图标时的回调
  handlePreview = (file) => {
    this.setState({
      previewImage: file.url || file.thumbUrl,
      previewVisible: true,
    });
  }

  handleChange = (info) => {
    let { file, fileList } = info;
    if (isType) {
      let handleFile = this.props.handleFile();
      handleFile.start();
      this.setState({ fileList });
      let { status } = file;

      if (status === 'error') {// 上传失败
        message.error('该文件上传失败');
      }

      if (status === 'done') { // 上传成功
        handleFile.done();
        console.log(`上传成功 - ${file.name}`, status);
      }
    } else {
      this.setState({ fileList: this.state.fileList });
      isType = true;
      tips = true;
    }
  }

  checkType = (file, fileList) => {
    /**
     * doc："application/msword"
     * docx："application/vnd.openxmlformats-officedocument.wordprocessingml.document"
     * ppt："application/vnd.ms-powerpoint"
     * pptx："application/vnd.openxmlformats-officedocument.presentationml.presentation"
     * xls："application/vnd.ms-excel"
     * xlsx："application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
     * pdf："application/pdf"
     */

    // 限制文件类型
    let isFile = true;
    fileList.forEach(v => {
      let type = v.type;
      const reg = /^application\/(msword|vnd\.openxmlformats-officedocument\.wordprocessingml\.document|vnd\.ms-powerpoint|vnd\.openxmlformats-officedocument\.presentationml\.presentation|vnd\.ms-excel|vnd\.openxmlformats-officedocument\.spreadsheetml\.sheet|pdf)$/;
      if (!reg.test(type)) {
        isFile = false;
        if (file.uid === fileList[fileList.length - 1].uid && tips) {
          message.error('只支持格式为word,ppt,excel和pdf的文件!!');
          tips = false
        }
      }
    })

    if (!isFile) {
      isType = false;
      return isType
    }

    // 限制文件大小
    let isLt50M = true;
    fileList.forEach(v => {
      let size = v.size;
      isLt50M = file.size / 1024 / 1024 < 50;
      if (!isLt50M) {
        isLt50M = false;
        if (file.uid === fileList[fileList.length - 1].uid && tips) {
          message.error('文件大小不能超过50M!');
          tips = false
        }
      }
    })
    if (!isLt50M) {
      isType = false;
      return isType
    }
  }
  render() {
    let { fileList, token, projectId } = this.state;
    const UPLOAD = serverConfig.API_SERVER + serverConfig.PROJECT.UPLOAD; //文件上传地址
    return (
      <div>
        <Upload
          action={UPLOAD}
          fileList={fileList}
          multiple={true}
          onChange={this.handleChange}
          beforeUpload={this.checkType}
          headers={{
            'X-Requested-With': null,
            // 'Content-Type': 'multipart/form-data',
            // 'contentType': false, //必须false才会避开jQuery对 formdata 的默认处理 XMLHttpRequest会对 formdata 进行正确的处理
            // 'processData': false, //必须false才会自动加上正确的Content-Type
          }}
          data={{ token, projectId }}
        >
          <Button className='btn btn--primary' type="primary">上传文件</Button>
        </Upload>
      </div>
    );
  }
}