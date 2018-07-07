import React from 'react';
import { Upload, Icon, message } from 'antd';
import { connect } from "react-redux";
import { serverConfig } from "../../../constants/ServerConfigure";
import './style.less';

const UPLOAD = serverConfig.API_SERVER + serverConfig.FILE.UPLOAD; //文件上传地址

@connect(
  state => {
    return {
      agency: state.agencyReducer
    }
  },
  null
)
export default class AvatarUpload extends React.Component {

  constructor(props) {
    super(props);
    this.state = {
      loading: false,
      imageUrl: '',
    }
  }
  componentWillMount() {
    const { token } = JSON.parse(localStorage.info);
    this.setState({ token })
  }
  getBase64 = (img, callback) => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(reader.result));
    reader.readAsDataURL(img);
  }

  beforeUpload = (file) => {
    // const isJPG = file.type === 'image/jpeg';
    const reg = /^image\/(png|jpeg|jpg)$/;
    const type = file.type;
    const isImage = reg.test(type);
    if (!isImage) {
      message.error('只支持格式为png,jpeg和jpg的图片!');
    }
    const isLt1M = file.size / 1024 / 1024 < 1;
    if (!isLt1M) {
      message.error('图片大小不能超过1M!');
    }
    return isImage && isLt1M;
  }

  handleChange = (info) => {
    let { file } = info;
    let handleFile = this.props.handleFile()
    if (info.file.status === 'uploading') {
      this.setState({ loading: true});
      return;
    }
    if (info.file.status === 'done') {
      // Get this url from response in real world.
      let { fileId, fileUrl, fileName, fileSize } = file.response.data
      handleFile.done(fileUrl)//文件fileId列表
      this.getBase64(info.file.originFileObj, imageUrl => this.setState({
        imageUrl,
        loading: false,
      }));
    }
  }

  render() {
    let { token, loading } = this.state;
    const uploadButton = (
      <div>
        <Icon type={loading ? 'loading' : 'plus'} />
        <div className="ant-upload-text">点击添加头像</div>
      </div>
    );
    
    const imageUrl = loading ? '' :  this.props.agency.avatar || this.state.imageUrl;
    // const imageUrl = this.state.imageUrl;
    // console.log('头像url - ',imageUrl)
    return (
      <Upload
      onClick={()=>console.log(1)}
        listType="picture-card"
        className="avatar-uploader"
        showUploadList={false}
        action={UPLOAD}
        beforeUpload={this.beforeUpload}
        onChange={this.handleChange}
        headers={{
          'X-Requested-With': null,
        }}
        data={{ token }}
      >
        {imageUrl ? <img src={imageUrl} alt="" /> : uploadButton}
      </Upload>
    );
  }
}
