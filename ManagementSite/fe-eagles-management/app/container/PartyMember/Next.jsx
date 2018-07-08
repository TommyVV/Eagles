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
  DatePicker
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
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
        onSubmit={this.handleSearch}
      >
        <Row gutter={24}>
          <Col span={6} key={2}>
            <FormItem label="选中党员">
              {getFieldDecorator(`memId`)(<span>李某</span>)}
            </FormItem>
          </Col>
        </Row>
        <Row gutter={24}>
          <Col span={6} key={2}>
            <FormItem label="党员名称">
              {getFieldDecorator(`name`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={6} key={1}>
            <FormItem label="党员级别">
              {getFieldDecorator("type")(
                <Select>
                  <Option value="0">全部</Option>
                  <Option value="1">支部</Option>
                  <Option value="2">小组</Option>
                </Select>
              )}
            </FormItem>
          </Col>
          <Col
            span={6}
            style={{
              textAlign: "cnter",
              paddingLeft: "7px",
              paddingTop: "3px"
            }}
          >
            <Button type="primary" htmlType="submit">
              搜索
            </Button>
            <Button style={{ marginLeft: 8 }} onClick={this.handleReset}>
              清空
            </Button>
          </Col>
        </Row>
      </Form>
    );
  }
}

const WrapperSearchForm = Form.create({
  mapPropsToFields: props => {
    // const project = props.project;
    return {
      exType: Form.createFormField({
        value: "0"
      }),
      state: Form.createFormField({
        value: "0"
      })
    };
  }
})(SearchForm);

class SetNextPartyMember extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      projectList: [], // 项目列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "党员名称",
        dataIndex: "name",
        width: "25%"
      },
      {
        title: "所属支部",
        dataIndex: "branch",
        width: "25%"
      },
      {
        title: "联系电话",
        dataIndex: "phone",
        width: "25%",
      },
      {
        title: "党员类型",
        dataIndex: "type",
        width: "20%",
      }
    ];
    this.data = [
      {
        key: "1",
        name: "张三",
        branch: "第一支部",
        phone: "18512144992",
        type: "党员"
      },
      {
        key: "2",
        name: "张四",
        branch: "第二支部",
        phone: "18512144992",
        type: "预备党员"
      },
      {
        key: "3",
        name: "张五",
        branch: "第二支部",
        phone: "18512144992",
        type: "预备党员"
      }
    ];
    this.getListConfig = {
      requestPage: 1,
      pageSize: 6,
      keyword: ""
    };
  }
  componentWillMount() {
    // this.getCurrentList(this.getListConfig);
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  // getCurrentList = async params => {
  //   try {
  //     let { keyword, requestPage } = params;
  //     let config = { ...this.getListConfig, requestPage, keyword };
  //     let { totalSize, projectList } = await getProjectList(config);
  //     console.log("projectList - ", projectList);
  //     projectList.forEach(v => (v.key = v.projectId));
  //     this.setState({ projectList, current: requestPage });
  //     this.updatePageConfig(totalSize);
  //   } catch (e) {
  //     message.error("获取失败");
  //     throw new Error(e);
  //   }
  // };
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
  // 下拉提示列表 关键字匹配
  fetchList = async value => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword };
      let { projectList } = await getProjectList(params);
      return projectList.map(v => ({
        text: v.projectName,
        value: v.projectName
      }));
    } catch (e) {
      throw new Error(e);
    }
  };
  // 回车搜索列表  关键字匹配
  searchList = async value => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword };
      let { projectList, totalSize } = await getProjectList(params);
      projectList.forEach(v => (v.key = v.projectId));
      this.setState({
        keyword,
        projectList,
        current: 1
      });
      this.updatePageConfig(totalSize);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 删除项目
  handleDelete = async shareIds => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { selectedRowKeys } = this.state;
          if (selectedRowKeys.length === 0) {
            return message.error("请选择需要删除的项目");
          }
          let { code } = await deleteProject({
            projectIdList: selectedRowKeys
          });
          if (code === 0) {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              requestPage: this.state.current,
              keyword: this.state.keyword
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
  render() {
    const { selectedRowKeys, pageConfig, projectList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm />
        <Table
          dataSource={this.data}
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
          className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col>
          <Col>
            <Button className="btn ">
              <a onClick={() => hashHistory.replace(`/partymember/detail`)}>
                新增
              </a>
            </Button>
          </Col>
          <Col>
            <Button className="btn ">
              <a onClick={() => hashHistory.replace(`/exercise/create`)}>
                导入
              </a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default SetNextPartyMember;
