import React, { Component } from "react";
import { connect } from "react-redux";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Cascader,
  Upload,
  Icon
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getOrgInfoById, createOrEditOrg } from "../../services/orgService";
import { getAllArea } from "../../services/areaService";
import { serverConfig } from "../../constants/ServerConfigure";
import { saveOrgInfo, clearInfo } from "../../actions/orgAction";
import "./style.less";

const FormItem = Form.Item;
@connect(
  state => {
    return {
      user: state.userReducer,
      orgReducer: state.orgReducer
    };
  },
  { saveOrgInfo, clearInfo }
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
          const { org } = this.props;
          let params = {
            Info: {
              ...org,
              ...values
            }
          };
          let { Code } = await createOrEditOrg(params);
          if (Code === "00") {
            let tip = this.props.org.OrgId ? "保存机构成功" : "创建机构成功";
            message.success(tip);
            hashHistory.replace("/orglist");
          } else {
            let tip = this.props.org.OrgId ? "保存机构失败" : "创建机构失败";
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
  // 将地区的数组转换为对象
  convertObj(area) {
    let areaParam = {};
    if (area.length) {
      area.map((obj, index) => {
        if (index == 0) {
          areaParam.Province = obj;
        }
        if (index == 1) {
          areaParam.City = obj;
        }
        if (index == 2) {
          areaParam.District = obj;
        }
      });
    } else {
      areaParam = {
        Province: "",
        City: "",
        District: ""
      };
    }
    return areaParam;
  }
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
  onChange(value) {
    // 保存数据
    let { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    const areaParam = this.convertObj(value);
    this.props.saveOrgInfo({ ...values, ...areaParam });
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
      this.props.saveOrgInfo({ ...values, Logo: imageUrl });
    } else if (info.file.status === "error") {
      message.error(`${info.file.name} 上传失败`);
    }
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { org, defaultArea } = this.props;
    let area = [];
    if (org.Province) {
      area.push(org.Province);
      if (org.City) {
        area.push(org.City);
        if (org.District) {
          area.push(org.District);
        }
      }
    }
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
          {getFieldDecorator("OrgId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="短信提供商">
          {getFieldDecorator("OrgName")(
            <Input placeholder="请输入短信提供商" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="优先级">
          {getFieldDecorator("Address")(<Input placeholder="请输入优先级" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="appId">
          {getFieldDecorator("Address")(<Input placeholder="请输入appId" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="appKey">
          {getFieldDecorator("Address")(<Input placeholder="请输入appKey" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="总数短信">
          {getFieldDecorator("Address")(<Input placeholder="请输入总数短信" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="已发数量">
          {getFieldDecorator("Address")(<Input placeholder="请输入已发数量" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="接口地址">
          {getFieldDecorator("Address")(<Input placeholder="请输入接口地址" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("GoodsStatus")(
            <Select>
              <Option value="0">正常</Option>
              <Option value="1">不正常</Option>
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
                {!this.props.org.OrgId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/orglist")}
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
    console.log("机构详情数据回显 - ", props);
    const org = props.org;
    return {
      OrgId: Form.createFormField({
        value: org.OrgId
      }),
      OrgName: Form.createFormField({
        value: org.OrgName
      }),
      Address: Form.createFormField({
        value: org.Address
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      orgReducer: state.orgReducer
    };
  },
  { saveOrgInfo, clearInfo }
)
class SmsSystemDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      AreaInfos: []
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.props.clearInfo();
      this.getAreaList();
    }
  }

  componentWillUnmount() {
    this.props.clearInfo();
  }
  // 根据id查询详情
  getInfo = async OrgId => {
    try {
      const { Info } = await getOrgInfoById({ OrgId });
      this.getAreaList();
      this.props.saveOrgInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 加载所有地区
  getAreaList = async () => {
    try {
      const { AreaInfos } = await getAllArea();
      this.setState({ AreaInfos });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        <FormMap
          org={this.props.orgReducer}
          defaultArea={this.state.AreaInfos}
        />
      </Nav>
    );
  }
}

export default SmsSystemDetail;
