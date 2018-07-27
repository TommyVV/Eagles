import { Upload, Icon, Modal, Checkbox, message } from "antd";
import React, { Component } from "react";
import { serverConfig } from "../../../../constants/config/ServerConfigure";
import { fileSize } from "../../../../constants/config/appconfig";

class AddImage extends React.Component {
  debugger;
  state = {
    loading: false,
    isShowUpload: this.props.Img ? true : false
  };
  change = e => {
    const isImg = e.target.checked;
    if (!isImg) {
      // 保存数据
      const { changeImg } = this.props;
      changeImg("");
    }
    this.setState({
      isShowUpload: isImg
    });
  };
  beforeUpload(file) {
    const reg = /^image\/(png|jpeg|jpg)$/;
    const type = file.type;
    const isImage = reg.test(type);
    if (!isImage) {
      message.error("只支持格式为png,jpeg和jpg的图片!");
    }

    if (file.size > fileSize) {
      message.error("图片必须小于10M");
    }
    return isImage && file.size <= fileSize;
  }
  onChangeImage = info => {
    if (info.file.status !== "uploading") {
      console.log(info.file, info.fileList);
    }
    if (info.file.status === "done") {
      message.success(`${info.file.name} 上传成功`);
      const imageUrl = info.file.response.Result.FileUploadResults[0].FileUrl;
      // 保存数据
      const { changeImg } = this.props;
      changeImg(imageUrl);
    } else if (info.file.status === "remove") {
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  render() {
    const { previewVisible, previewImage, fileList, isShowUpload } = this.state;
    const { Img } = this.props;
    return (
      <div>
        <Checkbox onChange={this.change}>是否包含图片</Checkbox>
        <div
          className="clearfix"
          style={{ display: isShowUpload ? "block" : "none" }}
        >
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this)}
          >
            {Img ? (
              <img src={Img} alt="avatar" style={{ width: "100%" }} />
            ) : (
              <div>
                <Icon type={this.state.loading ? "loading" : "plus"} />
                <div className="ant-upload-text">上传</div>
              </div>
            )}
          </Upload>
          {/* <Modal
            visible={previewVisible}
            footer={null}
            onCancel={this.handleCancel}
          >
            <img alt="example" style={{ width: "100%" }} src={previewImage} />
          </Modal> */}
        </div>
      </div>
    );
  }
}

export default AddImage;
