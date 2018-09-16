import React, { Component } from "react";
import {
  Table,
  Row,
  Col,
  Button,
  message,
  Modal,
  Form,
  Input,
  Select,
  Cascader
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import { getOrgList, deleteOrg } from "../../services/orgService";
import { getAllArea } from "../../services/areaService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class OrgList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      orgList: [], // 项目列表数组
      AreaInfos: [] // 地区
    };
    this.columns = [
      {
        title: "组织编号",
        dataIndex: "OrgId"
      },
      {
        title: "组织名称",
        dataIndex: "OrgName"
      },
      {
        title: "添加时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() => hashHistory.replace(`/org/detail/${obj.OrgId}`)}
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.OrgId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
            </div>
          );
        }
      }
    ];
    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      Province: "",
      City: "",
      District: ""
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
    this.getAreaList();
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount, Message } = await getOrgList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.OrgId;
      });
      this.setState({ orgList: List, current: PageNumber, Message });
      this.updatePageConfig(TotalCount);
    } catch (e) {
      message.error("获取失败");
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
  // 更新分页配置
  updatePageConfig(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.PageSize,
      current: this.state.current,
      onChange: async (page, pagesize) => {
        this.getCurrentList({
          ...this.getListConfig,
          PageNumber: page
        });
      }
    };
    this.setState({ pageConfig });
  }
  // 删除项目
  handleDelete = async OrgId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code, Message } = await deleteOrg({
            OrgId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error(Message);
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  // 编辑项目
  handleEdit = async () => {
    try {
      let { selectedRowKeys } = this.state;
      console.log(selectedRowKeys);
      if (selectedRowKeys.length > 1) {
        return message.error("不能同时编辑多个项目");
      }
      if (selectedRowKeys.length === 0) {
        return message.error("请选择需要编辑的项目");
      }
      hashHistory.replace(`/project/create/${selectedRowKeys[0]}`);
    } catch (e) {
      throw new Error(e);
    }
  };
  onChange(value, selectedOptions) {
    const areaParam = {};
    value.map((obj, index) => {
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
      ...this.getListConfig,
      ...areaParam
    };
    this.getCurrentList(params);
  }
  render() {
    const { selectedRowKeys, pageConfig, Message, orgList, AreaInfos } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    const formItemLayout = {
      labelCol: {
        xl: { span: 3 }
      },
      wrapperCol: {
        xl: { span: 10 }
      }
    };
    return (
      <Nav>
        <Row gutter={24}>
          <Col span={12}>
            <Form>
              <FormItem {...formItemLayout} label="选择组织">
                <Cascader
                  options={AreaInfos}
                  onChange={this.onChange.bind(this)}
                  placeholder="请选择地区"
                  changeOnSelect
                />
              </FormItem>
            </Form>
          </Col>
        </Row>
        <Table
          dataSource={orgList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
        <Row
          type="flex"
          gutter={24}
        // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button
              className="btn btn--primary"
              onClick={() => hashHistory.replace(`/org/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default OrgList;
