import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  DatePicker
} from "antd";
import moment from "moment";
import "moment/locale/zh-cn";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/systemService";
import { serverConfig } from "../../constants/config/ServerConfigure";
// 引入编辑器以及编辑器样式
import BraftEditor from "braft-editor";
import "braft-editor/dist/braft.css";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
const TextArea = Input.TextArea;
class Base extends Component {
  constructor(props) {
    super(props);
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { system } = this.props;
          const { RepeatTime, NoticeTime } = values;
          let params = {
            Info: {
              ...system,
              ...values,
              NoticeTime: new Date(
                moment(NoticeTime, "yyyy-MM-dd").format()
              ).format("yyyy-MM-dd")
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = system.NewsId ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/systemlist");
          } else {
            let tip = system.NewsId ? "保存失败" : "创建失败";
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
  changDate(value) {
    console.log(moment(value, "yy-MM-dd").format());
    console.log(
      new Date(moment(value, "yy-MM-dd").format()).format("yyyy-MM-dd")
    );

    // const { system } = this.props;
    // const { RepeatTime, NoticeTime } = values;
    // const system={

    // };
    // if (value == "0") {
    // }
  }
  render() {
    const { getFieldDecorator, getFieldsValue, setFieldsValue } = this.props.form;
    const { system } = this.props;
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
      placeholder: "请输入内容",
      initialContent: system.HtmlDesc,
      media: {
        validateFn: file => {
          return file.size < 1024 * 1024 * 5;
        },
        uploadFn: async param => {
          // const res=await uploadFile(file);
          console.log(param);
          let formData = new FormData();
          formData.append("file", param.file);
          var request = new XMLHttpRequest();
          request.open(
            "POST",
            serverConfig.API_SERVER + serverConfig.FILE.UPLOAD
          );
          request.send(formData);
          request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
              let { Result } = JSON.parse(request.responseText);
              let { FileId, FileUrl, FileName } = Result.FileUploadResults[0];
              // 上传成功后调用param.success并传入上传后的文件地址
              param.success({
                url: FileUrl,
                meta: {
                  id: FileId,
                  title: FileName,
                  alt: FileName,
                  loop: false, // 指定音视频是否循环播放
                  autoPlay: false, // 指定音视频是否自动播放
                  controls: false // 指定音视频是否显示控制栏
                  // poster: "http://xxx/xx.png" // 指定视频播放器的封面
                }
              });
            }
          };
        },
        onInsert: files => {
          console.log(files);
        }
      },
      onChange: (HtmlDesc, info) => {
        setFieldsValue({ HtmlDesc });
        console.log("消息内容：", getFieldsValue());
      }
      // onRawChange: this.handleRawChange
    };
    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("NewsId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="标题">
          {getFieldDecorator("NewsName", {
            rules: [
              {
                required: true,
                message: "必填，请输入标题"
              }
            ]
          })(<Input placeholder="必填，请输入标题" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="类型">
          {getFieldDecorator("NewsType")(
            <Select>
              <Option value="00">领袖诞辰</Option>
              <Option value="10">系统通知</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="重复类型">
          {getFieldDecorator("RepeatTime")(
            <Select onChange={this.changDate}>
              <Option value="0">每年</Option>
              <Option value="1">仅一次</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="提醒时间">
          {getFieldDecorator("NoticeTime", {
            rules: [
              {
                required: true,
                message: "必填，请选择提醒时间"
              }
            ]
          })(
            <DatePicker placeholder="请选择提醒时间" />
          )}
        </FormItem>
        <FormItem {...formItemLayoutContent} label="内容">
          {getFieldDecorator("HtmlDesc", {
            rules: [
              {
                required: true,
                message: "必填，请输入内容"
              }
            ]
          })(<div className="editor-wrap">
            <BraftEditor {...editorProps} />
          </div>)}
        </FormItem>
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("Status")(
            <Select>
              <Option value="0">正常</Option>
              <Option value="1">禁用</Option>
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
                {!system.NewsId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/systemlist")}
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
    const { system } = props;
    console.log("详情数据回显 - ", system);
    return {
      NewsId: Form.createFormField({
        value: system.NewsId
      }),
      NewsName: Form.createFormField({
        value: system.NewsName
      }),
      NewsType: Form.createFormField({
        value: system.NewsType ? system.NewsType + "" : "00"
      }),
      RepeatTime: Form.createFormField({
        value: system.RepeatTime ? system.RepeatTime + "" : "0"
      }),
      NoticeTime: Form.createFormField({
        value: system.NoticeTime
          ? moment(system.NoticeTime, "YYYY-MM-DD")
          : null
      }),
      HtmlDesc: Form.createFormField({
        value: system.HtmlDesc
      }),
      Status: Form.createFormField({
        value: system.Status ? system.Status + "" : "0"
      })
    };
  }
})(Base);
class SystemDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      system: {}
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    }
  }

  // 根据id查询详情
  getInfo = async NewsId => {
    try {
      const { News } = await getInfoById({ NewsId });
      this.setState({ system: News });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        <FormMap system={this.state.system} />
      </Nav>
    );
  }
}

export default SystemDetail;
