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
import { getRankList, getRankInfoById } from "../../services/scoreService";
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
    getCurrentList({
      PageNumber: 1,
      PageSize: 10
    });
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
              {getFieldDecorator(`UserName`)(<Input />)}
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
      UserName: Form.createFormField({
        value: props.obj.UserName
      })
    };
  }
})(SearchForm);
class RankList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      rankList: [], // 列表数组
      obj: {}
    };
    this.columns = [
      {
        title: "用户姓名",
        dataIndex: "UserName"
      },
      {
        title: "用户积分总数",
        dataIndex: "Score"
      },
      // {
      //   title: "用户使用积分",
      //   dataIndex: "UserIdentity"
      // },
      {
        title: "党员状态",
        dataIndex: "UserIdentity",
        render: text => <span>{text == "0" ? "正式党员" : "预备党员"}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(
                    `/rank/detail/${obj.UserId}/${obj.UserName}/${obj.Score}`
                  )
                }
              >
                查看详细信息
              </a>
            </div>
          );
        }
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      UserName: ""
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount, Message } = await getRankList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.UserId;
      });
      this.setState({ rankList: List, current: PageNumber, Message });
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
    const { pageConfig, rankList, obj, Message } = this.state;
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrent.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={rankList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
      </Nav>
    );
  }
}

export default RankList;
