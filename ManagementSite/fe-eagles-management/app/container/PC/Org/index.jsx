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
import { getOrgList, deleteOrg } from "../../../services/orgService";
import util from "../../../utils/util";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class OrgList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      orgList: [] // 项目列表数组
    };
    this.columns = [
      {
        title: "机构编号",
        dataIndex: "OrgId"
      },
      {
        title: "机构名称",
        dataIndex: "OrgName"
      },
      {
        title: "添加时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-DD")}</span>
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
  // 更新分页配置
  updatePageConfig(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.pageSize,
      current: this.state.current,
      onChange: async (page, pagesize) => {
        this.getCurrentList({
          ...this.getListConfig,
          requestPage: page,
          keyword: this.state.keyword
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
          let { Code } = await deleteOrg({
            OrgId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              requestPage: this.state.current
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
    const { selectedRowKeys, pageConfig, orgList } = this.state;
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
      <Nav>
        <Row gutter={24}>
          <Col span={12}>
            <Form>
              <FormItem {...formItemLayout} label="选择组织">
                <Cascader
                  options={options}
                  onChange={this.onChange.bind(this)}
                  placeholder="请选择地区"
                />
              </FormItem>
            </Form>
          </Col>
        </Row>
        <Table
          dataSource={orgList}
          columns={this.columns}
          pagination={pageConfig}
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

export default OrgList;
