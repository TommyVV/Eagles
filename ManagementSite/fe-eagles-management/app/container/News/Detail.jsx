import React, { Component } from "react";
import { connect } from "react-redux";
import {
  Button,
  Input,
  InputNumber,
  Form,
  message,
  Row,
  Col,
  Select,
  Upload,
  Icon,
  Checkbox,
  DatePicker
} from "antd";
import "moment/locale/zh-cn";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getNewsInfoById, createOrEditNews } from "../../services/newsService";
import { getList } from "../../services/programaService";
import { getQuestionList } from "../../services/questionService";
import { serverConfig } from "../../constants/config/ServerConfigure";
import { fileSize, newsMap } from "../../constants/config/appconfig";
import { saveInfo, clearInfo } from "../../actions/newsAction";
import "./style.less";
import Editor from "../../components/Common/Editor";

const FormItem = Form.Item;
const Option = Select.Option;
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      newsReducer: state.newsReducer
    };
  },
  { saveInfo, clearInfo }
)
class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: false,
      fileList: [],
      attachs: []
    };
  }

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          let { news, Attachs } = this.props;
          let content = news.Content;
          content = typeof content == "string" ? content : content.toHTML();
          if (content === "<p></p>" || !content) {
            message.error("请输入内容");
          } else {
            console.log("Received values of form: ", values);
            let { StartTime, EndTime, IsTop } = values;
            let { Attachments } = news;
            let attach = {}; // 存附件对象
            let index = 0;
            Attachs.map(obj => {
              let i = index + 1;
              ++index;
              attach[`Attach${i}`] = obj.AttachmentUrl;
              attach[`AttachName${i}`] = obj.AttachmentName;
            });
            Attachments.map(obj => {
              if (obj.response) {
                let i = index + 1;
                ++index;
                const file = obj.response.Result.FileUploadResults[0];
                attach[`Attach${i}`] = file.FileUrl;
                attach[`AttachName${i}`] = file.FileName;
              }
            });
            let params = {
              Info: {
                ...news,
                ...values,
                IsTop: IsTop,
                // StartTime: moment(StartTime, "yyyy-MM-dd").format(),
                // EndTime: moment(EndTime, "yyyy-MM-dd").format(),
                ...attach,
                Content: content
              }
            };
            let { Code, Message } = await createOrEditNews(params);
            if (Code === "00") {
              let tip = this.props.news.NewsId ? "保存成功" : "创建成功";
              message.success(tip);
              hashHistory.replace("/newslist");
            } else {
              // let tip = this.props.news.NewsId ? "保存失败" : "创建失败";
              message.error(Message);
            }
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
      let Content = this.editorInstance.state.editorState.toHTML();
      this.props.saveInfo({
        ...news,
        ...getFieldsValue(),
        Attachments: info.fileList,
        Content
      });
    }
    if (info.file.status == "removed") {
      let newKeys = Attachs.filter((v, i) => {
        return i != info.file.uid;
      });
      setObj(newKeys);
      let Content = this.editorInstance.state.editorState.toHTML();
      this.props.saveInfo({
        ...news,
        ...getFieldsValue(),
        Attachments: info.fileList,
        Content
      });
    }
    if (info.file.status === "done") {
      message.success(`${info.file.name} 上传成功`);
      let { news } = this.props;
      let Content = this.editorInstance.state.editorState.toHTML();
      this.props.saveInfo({
        ...news,
        ...getFieldsValue(),
        Attachments: info.fileList,
        Content
      });
      return;
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  // 是否有试卷
  change = (attr, value) => {
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    let Content = this.editorInstance.state.editorState.toHTML();
    this.props.saveInfo({
      ...values,
      [attr]: value,
      TestId: value == "0" ? "" : values.TestId,
      Content
    });
  };
  // 选分类
  changeBox = (attr, e) => {
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    let Content = this.editorInstance.state.editorState.toHTML();
    this.props.saveInfo({ ...values, [attr]: e.target.checked ? "1" : "0", Content });
  };
  // 新闻封面
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
        let Content = this.editorInstance.state.editorState.toHTML();
        this.props.saveInfo({ ...values, NewsImg: imageUrl, Content });
      } else {
        message.error(`${Message}`);
      }
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  changeContent = content => {
    const { getFieldsValue } = this.props.form;
    this.props.saveInfo({
      ...getFieldsValue(),
      Content: content
    });
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { news, programaList, questionList, Attachs } = this.props; //是否显示试卷列表
    let external = news == null ? false : news.IsExternal == 1 ? true : false;
    console.log("保存的附件：", Attachs);
    let fileList = [];
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
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 8 }
      }
    };
    const formItemLayoutContent = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 19 }
      }
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
                message: "必填，请输入标题，不超过50个字"
              }
            ]
          })(<Input placeholder="必填，请输入标题" maxLength={50} />)}
        </FormItem>
        <FormItem {...formItemLayout} label="类型">
          {getFieldDecorator("NewsType")(
            <Select>
              <Option value="0">新闻</Option>
              <Option value="1">会议</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="作者">
          {getFieldDecorator("Author", {
            rules: [
              {
                required: true
              }
            ]
          })(<Input placeholder="必填，请输入作者" />)}
        </FormItem>

        <FormItem {...formItemLayout} label="来源">
          {getFieldDecorator("Source", {
            rules: [
              {
                required: true,
                message: "必填，请输入来源"
              }
            ]
          })(<Input placeholder="必填，请输入新闻来源" />)}
        </FormItem>
        {/* <FormItem label="生效时间" {...formItemLayoutDate}>
          <Col span={6}>
            <FormItem>{getFieldDecorator("StartTime")(<DatePicker />)}</FormItem>
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
        </FormItem> */}
        <FormItem {...formItemLayout} label="新闻封面">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this)}
          >
            {news.NewsImg ? (
              <img src={news.NewsImg} alt="avatar" style={{ width: "100%" }} />
            ) : (
                <div>
                  <Icon type={this.state.loading ? "loading" : "plus"} />
                  <div className="ant-upload-text">上传</div>
                </div>
              )}
          </Upload>
          <b
            style={{
              position: "absolute",
              width: "200px",
              top: "-112px",
              left: "120px"
            }}
          >
            {" "}
            新闻封面建议长宽比为1.6 : 1
          </b>
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="外部链接"
          style={{ display: external ? "block" : "none" }}
        >
          {getFieldDecorator("ExternalUrl", {
            rules: [
              {
                message: "外部链接"
              }
            ]
          })(<Input placeholder="外部链接" />)}
        </FormItem>
        <FormItem
          {...formItemLayoutContent}
          label="内容"
          className="label-star"
          style={{ display: !external ? "block" : "none" }}
        >
          <Editor
            content={news.Content}
            text={"必填，请输入内容"}
            ref={instance => (this.editorInstance = instance)}
            onChange={this.changeContent.bind(this)}
          />
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="是否有试卷"
          style={{ display: !external ? "block" : "none" }}
        >
          {getFieldDecorator("isTest")(
            <Select onChange={this.change.bind(this, "isTest")}>
              <Option value="0">否</Option>
              <Option value="1">是</Option>
            </Select>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="试卷选择"
          style={{ display: news.isTest == "1" ? "block" : "none" }}
        >
          {getFieldDecorator("TestId")(
            <Select>
              {questionList.length &&
                questionList.map((obj, index) => {
                  return (
                    <Option key={index} value={obj.ExercisesId + ""}>
                      {obj.ExercisesName}
                    </Option>
                  );
                })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="是否置顶">
          {getFieldDecorator("IsTop")(
            <Select>
              <Option value={0}>否</Option>
              <Option value={1}>是</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="所属栏目">
          {getFieldDecorator("ModuleId")(
            <Select>
              {programaList.length &&
                programaList.map((obj, index) => {
                  return (
                    <Option key={index} value={obj.ColumnId + ""}>
                      {obj.ColumnName}
                    </Option>
                  );
                })}
            </Select>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="分类"
          style={{ display: !external ? "block" : "none" }}
        >
          <Row>
            {/* <Col span={8}>
              {news.IsImage == "0" || news.IsImage == "1" ? (
                <Checkbox
                  checked={news.IsImage == "1" ? true : false}
                  onChange={this.changeBox.bind(this, "IsImage")}
                >
                  有图片
                </Checkbox>
              ) : (
                  <Checkbox onChange={this.changeBox.bind(this, "IsImage")}>
                    有图片
                </Checkbox>
                )}
            </Col> */}
            <Col span={8}>
              {news.IsVideo == "0" || news.IsVideo == "1" ? (
                <Checkbox
                  checked={news.IsVideo == "1" ? true : false}
                  onChange={this.changeBox.bind(this, "IsVideo")}
                >
                  有视频
                </Checkbox>
              ) : (
                  <Checkbox onChange={this.changeBox.bind(this, "IsVideo")}>
                    有视频
                </Checkbox>
                )}
            </Col>
            <Col span={8}>
              {news.CanStudy == "0" || news.CanStudy == "1" ? (
                <Checkbox
                  checked={news.CanStudy == "1" ? true : false}
                  onChange={this.changeBox.bind(this, "CanStudy")}
                >
                  允许学习
                </Checkbox>
              ) : (
                  <Checkbox onChange={this.changeBox.bind(this, "CanStudy")}>
                    允许学习
                </Checkbox>
                )}
            </Col>
            {/* <Col span={8}>
              {news.IsAttach == "0" || news.IsAttach == "1" ? (
                <Checkbox
                  checked={news.IsAttach == "1" ? true : false}
                  onChange={this.changeBox.bind(this, "IsAttach")}
                >
                  有附件
                </Checkbox>
              ) : (
                  <Checkbox onChange={this.changeBox.bind(this, "IsAttach")}>
                    有附件
                </Checkbox>
                )}
            </Col> */}
          </Row>
          {/* <Row>
            <Col span={8}>
              {news.IsClass == "0" || news.IsClass == "1" ? (
                <Checkbox
                  checked={news.IsClass == "1" ? true : false}
                  onChange={this.changeBox.bind(this, "IsClass")}
                >
                  有课件
                </Checkbox>
              ) : (
                  <Checkbox onChange={this.changeBox.bind(this, "IsClass")}>
                    有课件
                </Checkbox>
                )}
            </Col>
          </Row> */}
        </FormItem>

        <FormItem
          {...formItemLayout}
          label="积分奖励"
          style={{ display: news.CanStudy == "1" ? null : "none" }}
        >
          {getFieldDecorator("RewardsScore")(
            <InputNumber
              placeholder="请输入积分奖励"
              min={0}
              style={{ width: "100%" }}
            />
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="学习时间"
          style={{ display: news.CanStudy == "1" ? null : "none" }}
        >
          {getFieldDecorator("StudyTime")(
            <InputNumber
              placeholder="请输入学习时间，单位（分钟）"
              min={0}
              style={{ width: "100%" }}
            />
          )}
        </FormItem>
        {/* <FormItem {...formItemLayout} label="附件" style={{ display: news.IsAttach == "1" ? null : "none" }}> */}
        <FormItem {...formItemLayout} label="附件">
          <Upload
            name="file"
            listType="text"
            showUploadList={true}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            onChange={this.handleChange.bind(this)}
            fileList={fileList}
          >
            <Button>
              <Icon type="upload" /> 点击上传
            </Button>
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
                {this.props.news.NewsId ? "更新" : "新建"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/newslist")}
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
    const { news, isRewardWrapper, programaList } = props;
    console.log("新闻详情数据回显 - ", news);
    return {
      NewsId: Form.createFormField({ value: news.NewsId ? news.NewsId : "" }),
      NewsName: Form.createFormField({
        value: news.NewsName
      }),
      RewardsScore: Form.createFormField({
        value: news.RewardsScore
      }),
      StudyTime: Form.createFormField({
        value: news.StudyTime
      }),
      NewsType: Form.createFormField({
        value: news.NewsType ? news.NewsType + "" : "0"
      }),
      Author: Form.createFormField({
        value: news.Author
      }),
      Source: Form.createFormField({
        value: news.Source
      }),
      // StartTime: Form.createFormField({
      //   value: news.StartTime ? moment(news.StartTime, "YYYY-MM-DD") : null
      // }),
      // EndTime: Form.createFormField({
      //   value: news.EndTime ? moment(news.EndTime, "YYYY-MM-DD") : null
      // }),
      isTest: Form.createFormField({
        value: news.isTest ? news.isTest + "" : "0"
      }),
      TestId: Form.createFormField({
        value: news.TestId ? news.TestId + "" : ""
      }),
      Content: Form.createFormField({
        value: news.Content
      }),
      ModuleId: Form.createFormField({
        value: news.ModuleId ? news.ModuleId + "" : ""
      }),
      IsTop: Form.createFormField({
        value: news.IsTop ? news.IsTop : 0
      }),
      ExternalUrl: Form.createFormField({
        value: news.ExternalUrl
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      newsReducer: state.newsReducer
    };
  },
  { saveInfo, clearInfo }
)
class NewsDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      programaList: [], // 栏目列表
      questionList: [], // 试卷列表
      Attachments: []
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿新闻详情
    } else {
      this.props.clearInfo();
      // 拿栏目、试卷详情
      this.getProgramaList();
      if (this.state.questionList.length == 0) {
        this.getQuestionList();
      }

    }
  }

  componentWillUnmount() {
    this.props.clearInfo();
  }

  // 查询栏目列表
  getProgramaList = async () => {
    const { List } = await getList({ IsPublic: true });
    this.setState({ programaList: List });
  };
  // 查询试卷列表
  getQuestionList = async () => {
    const res = await getQuestionList({ ExercisesType: 10, IsUse: 0 });
    console.log("getQuestionList", res);
    this.setState({ questionList: res.List });
  };
  // 根据id查询详情
  getInfo = async NewsId => {
    try {
      // 说明有试卷
      this.getQuestionList();
      this.getProgramaList();
      let { Info } = await getNewsInfoById({ NewsId });
      let questionList = this.state.questionList;
      questionList.push({ "ExercisesId": Info.TestId, "ExercisesName": Info.TestName });
      console.log("newsDetails", Info);
      Info = {
        ...Info,
        isTest: Info.TestId ? "1" : "0"
      };

      this.state.Attachments = Info.Attachments;
      this.setState({ questionList: questionList });
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    const { programaList, questionList, Attachments } = this.state;
    return (
      <Nav>
        <FormMap
          news={this.props.newsReducer}
          Attachs={Attachments}
          programaList={programaList}
          questionList={questionList}
          setObj={attachs => {
            this.state.Attachments = attachs;
          }}
        />
      </Nav>
    );
  }
}

export default NewsDetail;
