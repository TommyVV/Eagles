import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Avatar,
  Icon
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import {
  createProject,
  getProjectInfoById,
  getFileList
} from "../../../services/projectService";
import Crop from "../../../components/PC/Crop";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;

class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showCrop: false, //裁剪图片
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

  render() {
    const { getFieldDecorator } = this.props.form;
    const { showCrop } = this.state;
    console.log("members - ", this.props);
    const formItemLayout = {
      labelCol: {
        xl: { span: 4 }
      },
      wrapperCol: {
        xl: { span: 6 }
      }
    };
    return (
      <div className="create_pro_form">
        <Form onSubmit={this.handleSubmit}>
          <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
            {getFieldDecorator("projectId")(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="所属机构">
            {getFieldDecorator("branch")(
              <Select>
                <Option value="0">第一支部</Option>
                <Option value="1">第二支部</Option>
                <Option value="2">第三支部</Option>
              </Select>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="姓名">
            {getFieldDecorator("name", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="性别">
            {getFieldDecorator("sex")(
              <Select>
                <Option value="0">男</Option>
                <Option value="1">女</Option>
              </Select>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="头像">
            {getFieldDecorator("avatar")(
              <span className="avatar-uploader  self-style">
                <div className="ant-upload ant-upload-select ant-upload-select-picture-card">
                  <span
                    className="ant-upload"
                    onClick={() => this.setState({ ["showCrop"]: true })}
                  >
                    <div>
                      <Icon type="plus" />
                      <div className="ant-upload-text">上传头像</div>
                    </div>
                  </span>
                </div>
              </span>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="民族">
            {getFieldDecorator("nation", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="出生日期">
            {getFieldDecorator("birthday", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="籍贯">
            {getFieldDecorator("place", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="常住地址">
            {getFieldDecorator("address", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="联系电话">
            {getFieldDecorator("phone", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="身份证">
            {getFieldDecorator("card", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="学历">
            {getFieldDecorator("education", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="毕业院校">
            {getFieldDecorator("school", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="工作单位">
            {getFieldDecorator("work", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="部门、职务">
            {getFieldDecorator("department", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="转预备党员日期">
            {getFieldDecorator("toPreDate", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="转正式党员日期">
            {getFieldDecorator("toMemDate", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="人员类别（正式/预备党员）">
            {getFieldDecorator("memType")(
              <Select>
                <Option value="0">预备党员</Option>
                <Option value="1">正式党员</Option>
              </Select>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="党费缴纳情况">
            {getFieldDecorator("pay")(
              <Select>
                <Option value="0">已缴纳</Option>
                <Option value="1">未缴纳</Option>
              </Select>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="党籍状态">
            {getFieldDecorator("state")(
              <Select>
                <Option value="0">正常</Option>
                <Option value="1">预备</Option>
                <Option value="1">开除</Option>
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
      </div>
    );
  }
}

const FormMap = Form.create({
  mapPropsToFields: props => {
    console.log("项目详情数据回显 - ", props);
    const project = props.project;
    return {
      projectId: Form.createFormField({ value: "" }),
      branch: Form.createFormField({ value: "0" }),
      sex: Form.createFormField({ value: "0" }),
      memType: Form.createFormField({ value: "0" }),
      pay: Form.createFormField({ value: "0" }),
      state: Form.createFormField({ value: "0" })
    };
  }
})(Base);

class PartyMemberDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      projectDetails: {} //项目详情
    };
  }

  componentWillMount() {
    // let { projectId } = this.props.params;
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

export default PartyMemberDetail;
