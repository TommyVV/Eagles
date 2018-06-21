import React from 'react';
import { Upload, message } from 'antd';
import { connect } from "react-redux";
import { serverConfig } from "../../../constants/ServerConfigure";
import { saveFileUrl } from "../../../actions/PC/shareAction";
import "./style.less";

const UPLOAD = serverConfig.API_SERVER + serverConfig.FILE.UPLOAD; //文件上传地址

let isType = true
let tips = true;
@connect(
  state => {
    return {
      share: state.shareReducer
    }
  },
  { saveFileUrl }
)
export default class ImageUpload extends React.Component {

  static defaultProps = {
    max: 5
  }

  constructor(props) {
    super(props);
    this.state = {
      flag: false,
      fileList: [],//附件列表
      filenameMap: new Map(),//记录上传成功图片的最新修改时间map
      token: '',
      uidToFileMap: new Map(),
    };
  }

  componentWillMount() {
    const { token } = JSON.parse(localStorage.info);
    this.setState({ token })
  }

  handleChange = (info) => {
    let { file, fileList } = info;
    let { status, fileId, name } = file;
    console.log('handlechange - ', isType)

    if (isType) { //是期望类型的文件
      let handleFile = this.props.handleFile()
      this.setState({ fileList, flag: true })

      if (status === 'error') {// 上传失败
        message.error('该文件上传失败')
      }

      if (status === 'removed') { // 删除附件
        let { uidToFileMap } = this.state;
        fileId ? fileId = fileId : fileId = uidToFileMap.get(file.uid) //映射 。 第一次发布
        handleFile.move(fileList, uidToFileMap, fileId)
        this.setState({ fileList })
      }

      if (status === 'done') { // 上传成功
        let { fileId, fileUrl, fileName, uid } = file.response.data;
        let { uidToFileMap } = this.state;
        uidToFileMap.set(uid, fileId);
        handleFile.done(fileList, uidToFileMap, fileId)//文件fileId列表
        console.log(`${file.name}  - 上传成功`, file);
        this.setState({ fileList });
        tips = true;
      }

    } else {
      this.setState({ fileList: this.state.fileList })
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
    let isLt1M = true;
    fileList.forEach(v => {
      let size = v.size;
      isLt1M = file.size / 1024 / 1024 < 50;
      if (!isLt1M) {
        isLt1M = false;
        if (file.uid === fileList[fileList.length - 1].uid && tips) {
          message.error('文件大小不能超过50M!');
          tips = false
        }
      }
    })
    if (!isLt1M) {
      isType = false;
      return isType
    }
  }

  handleData = (file) => {
    return {
      token: this.state.token,
      uid: file.uid
    }
  }

  render() {
    let { fileList, flag, token } = this.state;
    let { max } = this.props;
    if (flag) { // 非映射关系不显示
      fileList = fileList
    } else { // 初始化加载图片
      let fileArr = []
      this.props.fileList && this.props.fileList.forEach((attachment, index) => fileArr.push({
        uid: attachment.fileId,
        fileId: attachment.fileId,
        url: attachment.fileUrl,
        name: attachment.fileName,
        status: 'done',
      }))
      fileList = fileArr
    }
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
          }}
          data={(file) => this.handleData(file)}
        >
          <a href="javascript:;" className='choose_file'>点击上传附件</a>
        </Upload>
      </div>
    );
  }
}