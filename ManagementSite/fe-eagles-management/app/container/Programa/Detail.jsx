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
  Icon,
  InputNumber
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/programaService";
import { serverConfig } from "../../constants/config/ServerConfigure";
import { fileSize, publicMap } from "../../constants/config/appconfig";
import { saveInfo, clearInfo } from "../../actions/programaAction";
import { pageMap } from "../../constants/config/appconfig";

import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
const { TextArea } = Input;
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      goodsReducer: state.goodsReducer
    };
  },
  { saveInfo, clearInfo }
)
class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: false // 上传
    };
  }

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          let params = {
            Info: {
              ...this.props.programa,
              ...values
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = this.props.programa.ColumnId
              ? "保存栏目成功"
              : "创建栏目成功";
            message.success(tip);
            hashHistory.replace("/programalist");
          } else {
            let tip = this.props.programa.ColumnId
              ? "保存栏目失败"
              : "创建栏目失败";
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
  // 传递图片前将数据保存
  saveInfo = () => {
    let { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    this.props.saveAgencyInfo(values);
    // console.log('上传图片记录表单数据 - ', values, this.props.share)
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
  // 商品缩略图 或者  详情图
  onChangeImage = (imageTitle, info) => {
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
        this.props.saveInfo({ ...values, [imageTitle]: imageUrl });
      } else {
        message.error(`${Message}`);
      }
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  changeTop(value) {
    let { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    this.props.saveInfo({ ...values, IsSetTop: value });
  }
  render() {
    const { getFieldDecorator } = this.props.form;
    const { programa } = this.props;
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
          {getFieldDecorator("ColumnId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="栏目名称">
          {getFieldDecorator("ColumnName", {
            rules: [
              {
                required: true,
                message: "必填，请输入栏目名称"
              }
            ]
          })(<Input placeholder="必填，请输入栏目名称" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="栏目类型">
          {getFieldDecorator("SubCateType")(
            <Select>
              {publicMap.map((obj, index) => {
                return (
                  <Option key={index} value={obj.value}>
                    {obj.text}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="地址">
          {getFieldDecorator("TargetUrl")(<Input placeholder="请输入地址" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="排序码">
          {getFieldDecorator("OrderBy")(<Input placeholder="请输入排序码" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="是否在首页显示">
          {getFieldDecorator("IsSetTop")(
            <Select onChange={this.changeTop.bind(this)}>
              <Option value="0">否</Option>
              <Option value="1">是</Option>
            </Select>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="首页显示数量"
          style={{ display: programa.IsSetTop == "1" ? null : "none" }}
        >
          {getFieldDecorator("IndexPageCount")(
            <InputNumber
              placeholder="必填，请输入首页显示数量"
              min={0}
              style={{ width: "100%" }}
            />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="缩略图">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this, "ColumnIcon")}
          >
            {programa.ColumnIcon ? (
              <img
                src={programa.ColumnIcon}
                alt="avatar"
                style={{ width: "100%" }}
              />
            ) : (
              <div>
                <Icon type={this.state.loading ? "loading" : "plus"} />
                <div className="ant-upload-text">上传</div>
              </div>
            )}
          </Upload>
        </FormItem>
        <FormItem {...formItemLayout} label="详情图">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this, "ColumnImg")}
          >
            {programa.ColumnImg ? (
              <img
                src={programa.ColumnImg}
                alt="avatar"
                style={{ width: "100%" }}
              />
            ) : (
              <div>
                <Icon type={this.state.loading ? "loading" : "plus"} />
                <div className="ant-upload-text">上传</div>
              </div>
            )}
          </Upload>
        </FormItem>
        <FormItem {...formItemLayout} label="所属页面">
          {getFieldDecorator("ModuleType")(
            <Select>
              {pageMap.map((obj, index) => {
                return index > 0 ? (
                  <Option key={index} value={obj.value}>
                    {obj.text}
                  </Option>
                ) : null;
              })}
            </Select>
          )}
        </FormItem>
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {!this.props.programa.ColumnId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/programalist")}
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
    console.log("详情数据回显 - ", props);
    const programa = props.programa;
    return {
      ColumnId: Form.createFormField({ value: programa.ColumnId }),
      ColumnName: Form.createFormField({ value: programa.ColumnName }),
      TargetUrl: Form.createFormField({ value: programa.TargetUrl }),
      OrderBy: Form.createFormField({ value: programa.OrderBy }),
      IsSetTop: Form.createFormField({
        value: programa.IsSetTop == 0 ? "0" : "1"
      }),
      IndexPageCount: Form.createFormField({ value: programa.IndexPageCount }),
      SubCateType: Form.createFormField({
        value: programa.SubCateType ? programa.SubCateType + "" : "0"
      }),
      ModuleType: Form.createFormField({
        value: programa.ModuleType ? programa.ModuleType.toString() : "1"
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      programaReducer: state.programaReducer
    };
  },
  { saveInfo, clearInfo }
)
class ProgramaDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.props.clearInfo();
    }
  }

  componentWillUnmount() {
    this.props.clearInfo();
  }
  // 根据id查询详情
  getInfo = async ColumnId => {
    try {
      const { Info } = await getInfoById({ ColumnId });
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    return (
      <Nav>
        <FormMap programa={this.props.programaReducer} />
      </Nav>
    );
  }
}

export default ProgramaDetail;
