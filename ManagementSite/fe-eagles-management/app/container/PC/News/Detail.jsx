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
import {
  createProject,
  getProjectInfoById,
  getFileList
} from "../../../services/projectService";
import Crop from "../../../components/PC/Crop";
import "./style.less";
import { getNewsInfoById } from "../../../services/newsService";

const FormItem = Form.Item;
const Option = Select.Option;
const { TextArea } = Input;

class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showCrop: false, //裁剪图片
      loading: false
    };
  }

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          let { projectMembers } = this.props.project;
          let newProjectMembers = projectMembers.filter(
            v => v.user_id !== this.props.user.userId
          ); //删除本人
          let { projectName } = values;
          let { code } = await createProject({
            ...values,
            ...this.props.project,
            projectName,
            projectMembers: JSON.stringify(newProjectMembers)
          });
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
      message.error("You can only upload JPG file!");
    }
    const isLt2M = file.size / 1024 / 1024 < 2;
    if (!isLt2M) {
      message.error("Image must smaller than 2MB!");
    }
    return isJPG && isLt2M;
  }
  handleChange = info => {
    if (info.file.status === "正在上传 ") {
      this.setState({ loading: true });
      return;
    }
    if (info.file.status === "done") {
      // Get this url from response in real world.
      getBase64(info.file.originFileObj, imageUrl =>
        this.setState({
          imageUrl,
          loading: false
        })
      );
    }
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { showCrop } = this.state;
    console.log("props - ", this.props);
    console.log(
      "StarTime - ",
      new Date(this.props.news.StarTime).format("yyyy-MM-dd")
    );
    const props = {
      name: "file",
      action: "//jsonplaceholder.typicode.com/posts/",
      headers: {
        authorization: "authorization-text"
      },
      onChange(info) {
        if (info.file.status !== "uploading") {
          console.log(info.file, info.fileList);
        }
        if (info.file.status === "done") {
          message.success(`${info.file.name} file uploaded successfully`);
        } else if (info.file.status === "error") {
          message.error(`${info.file.name} file upload failed.`);
        }
      }
    };
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 10 }
      }
    };
    const formItemLayoutDate = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 12 }
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
            <FormItem>
              {this.props.news.StarTime ? (
                <DatePicker
                  defaultValue={moment(
                    new Date(this.props.news.StarTime).format("yyyy-MM-dd"),
                    "YYYY-MM-DD"
                  )}
                />
              ) : (
                <DatePicker />
              )}
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
        <FormItem {...formItemLayout} label="新闻封面">
          {getFieldDecorator("NewsImg")(
            <Upload
              name="avatar"
              listType="picture-card"
              className="avatar-uploader"
              showUploadList={false}
              action="//jsonplaceholder.typicode.com/posts/"
              beforeUpload={this.beforeUpload}
              onChange={this.handleChange}
            >
              {this.props.news.NewsImg ? (
                <img
                  src={this.props.news.NewsImg}
                  alt="avatar"
                  style={{ width: "100%" }}
                />
              ) : (
                <div>
                  <Icon type={this.state.loading ? "loading" : "plus"} />
                  <div className="ant-upload-text">Upload</div>
                </div>
              )}
            </Upload>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="内容">
          {getFieldDecorator("price", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<TextArea rows={6} />)}
        </FormItem>
        <FormItem {...formItemLayout} label="习题选择">
          {getFieldDecorator("exercise")(
            <Select>
              <Option value="0">习题一</Option>
              <Option value="1">习题二</Option>
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
            <Col span={2} offset={4}>
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
        {showCrop ? (
          <Crop
            handleFile={() => this.handleFile("avatar")}
            onCancel={() =>
              this.setState({
                ["showCrop"]: false
              })
            }
          />
        ) : null}
      </Form>
    );
  }
}

const FormMap = Form.create({
  mapPropsToFields: props => {
    const news = props.news;
    console.log("新闻详情数据回显 - ", news);
    return {
      NewsId: Form.createFormField({ value: news.NewsId ? news.NewsId : "" }),
      NewsName: Form.createFormField({
        value: news.NewsName ? news.NewsName : ""
      }),
      Source: Form.createFormField({
        value: news.Source ? news.Source : ""
      }),
      // StartTime: Form.createFormField({
      //   value: news.StartTime ? moment(news.StartTime,"YYYY-MM-DD") : ""
      // }),
      state: Form.createFormField({ value: "0" })
    };
  }
})(Base);

class NewsDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      newsDetail: {} //新闻详情
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿新闻详情
    }
  }

  componentWillUnmount() {
    // this.props.clearProjectInfo();
  }
  // 根据id查询详情
  getInfo = async NewsId => {
    try {
      const { Info } = await getNewsInfoById({ NewsId });
      console.log("projectDetails", Info);
      this.setState({ newsDetail: Info });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    const { newsDetail } = this.state;
    return (
      <Nav>
        <FormMap news={newsDetail} />
      </Nav>
    );
  }
}

export default NewsDetail;
