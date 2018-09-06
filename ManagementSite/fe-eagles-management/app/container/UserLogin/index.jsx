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
import { getLoginList } from "../../services/memberService";
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
class UserLoginList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [], // 列表数组
      obj: {}
    };
    this.columns = [
      {
        title: "用户姓名",
        dataIndex: "Name"
      },
      {
        title: "手机号",
        dataIndex: "Phone"
      },
      
      {
        title: "是否是游客",
        dataIndex: "IsCustomer"
      },
      {
        title: "登录时间",
        dataIndex: "CreateTime"
      },
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
      let { List, TotalCount } = await getLoginList(params);
      console.log("List - ", List);
      List.forEach((v,i) => {
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
    const { pageConfig, List, obj } = this.state;
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrent.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={List}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
      </Nav>
    );
  }
}

export default UserLoginList;
