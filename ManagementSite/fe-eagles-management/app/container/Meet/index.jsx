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
import { getNewsList, deleteNews } from "../../services/newsService";
import moment from "moment";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    const _this = this;
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      let params = {
        ..._this.props.getListConfig,
        ...values,
        PageNumber: 1
      };
      const getCurrentList = this.props.getCurrentList;
      getCurrentList(params);
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    let params = {
      ...this.props.getListConfig,
      ...values
    };
    const getCurrentList = this.props.getCurrentList;
    getCurrentList(params);
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
          <Col span={5} key={1}>
            <FormItem label="会议名称">
              {getFieldDecorator(`NewsName`)(<Input />)}
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
    const config = props.getListConfig;
    console.log(config);
    return {
      NewsName: Form.createFormField({
        value: config.NewsName
      }),
      StarTime: Form.createFormField({
        value: config.StarTime ? moment(config.StarTime, "YYYY-MM-dd") : null
      }),
      EndTime: Form.createFormField({
        value: config.EndTime ? moment(config.EndTime, "YYYY-MM-dd") : null
      })
    };
  }
})(SearchForm);

class MeetList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      newsList: [], // 新闻列表数组
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "会议名称",
        dataIndex: "NewsName",
        width: "30%"
      },
      {
        title: "会议发起人",
        dataIndex: "Author"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a onClick={() => hashHistory.replace(`/meet/detail/${obj.NewsId}`)}>
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.NewsId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
              <a style={{ paddingLeft: "24px" }} onClick={() => hashHistory.replace(`/importmember/${obj.NewsId}/${obj.NewsName}`)}>
              导入
              </a>
            </div>
          );
        }
      }
    ];
    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      NewsName: "",
      NewsType: "1"
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
    try {
      let { List, TotalCount } = await getNewsList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.NewsId;
      });
      this.getListConfig = params; // 保存搜索的数据
      this.setState({ newsList: List, current: params.PageNumber });
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
  // 删除项目
  handleDelete = async NewsId => {
    console.log(NewsId);
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await deleteNews({
            NewsId
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
  render() {
    const { selectedRowKeys, pageConfig, newsList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrentList}
          getListConfig={this.getListConfig}
        />
        <Table
          dataSource={newsList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
      </Nav>
    );
  }
}

export default MeetList;
