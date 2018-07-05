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
  DatePicker
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
      showCrop: false //裁剪图片
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
    const { showCrop } = this.state;
    console.log("members - ", this.props);
    // const formItemLayout = {
    //   labelCol: {
    //     xl: { span: 4 }
    //   },
    //   wrapperCol: {
    //     xl: { span: 6 }
    //   }
    // };
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
    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("intergralId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="商品名称">
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
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("state")(
            <Select>
              <Option value="0">上架</Option>
              <Option value="1">下架</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="所需积分">
          {getFieldDecorator("intergral", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("state")(
            <Select>
              <Option value="0">可用</Option>
              <Option value="1">不可用</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="已售">
          {getFieldDecorator("sold", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem label="销售时间" {...formItemLayoutDate}>
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
        <FormItem {...formItemLayout} label="每人允许兑换的最大数量">
          {getFieldDecorator("max", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="商品参考价格">
          {getFieldDecorator("price", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
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
        <FormItem {...formItemLayout} label="库存">
          {getFieldDecorator("stock", {
            rules: [
              {
                required: true,
                message: "必填，20字以内!",
                pattern: /^(?!.{21}|\s*$)/g
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="产品缩略图">
          {getFieldDecorator("view")(
            <span className="avatar-uploader  self-style">
              <div className="ant-upload ant-upload-select ant-upload-select-picture-card">
                <span
                  className="ant-upload"
                  onClick={() => this.setState({ ["showCrop"]: true })}
                >
                  <div>
                    <Icon type="plus" />
                    <div className="ant-upload-text">上传</div>
                  </div>
                </span>
              </div>
            </span>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="产品详情图">
          {getFieldDecorator("detailImg")(
            <span className="avatar-uploader  self-style">
              <div className="ant-upload ant-upload-select ant-upload-select-picture-card">
                <span
                  className="ant-upload"
                  onClick={() => this.setState({ ["showCrop"]: true })}
                >
                  <div>
                    <Icon type="plus" />
                    <div className="ant-upload-text">上传</div>
                  </div>
                </span>
              </div>
            </span>
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
    );
  }
}

const FormMap = Form.create({
  mapPropsToFields: props => {
    console.log("项目详情数据回显 - ", props);
    const project = props.project;
    return {
      // intergralId: Form.createFormField({ value: "" }),
      // type: Form.createFormField({ value: "0" }),
      // state: Form.createFormField({ value: "0" })
    };
  }
})(Base);

class GoodsDetail extends Component {
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

export default GoodsDetail;
