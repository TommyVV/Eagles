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
import moment from "moment";
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
      <Form>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("NewsId")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="标题">
          {getFieldDecorator("NewsName", {
            rules: [
              {
                required: true,
                message: "必填，请输入标题，不超过50个字"
              }
            ]
          })(<Input placeholder="必填，请输入标题" maxLength={50} disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="类型">
          {getFieldDecorator("NewsType")(
            <Select disabled>
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
          })(<Input placeholder="必填，请输入作者" disabled />)}
        </FormItem>

        <FormItem {...formItemLayout} label="来源">
          {getFieldDecorator("Source", {
            rules: [
              {
                required: true,
                message: "必填，请输入来源"
              }
            ]
          })(<Input placeholder="必填，请输入新闻来源" disabled />)}
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
            disabled={true}
          >
            {news.NewsImg ? (
              <img
                src={news.NewsImg}
                alt="avatar"
                style={{ width: "100%" }}
                disabled
              />
            ) : (
              <div disabled>
                <Icon type={this.state.loading ? "loading" : "plus"} />
                <div className="ant-upload-text">上传</div>
              </div>
            )}
          </Upload>
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
          })(<Input placeholder="外部链接" disabled />)}
        </FormItem>
        <FormItem
          {...formItemLayoutContent}
          label="内容"
          style={{ display: !external ? "block" : "none" }}
        >
          {getFieldDecorator("Content")(
            <div className="editor-wrap">
              <div dangerouslySetInnerHTML={{ __html: news.Content }} />
            </div>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="是否有试卷"
          style={{ display: !external ? "block" : "none" }}
        >
          {getFieldDecorator("isTest")(
            <Select disabled>
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
            <Select disabled>
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
            <Select disabled>
              <Option value={0}>否</Option>
              <Option value={1}>是</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="所属栏目">
          {getFieldDecorator("ModuleId")(
            <Select disabled>
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
                <Checkbox disabled checked={news.IsVideo == "1" ? true : false}>
                  有视频
                </Checkbox>
              ) : (
                <Checkbox disabled>有视频</Checkbox>
              )}
            </Col>
            <Col span={8}>
              {news.CanStudy == "0" || news.CanStudy == "1" ? (
                <Checkbox
                  disabled
                  checked={news.CanStudy == "1" ? true : false}
                >
                  允许学习
                </Checkbox>
              ) : (
                <Checkbox disabled>允许学习</Checkbox>
              )}
            </Col>
          </Row>
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
              disabled
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
              disabled
            />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="附件">
          <Upload
            name="file"
            listType="text"
            showUploadList={true}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            fileList={fileList}
            disabled={true}
          />
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
class NewsOrgDetail extends Component {
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
      this.getQuestionList();
    }
  }

  componentWillUnmount() {
    this.props.clearInfo();
  }

  // 查询栏目列表
  getProgramaList = async () => {
    const { List } = await getList({ IsPublic: true });
    console.log("getProgramaList", List);
    this.setState({ programaList: List });
  };
  // 查询试卷列表
  getQuestionList = async () => {
    const res = await getQuestionList({ ExercisesType: 5 });
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

export default NewsOrgDetail;
