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
  DatePicker
} from "antd";
import moment from "moment";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/goodsService";
import { serverConfig } from "../../constants/ServerConfigure";
import { saveInfo, clearInfo } from "../../actions/goodsAction";
// 引入编辑器以及编辑器样式
import BraftEditor from "braft-editor";
import "braft-editor/dist/braft.css";

import "./style.less";
import { uploadFile } from "../../services/fileService";

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
          let { SellStartTime, SellEndTime } = values;
          let params = {
            Info: {
              ...this.props.goods,
              ...values,
              SellStartTime: moment(SellStartTime, "yyyy-MM-dd").format(),
              SellEndTime: moment(SellEndTime, "yyyy-MM-dd").format()
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = this.props.goods.GoodsId
              ? "保存商品成功"
              : "创建商品成功";
            message.success(tip);
            hashHistory.replace("/goodslist");
          } else {
            let tip = this.props.goods.GoodsId
              ? "保存商品失败"
              : "创建商品失败";
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
    const isJPG = file.type === "image/jpeg";
    if (!isJPG) {
      message.error("只能上传图片!");
    }
    const isLt5M = file.size / 1024 / 1024 < 5;
    if (!isLt5M) {
      message.error("图片必须小于5M!");
    }
    return isJPG && isLt5M;
  }
  // 商品缩略图 或者  详情图
  onChangeImage = (imageTitle, info) => {
    if (info.file.status !== "uploading") {
      console.log(info.file, info.fileList);
    }
    if (info.file.status === "done") {
      message.success(`${info.file.name} 上传成功`);
      const imageUrl = info.file.response.Result.FileUploadResults[0].FileUrl;
      // 保存数据
      let { getFieldsValue } = this.props.form;
      let values = getFieldsValue();
      if (imageTitle == "GoodsIcon") {
        this.props.saveInfo({ ...values, GoodsIcon: imageUrl });
      } else if (imageTitle == "GoodsImg") {
        this.props.saveInfo({ ...values, GoodsImg: imageUrl });
      }
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };

  render() {
    const {
      getFieldDecorator,
      setFieldsValue,
      getFieldsValue
    } = this.props.form;
    const { goods } = this.props;
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
    const formItemLayoutDate = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 11 }
      }
    };
    const formItemLayoutContent = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 17 }
      }
    };
    // 编辑器属性
    const editorProps = {
      height: 300,
      contentFormat: "html",
      placeholder: "请输入商品详情",
      initialContent: goods.Content,
      media: {
        validateFn: file => {
          return file.size < 1024 * 1024 * 5;
        },
        uploadFn: async  (file) => {
          // const res=await uploadFile(file);
          debugger
        }
      },
      onChange: (Content, info) => {
        setFieldsValue({ Content });
        console.log("新闻内容：", getFieldsValue());
      }
      // onRawChange: this.handleRawChange
    };
    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("GoodslId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="商品名称">
          {getFieldDecorator("GoodsName", {
            rules: [
              {
                required: true,
                message: "必填，请输入商品名称!"
              }
            ]
          })(<Input placeholder="必填，请输入商品名称!" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("GoodsStatus")(
            <Select>
              <Option value="0">上架</Option>
              <Option value="1">下架</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="所需积分">
          {getFieldDecorator("Score", {
            rules: [
              {
                required: true,
                message: "必填，请输入商品所需积分!"
              }
            ]
          })(<Input placeholder="必填，请输入商品" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="已售">
          {getFieldDecorator("Sale")(<Input placeholder="请输入已售数量" />)}
        </FormItem>
        <FormItem label="销售时间" {...formItemLayoutDate}>
          <Col span={6}>
            <FormItem>
              {getFieldDecorator("SellStartTime", {
                rules: [
                  {
                    required: true,
                    message: "必填，请选择开始时间!"
                  }
                ]
              })(<DatePicker placeholder="请选择开始时间" />)}
            </FormItem>
          </Col>
          <Col span={1}>
            <span
              style={{
                display: "inline-block",
                width: "100%",
                textAlign: "center"
              }}
            >
              -
            </span>
          </Col>
          <Col span={6}>
            <FormItem>
              {getFieldDecorator("SellEndTime", {
                rules: [
                  {
                    required: true,
                    message: "必填，请选择结束时间!"
                  }
                ]
              })(<DatePicker placeholder="请选择结束时间" />)}
            </FormItem>
          </Col>
        </FormItem>
        <FormItem {...formItemLayout} label="每人允许兑换的最大数量">
          {getFieldDecorator("MaxExchangeNum", {
            rules: [
              {
                required: true,
                message: "必填，请输入每人兑换最大数量!"
              }
            ]
          })(<Input placeholder="必填，请输入每人兑换最大数量" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="商品参考价格">
          {getFieldDecorator("ReferePrice", {
            rules: [
              {
                required: true,
                message: "必填，请输入参考价格!"
              }
            ]
          })(<Input placeholder="必填，请输入参考价格!" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="库存">
          {getFieldDecorator("Stock", {
            rules: [
              {
                required: true,
                message: "必填，请输入库存!"
              }
            ]
          })(<Input placeholder="必填，请输入库存!" />)}
        </FormItem>
        <FormItem {...formItemLayoutContent} label="产品描述">
          {getFieldDecorator("Content", {
            rules: [
              {
                required: true
              }
            ]
          })(
            <div className="editor-wrap">
              <BraftEditor {...editorProps} />
            </div>
          )}
        </FormItem>

        <FormItem {...formItemLayout} label="产品缩略图">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this, "GoodsIcon")}
          >
            {goods.GoodsIcon ? (
              <img
                src={goods.GoodsIcon}
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
        <FormItem {...formItemLayout} label="产品详情图">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this, "GoodsImg")}
          >
            {goods.GoodsImg ? (
              <img
                src={goods.GoodsImg}
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
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {!this.props.goods.GoodsId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/goodslist")}
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
    const goods = props.goods;
    return {
      GoodsId: Form.createFormField({ value: goods.GoodsId }),
      GoodsName: Form.createFormField({ value: goods.GoodsName }),
      GoodsStatus: Form.createFormField({
        value: goods.GoodsStatus == 0 ? "0" : "1"
      }),
      Score: Form.createFormField({ value: goods.Score }),
      Sale: Form.createFormField({ value: goods.Sale }),
      SellStartTime: Form.createFormField({
        value: goods.SellStartTime
          ? moment(goods.SellStartTime, "YYYY-MM-DD")
          : null
      }),
      SellEndTime: Form.createFormField({
        value: goods.SellEndTime
          ? moment(goods.SellEndTime, "YYYY-MM-DD")
          : null
      }),
      MaxExchangeNum: Form.createFormField({ value: goods.MaxExchangeNum }),
      ReferePrice: Form.createFormField({ value: goods.ReferePrice }),
      Stock: Form.createFormField({ value: goods.Stock }),
      Content: Form.createFormField({ value: goods.Content })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      goodsReducer: state.goodsReducer
    };
  },
  { saveInfo, clearInfo }
)
class GoodsDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿新闻详情
    } else {
      this.props.clearInfo();
    }
  }

  componentWillUnmount() {
    this.props.clearInfo();
  }
  // 根据id查询详情
  getInfo = async GoodsId => {
    try {
      const { Info } = await getInfoById({ GoodsId });
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    return (
      <Nav>
        <FormMap goods={this.props.goodsReducer} />
      </Nav>
    );
  }
}

export default GoodsDetail;
