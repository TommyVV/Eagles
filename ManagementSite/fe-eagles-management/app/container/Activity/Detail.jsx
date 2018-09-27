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
  DatePicker,
  Checkbox
} from "antd";
import moment from "moment";
import "moment/locale/zh-cn";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/activityService";
import { getQuestionList } from "../../services/questionService";
import { serverConfig } from "../../constants/config/ServerConfigure";
import { fileSize, newsMap } from "../../constants/config/appconfig";
import { saveInfo, clearInfo } from "../../actions/activityAction";
import "./style.less";
import Editor from "../../components/Common/Editor";

const FormItem = Form.Item;
const Option = Select.Option;
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      activityReducer: state.activityReducer
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

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          let { BeginTime, EndTime } = values;
          if (moment(BeginTime).isBefore(EndTime)) {
            let content=this.editorInstance.state.editorState.toHTML();
            if (content === "<p></p>" || !content) {
              message.error("请输入活动内容");
            }else{
              const { news } = this.props;
              let params = {
                DetailInfo: {
                  ...news,
                  ...values,
                  BeginTime: moment(BeginTime, "yyyy-MM-dd").format(),
                  EndTime: moment(EndTime, "yyyy-MM-dd").format(),
                  Content: content
                }
              };
              let { Code, Message } = await createOrEdit(params);
              if (Code === "00") {
                let tip = news.ActivityTaskId ? "保存成功" : "创建成功";
                message.success(tip);
                hashHistory.replace("/activitylist");
              } else {
                // let tip = news.ActivityTaskId ? "保存失败" : "创建失败";
                message.error(Message);
              }
            }
          } else {
            message.error("开始时间必须小于结束时间");
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

  // 封面
  onChangeImage = info => {
    console.log(info.file, info.fileList);
    if (info.file.status === "done") {
      const { Code, Result, Message } = info.file.response;
      if (Code == "00") {
        message.success(`${info.file.name} 上传成功`);
        const imageUrl = Result.FileUploadResults[0].FileUrl;
        // 保存数据
        let { getFieldsValue } = this.props.form;
        let values = getFieldsValue();
        this.props.saveInfo({ ...values, ImageUrl: imageUrl });
      } else {
        message.error(`${Message}`);
      }
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  disabledDate(current){
    return current && current < moment().startOf('day');
  }
  render() {
    const {
      getFieldDecorator,
      setFieldsValue,
      getFieldsValue
    } = this.props.form;
    const { news, List } = this.props; //是否显示试卷列表

    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 3 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 10 }
      }
    };
    const formItemLayoutDate = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 3 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 12 }
      }
    };
    const formItemLayoutContent = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 3 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 19 }
      }
    };
    // 编辑器属性
    const editorProps = {
      height: 300,
      contentFormat: "html",
      initialContent: news.Content,
      placeholder: "必填，请输入活动内容",
      onChange: Content => {
        if (this.editorInstance.isEmpty()) {
          setFieldsValue({ Content: "" });
        } else {
          setFieldsValue({ Content });
        }
        console.log("新闻内容：", getFieldsValue());
      },
      media: {
        validateFn: file => {
          return file.size < fileSize;
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
          request.onreadystatechange = function() {
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
      }
      // onRawChange: this.handleRawChange
    };
    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("ActivityTaskId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="标题">
          {getFieldDecorator("ActivityTaskName", {
            rules: [
              {
                required: true,
                message: "必填，请输入标题",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，请输入标题" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="类型">
          {getFieldDecorator("ActivityTaskType")(
            <Select>
              <Option value="1">投票</Option>
              <Option value="2">调查问卷</Option>
            </Select>
          )}
        </FormItem>
        <FormItem label="生效时间" {...formItemLayoutDate}>
          <Col span={6}>
            <FormItem>
              {getFieldDecorator("BeginTime")(<DatePicker disabledDate={this.disabledDate} placeholder="请选择开始时间"/>)}
            </FormItem>
          </Col>
          <Col span={1}>
            <span
              style={{
                display: "inline-block",
                width: "100%",
                textAlign: "left"
              }}
            >
              -
            </span>
          </Col>
          <Col span={6}>
            <FormItem>{getFieldDecorator("EndTime")(<DatePicker disabledDate={this.disabledDate} placeholder="请选择结束时间"/>)}</FormItem>
          </Col>
        </FormItem>
        <FormItem {...formItemLayoutContent} label="内容">
          {getFieldDecorator("Content")(
            <Editor
              content={news.Content}
              text={"必填，请输入活动内容"}
              ref={instance => (this.editorInstance = instance)}
            />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="最大参与人数">
          {getFieldDecorator("MaxPartakePeople")(
            <Input placeholder="请输入最大参与人数" />
          )}
        </FormItem>

        <FormItem {...formItemLayout} label="试卷选择">
          {getFieldDecorator("ExampleId")(
            <Select>
              {List.map((o, i) => {
                return (
                  <Option value={o.ExercisesId} key={i}>
                    {o.ExercisesName}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="图片">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this)}
          >
            {news.ImageUrl ? (
              <img src={news.ImageUrl} alt="avatar" style={{ width: "100%" }} />
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
            <Col span={2} offset={2}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {news.ActivityTaskId ? "保存" : "新建"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/activitylist")}
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
    const { news } = props;
    console.log("详情数据回显 - ", news);
    return {
      ActivityTaskId: Form.createFormField({
        value: news.ActivityTaskId
      }),
      ActivityTaskName: Form.createFormField({
        value: news.ActivityTaskName
      }),
      ActivityTaskType: Form.createFormField({
        value: news.ActivityTaskType ? news.ActivityTaskType.toString() : "1"
      }),
      BeginTime: Form.createFormField({
        value: news.BeginTime ? moment(news.BeginTime, "YYYY-MM-DD") : null
      }),
      EndTime: Form.createFormField({
        value: news.EndTime ? moment(news.EndTime, "YYYY-MM-DD") : null
      }),
      Content: Form.createFormField({
        value: news.Content
      }),
      MaxPartakePeople: Form.createFormField({
        value: news.MaxPartakePeople
      }),
      ExampleId: Form.createFormField({
        value: news.ExampleId ? news.ExampleId : ""
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      activityReducer: state.activityReducer
    };
  },
  { saveInfo, clearInfo }
)
class ActivityDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [], // 习题列表
      Attachments: []
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.props.clearInfo();
      // 拿习题详情
      this.getList();
    }
  }

  componentWillUnmount() {
    this.props.clearInfo();
  }

  // 查询试卷列表
  getList = async () => {
    const { List } = await getQuestionList();
    console.log("List", List);
    this.setState({ List });
  };
  // 根据id查询详情
  getInfo = async ActivityId => {
    try {
      this.getList();
      let { Result, Code, Message } = await getInfoById({ ActivityId });
      if (Code == "00") {
        let { Info } = Result;
        console.log("newsDetails");
        Info = {
          ...Info,
          isTest: Info.TestId ? "1" : "0"
        };
        this.state.Attachments = Info.Attachments;
        this.props.saveInfo(Info);
      } else {
        message.error(Message);
      }
    } catch (e) {
      message.error("系统繁忙，请稍后再试");
      throw new Error(e);
    }
  };

  render() {
    const { List, Attachments } = this.state;
    return (
      <Nav>
        <FormMap
          news={this.props.activityReducer}
          List={List}
          setObj={attachs => {
            this.state.Attachments = attachs;
          }}
          Attachs={Attachments}
        />
      </Nav>
    );
  }
}

export default ActivityDetail;
