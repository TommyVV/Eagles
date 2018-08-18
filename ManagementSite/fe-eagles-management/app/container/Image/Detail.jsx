import React, { Component } from "react";
import { connect } from "react-redux";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Upload,
  Icon
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/imageService";
import { getOrgList } from "../../services/orgService";
import { serverConfig } from "../../constants/config/ServerConfigure";
import { fileSize, pageMap } from "../../constants/config/appconfig";
import { saveInfo, clearInfo } from "../../actions/imageAction";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
@connect(
  state => {
    return {
      user: state.userReducer,
      imageReducer: state.imageReducer
    };
  },
  { saveInfo, clearInfo }
)
class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: false
    };
  }
  componentWillUnmount() {
    this.props.clearInfo();
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { image, Orgs } = this.props;
          const { OrgId } = values;
          const org = Orgs.filter(o => o.OrgId == OrgId);
          let params = {
            Info: {
              ...image,
              ...values,
              OrgName: org && org[0].OrgName
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = image.Id ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/imagelist");
          } else {
            let tip = image.Id ? "保存失败" : "创建失败";
            message.error(tip);
          }
        } catch (e) {
          throw new Error(e);
        }
      } else {
        message.error("请检查字段是否正确");
      }
    });
  };

  beforeUpload(file) {
    const reg = /^image\/(png|jpeg|jpg|bmp)$/;
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

      const { Code, Result, Message } = info.file.response;
      if (Code == "00") {
        message.success(`${info.file.name} 上传成功`);
        const imageUrl = Result.FileUploadResults[0].FileUrl;
        // 保存数据
        let { getFieldsValue } = this.props.form;
        let values = getFieldsValue();
        this.props.saveInfo({ ...values, Img: imageUrl });
      } else {
        message.error(`${Message}`);
      }



    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { Orgs, image } = this.props;
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 6 }
      }
    };

    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("Id")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="所属机构">
          {getFieldDecorator("OrgId", {
            rules: [
              {
                required: true,
                message: "必填，请选择所属机构"
              }
            ]
          })(
            <Select>
              {Orgs.length &&
                Orgs.map((obj, index) => {
                  return (
                    <Option key={index} value={obj.OrgId}>
                      {obj.OrgName}
                    </Option>
                  );
                })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="页面类型">
          {getFieldDecorator("PageId", {
            rules: [
              {
                required: true,
                message: "必填，请选择页面类型"
              }
            ]
          })(
            <Select>
              {pageMap.map((obj, index) => {
                return (
                  <Option key={index} value={obj.value}>
                    {obj.text}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>

        <FormItem {...formItemLayout} label="跳转链接">
          {getFieldDecorator("TargetUrl", {
            rules: [
              {
                required: true,
                message: "必填，请输入跳转链接"
              }
            ]
          })(<Input placeholder="请输入跳转链接" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="滚动图片">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this)}
          >
            {image.Img ? (
              <img src={image.Img} alt="avatar" style={{ width: "100%" }} />
            ) : (
              <div>
                <Icon type={this.state.loading ? "loading" : "plus"} />
                <div className="ant-upload-text">上传</div>
              </div>
            )}
          </Upload>
          <b style={{ position: "absolute", width: "200px", top: "-112px", left: "120px" }}>  滚动图片建议长宽比为1 : 2.1</b>
        </FormItem>
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {!this.props.image.Id ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/imagelist")}
              >
                取消
              </Button>
            </Col>
          </Row>
        </FormItem>
      </Form>
    );
  }
}

const FormMap = Form.create({
  mapPropsToFields: props => {
    const { image } = props;
    console.log("机构详情数据回显 - ", image);
    return {
      Id: Form.createFormField({
        value: image.Id
      }),
      OrgId: Form.createFormField({
        value: image.OrgId ? image.OrgId : ""
      }),
      OrgName: Form.createFormField({
        value: image.OrgName
      }),
      PageId: Form.createFormField({
        value: image.OrgId ? image.PageId : "0"
      }),
      TargetUrl: Form.createFormField({
        value: image.TargetUrl
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      imageReducer: state.imageReducer
    };
  },
  { saveInfo, clearInfo }
)
class ImageDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Orgs: []
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.props.clearInfo();
      this.getOrgList();
    }
  }
  // 加载所有机构
  getOrgList = async () => {
    try {
      const { List } = await getOrgList();
      this.setState({ Orgs: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  componentWillUnmount() {
    this.props.clearInfo();
  }
  // 根据id查询详情
  getInfo = async Id => {
    try {
      await this.getOrgList();
      const { Info } = await getInfoById({ Id });
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        <FormMap image={this.props.imageReducer} Orgs={this.state.Orgs} />
      </Nav>
    );
  }
}

export default ImageDetail;
