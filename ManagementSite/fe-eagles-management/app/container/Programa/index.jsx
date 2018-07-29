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
  Select
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import Nav from "../Nav";
import "./style.less";
import { getList, createOrEdit, del } from "../../services/programaService";
import { pageMap, stateMap } from "../../constants/config/appconfig";

const confirm = Modal.confirm;
class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    const view = this;
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      console.log("Received values of form: ", values);
      let params = {
        ...this.props.pageConfig,
        ...values
      };
      const getCurrentList = view.props.getCurrentList;
      getCurrentList(params);
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch.bind(this)}
      >
        <Row gutter={24}>
          <Col span={5} key={5}>
            <FormItem label="所属页面">
              {getFieldDecorator(`Page`)(
                <Select>
                  {pageMap.map((obj, index) => {
                    return (
                      <Option key={index} value={obj.value}>
                        {obj.text}
                      </Option>
                    );
                  })}
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={5} key={6}>
            <FormItem label="审核状态">
              {getFieldDecorator(`Status`)(
                <Select>
                  <Option value="0">全部</Option>
                  {stateMap.map((obj, index) => {
                    return (
                      <Option key={index} value={obj.Status}>
                        {obj.text}
                      </Option>
                    );
                  })}
                </Select>
              )}
            </FormItem>
          </Col>
        </Row>
      </Form>
    );
  }
}

const WrapperSearchForm = Form.create({
  mapPropsToFields: props => {
    return {
      Status: Form.createFormField({
        value: "0"
      }),
      Page: Form.createFormField({
        value: "0"
      }),
    };
  }
})(SearchForm);
class ProgramaList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // id数组
      programaList: [], // 列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "栏目名称",
        dataIndex: "ColumnName"
      },
      {
        title: "地址",
        dataIndex: "TargetUrl"
      },
      {
        title: "状态",
        dataIndex: "AuditStatus"
      },
      {
        title: "排序",
        dataIndex: "OrderBy"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/programa/detail/${obj.ColumnId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.ColumnId)}
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
      GoodsName: "",
      GoodsStatus: "0"
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 间接调用getCurrentList
  getCurrent(params) {
    this.getCurrentList(params);
  }
  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount } = await getList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.ColumnId;
      });
      this.setState({ programaList: List, current: PageNumber });
      this.updatePageConfig(TotalCount);
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
  // 快速上下架
  changeStatus = async goods => {
    const tipTitle = goods.GoodsStatus == "0" ? "上架" : "下架";
    confirm({
      title: `是否确认${tipTitle}“${goods.GoodsName}”?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          const params = {
            ...goods,
            GoodsStatus: goods.GoodsStatus == "0" ? "1" : "0"
          };
          let { code } = await createOrEdit(params);
          if (Code === "00") {
            message.success(`${tipTitle}成功`);
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error(`${tipTitle}失败`);
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  // 删除
  handleDelete = async ColumnId => {
    confirm({
      title: `是否确认删除?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({ ColumnId });
          if (Code === "00") {
            message.success(`删除成功`);
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error(`删除失败`);
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  // 批量操作
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
  render() {
    const { selectedRowKeys, pageConfig, programaList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm
          pageConfig={pageConfig}
          getCurrentList={this.getCurrent.bind(this)}
        />
        <Table
          dataSource={programaList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row
          type="flex"
          justify="center"
          gutter={24}
          className={programaList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col>
          <Col>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/programa/detail`)}>
                新增
              </a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ProgramaList;
