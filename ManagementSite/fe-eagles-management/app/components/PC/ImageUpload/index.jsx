import React from 'react';
import { Upload, Icon, Modal, message } from 'antd';
import { serverConfig } from "../../../constants/ServerConfigure";
import './fileload.less';

const UPLOAD = serverConfig.API_SERVER + serverConfig.FILE.UPLOAD; //文件上传地址
let isType = true;
let tips = true;
// let isImage = true;
export default class ImageUpload extends React.PureComponent {

  static defaultProps = {
    max: 5,
  }

  constructor(props) {
    super(props);
    this.state = {
      previewVisible: false,
      flag: false,//用于判断映射关系
      previewImage: '',
      fileList: [],//图片列表，用于显示
      token: '',
      uidToFileMap: new Map(),
    };
  }

  componentWillMount() {
    const { token } = JSON.parse(localStorage.info);
    this.setState({ token })
  }



  // 隐藏预览
  handleCancel = () => this.setState({ previewVisible: false })

  // 点击文件链接或预览图标时的回调
  handlePreview = (file) => {
    this.setState({
      previewImage: file.url || file.thumbUrl,
      previewVisible: true,
    });
  }

  // 上传图片，成功显示，失败报错
  handleChange = (info) => {
    let { file, fileList } = info;
    let { status, fileId, name } = file;
    console.log('handlechange - ', isType)

    if (isType) { // 检查类型
      let handleFile = this.props.handleFile();
      this.setState({ fileList, flag: true })

      if (status === 'error') {// 上传失败
        message.error(`${name}上传失败`)
      }

      if (status === 'removed') { // 删除图片
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
    } else { // 非期望图片则警告显示
      this.setState({fileList: this.state.fileList})
      isType = true;
      tips = true;
    }
  }

  // 上传图片前检查类型
  checkType = (file, fileList) => {

    // 限制图片类型
    let isImage = true;
    fileList.forEach(v => {
      let type = v.type;
      let reg = /^image\/(png|jpeg|jpg)$/;
      if (!reg.test(type)) {
        isImage = false;
        if (file.uid === fileList[fileList.length - 1].uid && tips) {
          message.error('只支持格式为png,jpeg和jpg的图片!');
          tips = false
        }
      }
    })
    if (!isImage) {
      isType = false;
      return isType
    }

    // 限制图片大小
    let isLt1M = true;
    fileList.forEach(v => {
      let size = v.size;
      isLt1M = file.size / 1024 / 1024 < 1;
      if (!isLt1M) {
        isLt1M = false;
        if (file.uid === fileList[fileList.length - 1].uid && tips) {
          message.error('图片大小不能超过1M!');
          tips = false
        }
      }
    })
    if (!isLt1M) {
      isType = false;
      return isType
    }

    // 限制个数
    let isMax = true;
    fileList.forEach(v => {
      let size = v.size;
      isMax = this.props.count + fileList.length > this.props.max ? false : true;
      console.log('isMax - ', isMax, this.props.count, fileList.length)
      if (!isMax) {
        isMax = false;
        if (file.uid === fileList[fileList.length - 1].uid && tips) {
          message.error('超出最大数量限制!');
          tips = false
        }
      }
    })
    if (!isMax) {
      isType = false;
      return isType
    }

    // 是否是期望类型的图片
    isType = true;
    return isType;
  }

  handleData = (file) => {
    return {
      token: this.state.token,
      uid: file.uid
    }
  }

  render() {
    let { previewVisible, previewImage, fileList, flag, token } = this.state;
    let { max } = this.props;
    if (flag) { // 非映射关系不显示
      fileList = fileList
    } else { // 初始化加载图片，第一次进来
      let fileArr = []
      this.props.fileList && this.props.fileList.forEach((img, index) => fileArr.push({
        uid: img.fileId,
        fileId: img.fileId,
        url: img.fileUrl,
        name: img.fileName,
      }))
      fileList = fileArr
    }
    const uploadButton = (
      <div>
        <Icon type="plus" />
        <div className="ant-upload-text">
          点击添加图片<br />
          <span>(限制{max}张以内)</span>
        </div>
      </div>
    );
    return (
      <div className="clearfix">
        <Upload
          action={UPLOAD}
          listType="picture-card"
          fileList={fileList}
          multiple={true}
          onPreview={this.handlePreview}
          onChange={this.handleChange}
          beforeUpload={this.checkType}
          accept="image/*"
          headers={{
            'X-Requested-With': null,
            // 'Content-Type': 'multipart/form-data',
            // 'contentType': false, //必须false才会避开jQuery对 formdata 的默认处理 XMLHttpRequest会对 formdata 进行正确的处理
            // 'processData': false, //必须false才会自动加上正确的Content-Type
          }}
          data={(file) => this.handleData(file)}
        >
          {fileList.length >= max ? null : uploadButton}
        </Upload>
        <Modal visible={previewVisible} footer={null} onCancel={this.handleCancel}>
          <img alt="example" style={{ width: '100%' }} src={previewImage} />
        </Modal>
      </div>
    );
  }
}