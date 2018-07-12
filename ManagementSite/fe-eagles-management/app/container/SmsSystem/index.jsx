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

class SmsSystemList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      orgList: [], // 项目列表数组
      AreaInfos: [] // 地区
    };
    this.columns = [
      {
        title: "短信商id",
        dataIndex: "OrgId"
      },
      {
        title: "短信商",
        dataIndex: "OrgName"
      },
      {
        title: "appId",
        dataIndex: "a"
      },
      {
        title: "appKey",
        dataIndex: "b"
      },
      {
        title: "短信总数",
        dataIndex: "n"
      },
      {
        title: "已用数量",
        dataIndex: "g"
      },
      {
        title: "状态",
        dataIndex: "t"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/smssystem/detail/${obj.OrgId}`)
                }
              >
                编辑
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
    // this.getCurrentList(this.getListConfig);
    // this.getAreaList();
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = this.getListConfig;
    try {
      let res = await getOrgList(params);
      console.log("orgList - ", res.List);
      res.List.forEach(v => {
        v.key = v.OrgId;
      });
      this.setState({ orgList: res.List, current: PageNumber });
      // this.updatePageConfig(totalSize);
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
  // 删除项目
  handleDelete = async OrgId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await deleteOrg({
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
            message.error("删除失败");
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
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
    const { selectedRowKeys, pageConfig, orgList, AreaInfos } = this.state;
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
        <Table
          dataSource={orgList}
          columns={this.columns}
          onChange={async (page, pagesize) => {
            this.getCurrentList({
              ...this.getListConfig,
              requestPage: page,
              keyword: this.state.keyword
            });
          }}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Row
          type="flex"
          gutter={24}
          // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/org/detail`)}>新增</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default SmsSystemList;