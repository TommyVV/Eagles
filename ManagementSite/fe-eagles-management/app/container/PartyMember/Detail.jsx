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
  DatePicker
} from "antd";
import moment from "moment";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/memberService";
// import { getOrgList } from "../../services/br";
import { serverConfig } from "../../constants/config/ServerConfigure";
import { fileSize, pageMap } from "../../constants/config/appconfig";
import { saveInfo, clearInfo } from "../../actions/imageAction";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
@connect(
  state => {
    return {
      user: state.userReducer,
      imageReducer: state.imageReducer
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
  componentWillUnmount() {
    this.props.clearInfo();
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { member, Orgs } = this.props;
          // const { OrgId } = values;
          // const org = Orgs.filter(o => o.OrgId == OrgId);
          let params = {
            Info: {
              ...member,
              ...values
              // OrgName: org && org[0].OrgName
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = member.Id ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/partymemberlist");
          } else {
            let tip = member.Id ? "保存失败" : "创建失败";
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

  beforeUpload(file) {
    const reg = /^image\/(png|jpeg|jpg)$/;
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
      this.props.saveInfo({ ...values, PhotoUrl: imageUrl });
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { Orgs, member } = this.props;
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

    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("UserId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="所属支部">
          {getFieldDecorator("BranchId")(
            <Select>
              <Option value="0">第一支部</Option>
              <Option value="1">第二支部</Option>
              <Option value="2">第三支部</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="姓名">
          {getFieldDecorator("UserName", {
            rules: [
              {
                required: true,
                message: "必填，请输入姓名"
              }
            ]
          })(<Input placeholder="必填，请输入姓名" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="性别">
          {getFieldDecorator("Sex")(
            <Select>
              <Option value="0">男</Option>
              <Option value="1">女</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="头像">
          <Upload
            name="avatar"
            listType="picture-card"
            className="avatar-uploader"
            showUploadList={false}
            action={serverConfig.API_SERVER + serverConfig.FILE.UPLOAD}
            beforeUpload={this.beforeUpload}
            onChange={this.onChangeImage.bind(this)}
          >
            {member.PhotoUrl ? (
              <img
                src={member.PhotoUrl}
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
        </FormItem>
        <FormItem {...formItemLayout} label="民族">
          {getFieldDecorator("Nation")(<Input placeholder="请输入民族" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="出生日期">
          {getFieldDecorator("Birth")(
            <DatePicker
              placeholder="请选择出生日期"
              style={{ width: "100%" }}
            />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="籍贯">
          {getFieldDecorator("NativePlace")(<Input placeholder="请输入籍贯" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="户籍地址">
          {getFieldDecorator("FamilyAddress")(
            <Input placeholder="请输入户籍地址" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="常住地址">
          {getFieldDecorator("DefaultAddress")(
            <Input placeholder="请输入常住地址" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="联系电话">
          {getFieldDecorator("Phone", {
            rules: [
              {
                required: true,
                message: "必填，请输入联系电话"
              }
            ]
          })(<Input placeholder="请输入联系电话" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="身份证">
          {getFieldDecorator("IdCard", {
            rules: [
              {
                required: true,
                message: "必填，请输入身份证"
              }
            ]
          })(<Input placeholder="必填，请输入身份证" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="学历">
          {getFieldDecorator("Education")(<Input placeholder="请输入学历" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="毕业院校">
          {getFieldDecorator("GraduateSchool")(
            <Input placeholder="请输入毕业院校" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="工作单位">
          {getFieldDecorator("Company")(<Input placeholder="请输入工作单位" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="部门、职务">
          {getFieldDecorator("Position")(
            <Input placeholder="请输入部门、职务" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="转预备党员日期">
          {getFieldDecorator("BeforJoinTime")(
            <DatePicker
              placeholder="请选择转预备党员日期"
              style={{ width: "100%" }}
            />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="转正式党员日期">
          {getFieldDecorator("FormalJoinTime")(
            <DatePicker
              placeholder="请选择转正式党员日期"
              style={{ width: "100%" }}
            />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="人员类别（正式/预备党员）">
          {getFieldDecorator("UserStatus")(
            <Select>
              <Option value="0">预备党员</Option>
              <Option value="1">正式党员</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="党费缴纳情况">
          {getFieldDecorator("IsMoney")(
            <Select>
              <Option value="1">已缴纳</Option>
              <Option value="0">未缴纳</Option>
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="党费缴纳到期时间">
          {getFieldDecorator("ExpireDateFee")(
            <DatePicker
            placeholder="请选择党费缴纳到期时间"
            style={{ width: "100%" }}
          />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="党籍状态">
          {getFieldDecorator("MemberStatus")(
            <Select>
              <Option value="0">正常</Option>
              <Option value="1">预备</Option>
              <Option value="2">开除</Option>
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
                {!member.UserId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/partymemberlist")}
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
    const { member } = props;
    console.log("机构详情数据回显 - ", member);
    return {
      UserId: Form.createFormField({
        value: member.UserId
      }),
      UserName: Form.createFormField({
        value: member.UserName
      }),
      Sex: Form.createFormField({
        value: member.Sex ? member.Sex + "" : "0"
      }),
      BranchId: Form.createFormField({
        value: member.BranchId ? member.BranchId + "" : ""
      }),
      Nation: Form.createFormField({
        value: member.Nation
      }),
      Birth: Form.createFormField({
        value: member.Birth ? moment(news.Birth, "YYYY-MM-DD") : null
      }),
      NativePlace: Form.createFormField({
        value: member.NativePlace
      }),
      FamilyAddress: Form.createFormField({
        value: member.FamilyAddress
      }),
      DefaultAddress: Form.createFormField({
        value: member.DefaultAddress
      }),
      Phone: Form.createFormField({
        value: member.Phone
      }),
      IdCard: Form.createFormField({
        value: member.IdCard
      }),
      Education: Form.createFormField({
        value: member.Education
      }),
      GraduateSchool: Form.createFormField({
        value: member.GraduateSchool
      }),
      Company: Form.createFormField({
        value: member.Company
      }),
      Position: Form.createFormField({
        value: member.Position
      }),
      BeforJoinTime: Form.createFormField({
        value: member.BeforJoinTime ? moment(news.BeforJoinTime, "YYYY-MM-DD") : null
      }),
      FormalJoinTime: Form.createFormField({
        value: member.FormalJoinTime ? moment(news.FormalJoinTime, "YYYY-MM-DD") : null
      }),
      UserStatus: Form.createFormField({
        value: member.UserStatus ? member.UserStatus + "" : "0"
      }),
      IsMoney: Form.createFormField({
        value: member.IsMoney ? "1" : "0"
      }),
      ExpireDateFee: Form.createFormField({
        value: member.ExpireDateFee ? moment(news.ExpireDateFee, "YYYY-MM-DD") : null
      }),
      MemberStatus: Form.createFormField({
        value: member.MemberStatus ? member.MemberStatus + "" : "0"
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      memberReducer: state.memberReducer
    };
  },
  { saveInfo, clearInfo }
)
class PartyMemberDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Orgs: []
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.props.clearInfo();
      // this.getOrgList();
    }
  }
  // 加载所有机构
  getOrgList = async () => {
    try {
      const { List } = await getOrgList();
      this.setState({ Orgs: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  componentWillUnmount() {
    this.props.clearInfo();
  }
  // 根据id查询详情
  getInfo = async UserId => {
    try {
      const { Info } = await getInfoById({ UserId });
      // this.getOrgList();
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        {/* <FormMap member={this.props.memberReducer} Orgs={this.state.Orgs} /> */}
        <FormMap member={this.props.memberReducer} />
      </Nav>
    );
  }
}

export default PartyMemberDetail;
