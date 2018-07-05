import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Cascader,
  Upload,
  Icon
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getOrgInfoById, createOrEditOrg } from "../../../services/orgService";
import { serverConfig } from "../../../constants/ServerConfigure";
import "./style.less";

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
          const { area } = this.props;
          const areaParam = {};
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
          let params = {
            Info: {
              ...values,
              ...areaParam,
              Logo: ""
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
  render() {
    const { getFieldDecorator } = this.props.form;
    const { area, org } = this.props;
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
    const options = [
      {
        value: "浙江",
        label: "浙江",
        children: [
          {
            value: "杭州",
            label: "杭州",
            children: [
              {
                value: "西湖",
                label: "西湖"
              }
            ]
          }
        ]
      },
      {
        value: "湖北",
        label: "湖北",
        children: [
          {
            value: "襄阳",
            label: "襄阳",
            children: [
              {
                value: "襄城区",
                label: "襄城区"
              }
            ]
          }
        ]
      }
    ];
    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("OrgId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="机构名称">
          {getFieldDecorator("OrgName", {
            rules: [
              {
                required: true
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="所属地区">
          {area.length ? (
            <Cascader
              options={options}
              // onChange={onChange}
              placeholder="请选择地区"
              defaultValue={area}
            />
          ) : org.OrgId ? (
            <Input />
          ) : (
            <Cascader
              options={options}
              // onChange={onChange}
              placeholder="请选择地区"
            />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="详细地址">
          {getFieldDecorator("Address", {
            rules: [
              {
                required: true
              }
            ]
          })(<Input placeholder="必填，20字以内" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="新闻封面">
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
              } else if (info.file.status === "error") {
                message.error(`${info.file.name} 上传失败`);
              }
            }}
          >
            {org.Logo ? (
              <img src={org.Logo} alt="avatar" style={{ width: "100%" }} />
            ) : (
              <div>
                <Icon type={this.state.loading ? "loading" : "plus"} />
                <div className="ant-upload-text">上传</div>
              </div>
            )}
          </Upload>
        </FormItem>
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {this.props.org.OrgId === "" ? "新建" : "保存"}
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

class OrgDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      orgDetail: {}, //机构详情
      area: []
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    }
  }

  componentWillUnmount() {
    // this.props.clearProjectInfo();
  }
  // 根据id查询详情
  getInfo = async OrgId => {
    try {
      const { Info } = await getOrgInfoById({ OrgId });
      console.log("newsDetails", Info);
      let area = [];
      if (Info.Province) {
        area.push(Info.Province);
        if (Info.City) {
          area.push(Info.City);
          if (Info.District) {
            area.push(Info.District);
          }
        }
      }
      this.setState({
        orgDetail: Info,
        area: area
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    const { orgDetail, area } = this.state;
    return (
      <Nav>
        <FormMap org={orgDetail} area={area} />
      </Nav>
    );
  }
}

export default OrgDetail;
