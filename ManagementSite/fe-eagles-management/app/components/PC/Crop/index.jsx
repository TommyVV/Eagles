import React, { Component } from 'react';
import { Modal, Button, Row, Col, message } from "antd";
import ReactCrop, { makeAspectCrop } from "react-image-crop";
import "react-image-crop/dist/ReactCrop.css";
import { fileUpload } from "../../../services/fileService";
import { serverConfig } from "../../../constants/ServerConfigure";
import axios from "axios";
import './style.less';

const UPLOAD = serverConfig.API_SERVER + serverConfig.FILE.UPLOAD; //文件上传地址

class Crop extends Component {
  constructor(props) {
    super(props);
    this.state = {
      crop: {
        x: 0,
        y: 0,
      },
      maxHeight: 100,
      dataUrl: '',
      file: '',
    }
  }
  onImageLoaded = (image) => {
    let width = image.naturalWidth;
    let height = image.naturalHeight
    let crop = makeAspectCrop({
      x: 40,
      y: 40,
      aspect: 1,
      width: 20,
    }, width / height)
    this.setState({ crop, image });
    let pixelCrop = {
      x: Math.round(width * 0.4),
      y: Math.round(height * 0.4),
      width: Math.round(width * 0.2),
      height: Math.round(width * 0.2)
    }
    this.onCropChange(crop, pixelCrop, image)
  }

  onCropChange = (crop, pixelCrop, image = this.state.image) => {
    let croppedImg = this.getCroppedImg(image, pixelCrop, '')
    let eleAppend = document.getElementById("img");
    eleAppend.src = croppedImg;
    this.setState({ crop });
  }

  onCropComplete = async (crop, pixelCrop) => {
    let { file } = this.state;
    let eleAppend = document.getElementById("img");
    var blob = eleAppend.src && this.dataURLtoBlob(eleAppend.src);
    var myfile = new File([blob], file.name, { type: file.type });
    let formData = new FormData();
    let { token } = JSON.parse(localStorage.info);
    formData.append('file', myfile);
    formData.append('token', token);
    // await fileUpload(formData)

    // let res = await axios.post(UPLOAD, formData).then(res => {
    //   // this.ticket = res.data.image
    //   // this.state.upData[tag] = logoTicket  
    //   // this.checkIfCanCommit()  
    //   console.log('res - ', res)

    // }).catch(err => {
    //   console.error(err)
    // })
    const isLt1M = myfile.size / 1024 / 1024 < 1;
    if (!isLt1M) {
      return message.error('截取头像大小不能超过1M!');
    }
    var request = new XMLHttpRequest();
    request.open("POST", UPLOAD);
    request.send(formData);
    let _this = this;
    request.onreadystatechange = function () {
      if (request.readyState == 4 && request.status == 200) {
        let handleFile = _this.props.handleFile()
        let { data } = JSON.parse(request.responseText)
        let { fileId, fileUrl, fileName } = data
        handleFile.done(fileUrl)//文件fileId列表
        _this.handleCancle();
      }
    }
  }

  dataURLtoBlob = (dataurl) => {
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
      bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
      u8arr[n] = bstr.charCodeAt(n);
    }
    return new Blob([u8arr], { type: mime });
  }

  getCroppedImg = (image, pixelCrop, fileName) => {
    const canvas = document.createElement('canvas');
    canvas.width = pixelCrop.width;
    canvas.height = pixelCrop.height;
    const ctx = canvas.getContext('2d');

    ctx.drawImage(
      image,
      pixelCrop.x,
      pixelCrop.y,
      pixelCrop.width,
      pixelCrop.height,
      0,
      0,
      pixelCrop.width,
      pixelCrop.height
    );

    // As Base64 string
    const base64Image = canvas.toDataURL('image/jpeg');
    return base64Image;

    // As a blob
    // return new Promise((resolve, reject) => {
    //   canvas.toBlob(file => {
    //     file.name = fileName;
    //     resolve(file);
    //   }, 'image/jpeg');
    // });
  }

  handleChnage = (e) => {
    const imageType = /^image\//;
    const file = e.target.files.item(0);
    if (!file || !imageType.test(file.type)) {
      return;
    }

    const reader = new FileReader();

    reader.onload = (e2) => {
      console.log('file - ', file)
      this.setState({ dataUrl: e2.target.result, file })
    };

    reader.readAsDataURL(file);
  }

  handClick = () => {
    let file = document.getElementById('file');
    file.click();
  }

  handleCancle = () => {
    this.props.onCancel();
  }
  render() {
    const { dataUrl, file } = this.state;
    return (
      <Modal
        visible={true}
        title={'上传头像'}
        onCancel={this.handleCancle}
        zIndex={1000}
        footer={[
          <Button key="back" onClick={this.handleCancle}>取消</Button>,
          <Button key="submit" type="primary" onClick={this.onCropComplete}>
            确认
          </Button>,
        ]}
        className='crop__modal'
      >
        <div>
          <Row gutter={24}>
            <Col span={4}>上传头像</Col>
            <Col className='filename' span={13}>
              {file.name}
            </Col>
            <Col span={4}>
              <Button onClick={this.handClick}>
                浏览...
              </Button>
            </Col>
          </Row>
          <input type="file" name="" id="file" accept='image/*' onChange={(e) => this.handleChnage(e)} />
        </div>
        <img src="" alt="" id="img" />
        <div id='crop-area'></div>
        <ReactCrop
          {...this.state}
          src={dataUrl}
          onImageLoaded={this.onImageLoaded}
          // onComplete={this.onCropComplete}
          onChange={this.onCropChange}
        />
      </Modal>
    );
  }
}

export default Crop;