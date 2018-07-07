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
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getNewsInfoById, createOrEditNews } from "../../services/newsService";
import { getProgramaList } from "../../services/programaService";
import { serverConfig } from "../../constants/ServerConfigure";
import { saveInfo, clearInfo } from "../../actions/newsAction";
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
      newsReducer: state.newsReducer
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
          let { StarTime, EndTime } = values;
          let params = {
            Info: {
              ...this.props.news,
              ...values,
              StarTime: moment(StarTime, "yyyy-MM-dd").format(),
              EndTime: moment(EndTime, "yyyy-MM-dd").format(),
              // NewsImg: this.props.news.NewsImg
            }
          };
          let { Code } = await createOrEditNews(params);
          if (Code === "00") {
            let tip = this.props.news.NewsId ? "保存新闻成功" : "创建新闻成功";
            message.success(tip);
            hashHistory.replace("/newslist");
          } else {
            let tip = this.props.news.NewsId ? "保存新闻失败" : "创建新闻失败";
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
  // 上传新闻的附件
  handleChange = info => {
    if (info.file.status !== "uploading") {
      console.log(info.file, info.fileList);
    }
    if (info.file.status === "done") {
      message.success(`${info.file.name} 上传成功`);
      let attach = {};
      info.fileList.map((obj, index) => {
        const url = obj.response.Result.FileUploadResults[0].FileUrl;
        attach[`Attach${++index}`] = url;
      });
      this.props.saveInfo({ ...this.props.news, ...attach });
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  rewardHandleChange = value => {
    const { setRewardWrapper } = this.props; //是否显示试卷列表
    // this.props.form.setFieldsValue({
    //   isTest: value,
    //   TestId: value == "0" ? "0" : ""
    // });
    setRewardWrapper(value == "1" ? true : false);
  };
  // 新闻封面
  onChangeImage = info => {
    if (info.file.status !== "uploading") {
      console.log(info.file, info.fileList);
    }
    if (info.file.status === "done") {
      message.success(`${info.file.name} 上传成功`);
      const imageUrl = info.file.response.Result.FileUploadResults[0].FileUrl;
      // 保存数据
      let { getFieldsValue } = this.props.form;
      let values = getFieldsValue();
      this.props.saveInfo({ ...values, NewsImg: imageUrl });
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
    const { news, isRewardWrapper, setNews } = this.props; //是否显示试卷列表
    const props = {
      name: "file",
      action: serverConfig.API_SERVER + serverConfig.FILE.UPLOAD,
      headers: {
        authorization: "authorization-text"
      },
      onChange: this.handleChange.bind(this)
    };
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 10 }
      }
    };
    const formItemLayoutDate = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 12 }
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
    // 编辑器属性
    const editorProps = {
      height: 300,
      contentFormat: "html",
      initialContent: news.Content,
      uploadImgShowBase64: false, // 是否使用base64
      uploadImgServer: serverConfig.API_SERVER + serverConfig.FILE.UPLOAD, // 图片上传到自己的服务器，自己需要自定义方法，todo
      onChange: Content => {
        setFieldsValue({ Content });
        console.log("新闻内容：", getFieldsValue());
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
                required: true
              }
            ]
          })(<Input placeholder="必填，请输入标题" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="来源">
          {getFieldDecorator("Source", {
            rules: [
              {
                required: true
              }
            ]
          })(<Input placeholder="必填，请输入新闻来源" />)}
        </FormItem>
        <FormItem label="生效时间" {...formItemLayoutDate}>
          <Col span={6}>
            <FormItem>{getFieldDecorator("StarTime")(<DatePicker />)}</FormItem>
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
        <FormItem {...formItemLayout} label="是否有试卷">
          {getFieldDecorator("isTest")(
            <Select onChange={this.rewardHandleChange}>
              <Option value="0">否</Option>
              <Option value="1">是</Option>
            </Select>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="试卷选择"
          style={{
            display: isRewardWrapper ? "block" : "none"
          }}
        >
          {getFieldDecorator("TestId")(
            <Select>
              <Option value="0">试卷一</Option>
              <Option value="1">试卷二</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="所属栏目">
          {getFieldDecorator("programa")(
            <Select>
              <Option value="0">栏目一</Option>
              <Option value="1">栏目二</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="分类">
          {getFieldDecorator("classify")(
            <Checkbox.Group style={{ width: "100%" }}>
              <Row>
                <Col span={8}>
                  <Checkbox value="A">新闻</Checkbox>
                </Col>
                <Col span={8}>
                  <Checkbox value="B">直播</Checkbox>
                </Col>
                <Col span={8}>
                  <Checkbox value="C">C</Checkbox>
                </Col>
                <Col span={8}>
                  <Checkbox value="D">D</Checkbox>
                </Col>
                <Col span={8}>
                  <Checkbox value="E">E</Checkbox>
                </Col>
              </Row>
            </Checkbox.Group>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="附件">
          <Upload {...props}>
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
    const { news, isRewardWrapper } = props;
    console.log("新闻详情数据回显 - ", news);
    return {
      NewsId: Form.createFormField({ value: news.NewsId ? news.NewsId : "" }),
      NewsName: Form.createFormField({
        value: news.NewsName
      }),
      Source: Form.createFormField({
        value: news.Source
      }),
      StarTime: Form.createFormField({
        value: news.StarTime ? moment(news.StarTime, "YYYY-MM-DD") : null
      }),
      EndTime: Form.createFormField({
        value: news.EndTime ? moment(news.EndTime, "YYYY-MM-DD") : null
      }),
      isTest: Form.createFormField({
        value: isRewardWrapper ? "1" : "0"
      }),
      TestId: Form.createFormField({
        value: news.TestId <= 0 ? "" : news.TestId
      }),
      Content: Form.createFormField({
        value: news.Content
      }),
      Category: Form.createFormField({
        value: news.TestId <= "0" ? "0" : "1"
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
      isRewardWrapper: false // 是否显示试卷
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿新闻详情
    }else{
      this.props.clearInfo();
    }
    // 拿栏目详情
    this.getProgramaList();
  }

  componentWillUnmount() {
    // this.props.clearProjectInfo();
  }

  // 查询栏目列表
  getProgramaList = async () => {
    const res = await getProgramaList();
    console.log("getProgramaList", res.List);
    this.setState({ programaList: res.List });
  };
  // 根据id查询详情
  getInfo = async NewsId => {
    try {
      const { Info } = await getNewsInfoById({ NewsId });
      console.log("newsDetails", Info);
      this.props.saveInfo(Info);
      // 说明有试卷
      if (Info.TestId > 0) {
        // const { List } = await getQuestionList();
        // console.log(List);
      } else {
        // const { List } = await getQuestionList();
        // console.log(List);
      }
      this.setState({
        newsDetail: Info,
        isRewardWrapper: Info.TestId > 0 ? true : false
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  setRewardWrapper(isRewardWrapper) {
    this.setState({
      isRewardWrapper
    });
  }

  render() {
    const { newsDetail, isRewardWrapper } = this.state;

    return (
      <Nav>
        <FormMap
          news={this.props.newsReducer}
          isRewardWrapper={isRewardWrapper}
          setRewardWrapper={this.setRewardWrapper.bind(this)}
        />
      </Nav>
    );
  }
}

export default NewsDetail;
