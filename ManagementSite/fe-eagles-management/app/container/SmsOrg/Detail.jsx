import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
} from "antd";
import moment from "moment";
import "moment/locale/zh-cn";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/orgSmsService";
import { getOrgList } from "../../services/orgService";
import { getList } from "../../services/systemSmsService";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
const TextArea = Input.TextArea;
class Base extends Component {
  constructor(props) {
    super(props);
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { system, org } = this.props;
          const { OrgId } = values;
          let OrgName = "";
          org.map((o, i) => {
            if (o.OrgId == OrgId) {
              OrgName = o.OrgName;
            }
          });
          let params = {
            Info: {
              ...system,
              ...values,
              OrgName
            }
          };
          let { Code,Message } = await createOrEdit(params);
          if (Code === "00") {
            let tip = system.VendorId ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/smsorglist");
          } else {
            // let tip = system.VendorId ? "保存失败" : "创建失败";
            message.error(Message);
          }
        } catch (e) {
          throw new Error(e);
        }
      } else {
        message.error("请检查字段是否正确");
      }
    });
  };
  changDate(value) {
    console.log(value);
    // const { system } = this.props;
    // const { RepeatTime, NoticeTime } = values;
    // const system={

    // };
    // if (value == "0") {
    // }
  }
  render() {
    const { getFieldDecorator } = this.props.form;
    const { system, org, vendor } = this.props;
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
          {/* {getFieldDecorator("VendorId")(<Input />)} */}
        </FormItem>
        <FormItem {...formItemLayout} label="组织名称">
          {getFieldDecorator("OrgId", {
            rules: [
              {
                required: true,
                message: "请选择组织"
              }
            ]
          })(
            <Select placeholder="请选择组织">
              {org.map((o, i) => {
                return (
                  <Option value={o.OrgId} key={i}>
                    {o.OrgName}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="短信提供商">
          {getFieldDecorator("VendorId", {
            rules: [
              {
                required: true,
                message: "请选择短信提供商"
              }
            ]
          })(
            <Select placeholder="请选择短信提供商">
              {vendor.map((o, i) => {
                return (
                  <Option value={o.VendorId} key={i}>
                    {o.VendorName}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="优先级">
          {getFieldDecorator("Priority")(<Input placeholder="请输入优先级" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="总数短信">
          {getFieldDecorator("MaxCount")(
            <Input placeholder="请输入总数短信" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="已发数量">
          {getFieldDecorator("SendCount")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("Status")(
            <Select>
              <Option value="0">正常</Option>
              <Option value="1">禁止</Option>
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
                {!system.VendorId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/smsorglist")}
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
    const { system } = props;
    console.log("详情数据回显 - ", system);
    return {
      VendorId: Form.createFormField({
        value: system.VendorId ? system.VendorId : ""
      }),
      OrgId: Form.createFormField({
        value: system.OrgId ? system.OrgId : ""
      }),
      Priority: Form.createFormField({
        value: system.Priority
      }),
      MaxCount: Form.createFormField({
        value: system.MaxCount
      }),
      SendCount: Form.createFormField({
        value: system.SendCount ? system.SendCount : "0"
      }),
      Status: Form.createFormField({
        value: system.Status ? system.Status + "" : "0"
      })
    };
  }
})(Base);
class SmsSystemDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      system: {},
      org: [],
      vendor: []
    };
  }

  componentWillMount() {
    let { org, vendor } = this.props.params;
    if (org && vendor) {
      this.getInfo(org, vendor); //拿详情
    } else {
      this.getOrg();
      this.getVendor();
    }
  }

  // 根据id查询详情
  getInfo = async (OrgId, VendorId) => {
    try {
      await this.getOrg();
      await this.getVendor();
      const { Info } = await getInfoById({ VendorId, OrgId });
      this.setState({ system: Info });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 查组织
  getOrg = async () => {
    try {
      const { List } = await getOrgList({
        PageNumber: 1,
        PageSize: 10000
      });
      this.setState({ org: List });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 查短信商
  getVendor = async () => {
    try {
      const { List } = await getList({
        PageNumber: 1,
        PageSize: 100000,
        Status:0
      });
      this.setState({ vendor: List });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    const { system, org, vendor } = this.state;
    return (
      <Nav>
        <FormMap system={system} org={org} vendor={vendor} />
      </Nav>
    );
  }
}

export default SmsSystemDetail;
