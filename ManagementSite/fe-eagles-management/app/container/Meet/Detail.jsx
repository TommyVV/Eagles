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
  Table
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
const { TextArea } = Input;

class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: []
    };

    this.columns = [
      {
        title: "参与人员姓名",
        dataIndex: "name"
      },
      {
        title: "联系电话",
        dataIndex: "phone"
      },
      {
        title: "是否为系统人员",
        dataIndex: "member"
      },
      {
        title: "操作",
        id: "1",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/project/detail/${record.projectId}`)
                }
              >
                删除
              </a>
            </div>
          );
        }
      }
    ];
    this.data = [
      {
        key: "1",
        name: "张三",
        phone: "15555555555",
        member: "是",
        id: "1"
      },
      {
        key: "2",
        name: "张三",
        phone: "15555555555",
        member: "是",
        id: "2"
      },
      {
        key: "3",
        name: "张三",
        phone: "15555555555",
        member: "否",
        id: "3"
      }
    ];
    this.getListConfig = {
      requestPage: 1,
      pageSize: 6,
      keyword: ""
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

  render() {
    const { getFieldDecorator } = this.props.form;
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
    const { selectedRowKeys, pageConfig, projectList } = this.state;
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
    const formItemLayoutTable = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 16 }
      }
    };
    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("systemId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="会议名称">
          {getFieldDecorator("num", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="发起人">
          {getFieldDecorator("password", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem label="发起时间" {...formItemLayoutDate}>
          <Col span={6}>
            <FormItem>
              {getFieldDecorator("startTime", {
                rules: [
                  {
                    required: true,
                    message: "必填，20字以内!",
                    pattern: /^(?!.{21}|\s*$)/g
                  }
                ]
              })(<DatePicker />)}
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
              {getFieldDecorator("endTime", {
                rules: [
                  {
                    required: true,
                    message: "必填，20字以内!",
                    pattern: /^(?!.{21}|\s*$)/g
                  }
                ]
              })(<DatePicker />)}
            </FormItem>
          </Col>
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
          })(<TextArea rows={4} />)}
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
        <FormItem {...formItemLayoutTable} label="参会人员">
          <Table
            dataSource={this.data}
            columns={this.columns}
            pagination={pageConfig}
            locale={{ emptyText: "暂无数据" }}
            bordered
          />
        </FormItem>
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {this.props.project.projectId === "" ? "新建" : "保存"}
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
    console.log("项目详情数据回显 - ", props);
    const project = props.project;
    return {
      intergralId: Form.createFormField({
        value: ""
      }),
      type: Form.createFormField({
        org: "0"
      })
    };
  }
})(Base);

class MeetDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      projectDetails: {} //项目详情
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    console.log(id);
    // let author = {
    //   name: this.props.user.userName,
    //   user_id: this.props.user.userId,
    //   avatar: this.props.user.avatar,
    //   open_id: this.props.user.openId
    // };
    // if (projectId) {
    //   this.getInfo(projectId, author); //当前用户排在第一位
    // } else {
    //   let projectMembers = [author];
    //   this.props.saveProjectInfo({ projectMembers });
    // }
  }

  componentWillUnmount() {
    // this.props.clearProjectInfo();
  }
  // 根据id查询详情
  getInfo = async (projectId, author) => {
    try {
      let projectDetails = await getProjectInfoById({ projectId });
      console.log("projectDetails", projectDetails);
      this.setState({ projectDetails });
      let projectMembers = [author, ...projectDetails.membersData];
      let prevDemandAuthor = {
        open_id: projectDetails.basicData.open_id,
        create: true
      };
      this.props.saveProjectInfo({
        projectId,
        projectMembers,
        // prevDemandAuthor,
        open_id: projectDetails.basicData.open_id,
        projectName: projectDetails.basicData.projectName,
        requirementId: projectDetails.basicData.requirementId,
        requirementName: projectDetails.basicData.requirementName
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    const { projectDetails } = this.state;
    return (
      <Nav>
        <FormMap project={projectDetails} />
      </Nav>
    );
  }
}

export default MeetDetail;
