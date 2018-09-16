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
import { getList, del } from "../../services/systemService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;
class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    const view = this;
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      let params = {
        PageNumber: 1,
        PageSize: 10,
        ...values
      };
      const { getCurrentList, setObj } = view.props;
      getCurrentList(params);
      setObj({ ...values });
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
    const { getCurrentList, setObj } = this.props;
    let params = {
      PageNumber: 1,
      PageSize: 10,
      Status: "-1"
    };
    getCurrentList(params);
    setObj({});
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
            <FormItem label="名称">
              {getFieldDecorator(`SystemMessageName`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={5} key={6}>
            <FormItem label="状态">
              {getFieldDecorator("Status")(
                <Select>
                  <Option value="-1">全部</Option>
                  <Option value="0">正常</Option>
                  <Option value="1">禁用</Option>
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
    return {
      SystemMessageName: Form.createFormField({
        value: props.obj.SystemMessageName
      }),
      Status: Form.createFormField({
        value: props.obj.Status ? props.obj.Status + "" : "-1"
      })
    };
  }
})(SearchForm);
class SystemList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      systemList: [], // 列表数组
      obj: {}
    };
    this.columns = [
      {
        title: "标题",
        dataIndex: "NewsName"
      },
      {
        title: "重复类型",
        dataIndex: "RepeatTime",
        render: obj => <span>{obj == "0" ? "每年" : "仅一次"}</span>
      },
      {
        title: "提醒时间",
        render: obj => (
          <span>
            {obj.RepeatTime == "0"
              ? new Date(obj.NoticeTime).format("MM-dd")
              : new Date(obj.NoticeTime).format("yyyy-MM-dd")}
          </span>
        )
      },
      // {
      //   title: "内容",
      //   dataIndex: "HtmlDesc"
      // },
      {
        title: "状态",
        dataIndex: "Status",
        render: obj => <span>{obj == "0" ? "正常" : "禁用"}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/system/detail/${obj.NewsId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.NewsId)}
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
      Status: "-1",
      SystemMessageName: ""
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
    const { PageNumber } = params;
    try {
      let { List, TotalCount, Message } = await getList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.NewsId;
      });
      this.setState({ systemList: List, current: PageNumber, Message });
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
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code, Message } = await del({
            NewsId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
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
  // 间接调用getCurrentList
  getCurrent(params) {
    this.getCurrentList(params);
  }
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const { Message, pageConfig, systemList, obj } = this.state;
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrent.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={systemList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
        <Row type="flex" gutter={24}>
          <Col>
            <Button
              className="btn btn--primary"
              type="primary"
              onClick={() => hashHistory.replace(`/system/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default SystemList;
