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
import { getList, del } from "../../../services/exerciseService";
import Nav from "../../Nav";
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
            <FormItem label="习题名称">
              {getFieldDecorator(`Question`)(<Input />)}
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
      Question: Form.createFormField({
        value: props.obj.Question
      })
    };
  }
})(SearchForm);
class ExList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [], // 列表数组
      pageConfig: {}, // 当前页配置
      obj: {}
    };
    this.columns = [
      {
        title: "习题编号",
        dataIndex: "QuestionId"
      },
      {
        title: "习题名称",
        dataIndex: "Question",
        width: "60%"
      },
      {
        title: "是否多选",
        dataIndex: "Multiple",
        render: text => <span>{text == "0" ? "单选" : "多选"}</span>
      },
      {
        title: "是否投票",
        dataIndex: "IsVote",
        render: text => <span>{text ? "是" : "否"}</span>
      },
      {
        title: "操作",
        render: obj => (
          <span>
            <a
              onClick={() =>
                hashHistory.replace(`/exercise/detail/${obj.QuestionId}`)
              }
            >
              编辑
            </a>
            <a
              onClick={() => this.handleDelete(obj.QuestionId)}
              style={{ paddingLeft: "24px" }}
            >
              删除
            </a>
          </span>
        )
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10
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
      let { List, TotalCount } = await getList(params);
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ List, current: PageNumber });
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
  handleDelete = async QuestionId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({
            QuestionId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
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
    const { List, pageConfig, obj } = this.state;
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrent.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          columns={this.columns}
          dataSource={List}
          bordered
          style={{ width: "90%" }}
          pagination={pageConfig}
        />
        <Row
          type="flex"
          gutter={24}
          // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button
              className="btn btn--primary"
              onClick={() => hashHistory.replace(`/exercise/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ExList;
