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
// 引入编辑器以及编辑器样式
import BraftEditor from "braft-editor";
import "braft-editor/dist/braft.css";

import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
const { TextArea } = Input;
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
            const { news, Attachs } = this.props;
            let { Attachments } = news;
            let attach = {}; // 存附件对象
            let index = 0;
            Attachs.map(obj => {
              let i = index + 1;
              ++index;
              attach[`Attach${i}`] = obj.AttachmentUrl;
              attach[`AttachName${i}`] = obj.AttachmentName;
            });
            Attachments && Attachments.map(obj => {
              if (obj.response) {
                let i = index + 1;
                ++index;
                const file = obj.response.Result.FileUploadResults[0];
                attach[`Attach${i}`] = file.FileUrl;
                attach[`AttachName${i}`] = file.FileName;
              }
            });

            let newArr = [...Attachs];
            if (Attachments && Attachments.length) {
              newArr = [...newArr, ...Attachments]
            }
            // if (values.ActivityTaskType == "0" && !newArr.length) {
            //   message.error("请上传附件");
            //   return;
            // }

            let params = {
              DetailInfo: {
                ...news,
                ...values,
                BeginTime: moment(BeginTime, "yyyy-MM-dd").format(),
                EndTime: moment(EndTime, "yyyy-MM-dd").format(),
                ...attach
              }
            };
            let { Code } = await createOrEdit(params);
            if (Code === "00") {
              let tip = news.ActivityTaskId ? "保存成功" : "创建成功";
              message.success(tip);
              hashHistory.replace("/activitylist");
            } else {
              let tip = news.ActivityTaskId ? "保存失败" : "创建失败";
              message.error(tip);
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
  // 上传新闻的附件
  handleChange = info => {
    console.log("上传新闻附件：", info);
    if (info.fileList.length > 4) {
      message.error("最多只能上传4个附件");
      return false;
    }
    let { news, Attachs, setObj, form } = this.props;
    let { getFieldsValue } = form;
    if (info.file.status == "uploading") {
      this.props.saveInfo({ ...news, ...getFieldsValue(), Attachments: info.fileList });
    }
    if (info.file.status == "removed") {
      let newKeys = Attachs.filter((v, i) => {
        return i != info.file.uid;
      });
      setObj(newKeys);
      this.props.saveInfo({ ...news, ...getFieldsValue(), Attachments: info.fileList });
    }
    if (info.file.status === "done") {
      message.success(`${info.file.name} 上传成功`);
      let { news } = this.props;
      this.props.saveInfo({ ...news, ...getFieldsValue(), Attachments: info.fileList });
      return;
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
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
  render() {
    const {
      getFieldDecorator,
      setFieldsValue,
      getFieldsValue
    } = this.props.form;
    const { news, List } = this.props; //是否显示试卷列表
    let fileList = [];
    news &&
      news.Attachments &&
      news.Attachments.map((o, i) => {
        if (o.lastModified) {
          fileList.push(o);
        } else {
          fileList.push({
            uid: i,
            name: o.AttachmentName || o.name,
            status: "done",
            url: o.AttachmentUrl
          });
        }
      });
    console.log(fileList);
    const props = {
      name: "file",
      action: serverConfig.API_SERVER + serverConfig.FILE.UPLOAD,
      onChange: this.handleChange.bind(this),
      fileList: fileList
    };
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
      placeholder: "必填，请输入新闻内容",
      onChange: Content => {
        setFieldsValue({ Content });
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
              <Option value="0">投票</Option>
              <Option value="1">调查问卷</Option>
            </Select>
          )}
        </FormItem>
        <FormItem label="生效时间" {...formItemLayoutDate}>
          <Col span={6}>
            <FormItem>
              {getFieldDecorator("BeginTime")(<DatePicker />)}
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
            <FormItem>{getFieldDecorator("EndTime")(<DatePicker />)}</FormItem>
          </Col>
        </FormItem>
        <FormItem {...formItemLayoutContent} label="内容">
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
        <FormItem {...formItemLayout} label="最大参与人数">
          {getFieldDecorator("MaxPartakePeople")(
            <Input placeholder="请输入最大参与人数" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="是否允许评论">
          {getFieldDecorator("IsComment")(
            <Select>
              <Option value="0">是</Option>
              <Option value="1">否</Option>
            </Select>
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
        <FormItem {...formItemLayout} label="附件">
          <Upload {...props}>
            <Button>
              <Icon type="upload" /> 上传
            </Button>
            {/* <span style={{ paddingLeft: "12px" }}>如活动类型为投票，必须上传附件</span> */}
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
        value: news.ActivityTaskType ? news.ActivityTaskType + "" : "0"
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
      IsComment: Form.createFormField({
        value: news.IsComment ? news.IsComment + "" : "0"
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

  // 查询栏目列表
  getList = async () => {
    const { List } = await getQuestionList();
    console.log("List", List);
    this.setState({ List });
  };
  // 根据id查询详情
  getInfo = async ActivityId => {
    try {
      this.getList();
      let { Info } = await getInfoById({ ActivityId });
      console.log("newsDetails", Info);
      Info = {
        ...Info,
        isTest: Info.TestId ? "1" : "0"
      };
      this.state.Attachments = Info.Attachments;
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
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
