import React, { Component } from "react";
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
import "./style.less";
import {
  getNewsInfoById,
  createOrEditNews
} from "../../../services/newsService";
import { getQuestionList } from "../../../services/questionService";
import { getProgramaList } from "../../../services/programaService";
import { serverConfig } from "../../../constants/ServerConfigure";
// 引入编辑器以及编辑器样式
import BraftEditor from "braft-editor";
import "braft-editor/dist/braft.css";

const FormItem = Form.Item;
const Option = Select.Option;
const { TextArea } = Input;

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
            ...values,
            StarTime: moment(StarTime, "yyyy-MM-dd").format(),
            EndTime: moment(EndTime, "yyyy-MM-dd").format(),
            NewsImg: this.props.news.NewsImg
          };
          params = delete params.isTest;
          console.log("params: ", params);
          let { code } = await createOrEditNews(params);
          if (code === 0) {
            let tip = this.props.project.projectId
              ? "保存项目成功"
              : "创建项目成功";
            message.success(tip);
            hashHistory.replace("/project");
          } else {
            let tip = this.props.project.projectId
              ? "保存项目失败"
              : "创建项目失败";
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
  // 上传附件成功或者删除
  handleFile = attr => {
    let _this = this;
    this.saveInfo();
    return {
      move(list, map, fileId) {
        let idList = [];
        list.forEach(file => {
          if (file.status) {
            idList.push(map.get(file.uid));
          }
          idList.push(file.fileId);
        });
        let noUndefindArray = idList.filter(v => v);
        let { deleteList } = _this.props.agency;
        deleteList.push(fileId);
        let count = attr + "Count";
        _this.props.saveFileUrl({
          [attr]: noUndefindArray.join(";"),
          deleteList,
          [count]: list.length
        });
      },
      done(list, map, fileId) {
        // list 为当前图片list 、map为uid和fileId的关联关系
        if (attr === "avatar") {
          _this.props.saveFileUrl({ [attr]: list });
          return;
        }
        let idList = [];
        let { uploadDeleteList } = _this.props.agency;
        list.forEach(file => {
          if (file.status) {
            //从编辑中获取fileId
            idList.push(map.get(file.uid));
          }
          idList.push(file.fileId);
        });
        let noUndefindArray = idList.filter(v => v);
        uploadDeleteList.push(fileId);
        let count = attr + "Count";
        _this.props.saveFileUrl({
          [attr]: noUndefindArray.join(";"),
          [count]: noUndefindArray.length,
          uploadDeleteList
        });
      }
    };
  };

  handleCancel = attr => {
    this.setState({
      [attr]: false
    });
  };
  onChangeTime = (date, dateString) => {
    console.log(date, dateString);
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
  handleChange = info => {
    debugger;
    if (info.file.status !== "uploading") {
      console.log(info.file, info.fileList);
    }
    if (info.file.status === "done") {
      message.success(`${info.file.name} 上传成功`);
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
      onChange: this.handleChange
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
      onChange: Content => {
        console.log(Content);
        setFieldsValue({ Content });
        console.log(getFieldsValue());
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
          {getFieldDecorator("NewsImg")(
            <Upload
              name="avatar"
              listType="picture-card"
              className="avatar-uploader"
              showUploadList={false}
              action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
              beforeUpload={this.beforeUpload}
              onChange={info => {
                if (info.file.status !== "uploading") {
                  console.log(info.file, info.fileList);
                }
                if (info.file.status === "done") {
                  message.success(`${info.file.name} 上传成功`);
                  const imageUrl =
                    info.file.response.Result.FileUploadResults[0].FileUrl;
                  setNews({ ...news, NewsImg: imageUrl });
                  setFieldsValue({ NewsImg: imageUrl });
                } else if (info.file.status === "error") {
                  message.error(`${info.file.name} 上传失败`);
                }
              }}
            >
              {news.NewsImg ? (
                <img
                  src={news.NewsImg}
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
          )}
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
          {getFieldDecorator("view")(
            <Upload {...props}>
              <Button>
                <Icon type="upload" /> 点击上传
              </Button>
            </Upload>
          )}
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
                onClick={() => hashHistory.replace("/project")}
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
      NewsImg: Form.createFormField({
        value: news.NewsImg
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

class NewsDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      newsDetail: {}, //新闻详情
      programaList: [], // 栏目列表
      isRewardWrapper: false // 是否显示试卷
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿新闻详情
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
  setNews(news) {
    this.setState({
      newsDetail: news
    });
  }

  render() {
    const { newsDetail, isRewardWrapper } = this.state;
    return (
      <Nav>
        <FormMap
          news={newsDetail}
          isRewardWrapper={isRewardWrapper}
          setRewardWrapper={this.setRewardWrapper.bind(this)}
          setNews={this.setNews.bind(this)}
        />
      </Nav>
    );
  }
}

export default NewsDetail;
