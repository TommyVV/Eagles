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
const TextArea = Input.TextArea;
import { hashHistory } from "react-router";
import util from "../../../utils/util";
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
            <FormItem label="商品名称">
              {getFieldDecorator(`name`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={6} key={1}>
            <FormItem label="状态">
              {getFieldDecorator("state")(
                <Select>
                  <Option value="0">待审核</Option>
                  <Option value="1">审核通过</Option>
                  <Option value="2">审核不通过</Option>
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={6} key={3}>
            <FormItem label="类型筛选">
              {getFieldDecorator("type")(
                <Select>
                  <Option value="0">文章</Option>
                  <Option value="1">积分</Option>
                  <Option value="2">其他</Option>
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
      type: Form.createFormField({
        value: "0"
      }),
      state: Form.createFormField({
        value: "0"
      })
    };
  }
})(SearchForm);

class CheckList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      projectList: [], // 项目列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {}, // 当前页配置
      visible: false
    };
    this.columns = [
      {
        title: "名称",
        dataIndex: "name",
        width: "20%"
      },
      {
        title: "上传用户",
        dataIndex: "user",
        width: "20%"
      },
      {
        title: "新增时间",
        dataIndex: "time",
        width: "20%",
        render: text => <span>{text}</span>
      },
      {
        title: "状态",
        dataIndex: "state",
        width: "20%",
        render: text => <span>{text}</span>
      },
      {
        title: "操作",
        width: "20%",
        render: (text, record) => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/project/detail/${record.projectId}`)
                }
              >
                详情
              </a>
              <a
                onClick={() =>
                  this.setState({
                    visible: true
                  })
                }
                style={{ paddingLeft: "24px" }}
              >
                审核通过
              </a>
            </div>
          );
        }
      }
    ];
    this.data = [
      {
        key: "1",
        name: "领导人生日",
        user: "张三",
        time: "2018-5-12 18:00",
        state: "待审核"
      },
      {
        key: "2",
        name: "领导人生日",
        user: "张三",
        time: "2018-5-12 18:00",
        state: "审核通过"
      },
      {
        key: "3",
        name: "领导人生日",
        user: "张三",
        time: "2018-5-12 18:00",
        state: "审核不通过"
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
  handleOk = e => {
    console.log(e);
    this.setState({
      visible: false
    });
  };
  handleCancel = e => {
    console.log(e);
    this.setState({
      visible: false
    });
  };
  render() {
    const { selectedRowKeys, pageConfig, projectList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 17 }
      }
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
              批量审核
            </Button>
          </Col>
        </Row>
        <Modal
          title="审核信息"
          visible={this.state.visible}
          onOk={this.handleOk}
          onCancel={this.handleCancel}
          width="420px"
          okText="确定"
          cancelText="取消"
        >
          <Form>
            <FormItem {...formItemLayout} label="状态">
              <Select>
                <Option value="0">s审核通过</Option>
                <Option value="1">审核不通过</Option>
              </Select>
            </FormItem>
            <FormItem {...formItemLayout} label="原因">
              <TextArea rows={4} />
            </FormItem>
          </Form>
        </Modal>
      </Nav>
    );
  }
}

export default CheckList;
