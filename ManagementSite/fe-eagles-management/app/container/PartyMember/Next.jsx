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
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { getListById, getList, setNext } from "../../services/memberService";
import { getList as getBranchList } from "../../services/branchService";
import { hashHistory } from "react-router";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;
class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      branchList: []
    };
  }
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
      const getCurrentList = view.props.getCurrentList;
      getCurrentList(params);
      const { setObj } = this.props;
      setObj(values);
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
  };
  componentWillMount() {
    this.getCurrentList();
  }
  // 加载当前页
  getCurrentList = async () => {
    try {
      let { List } = await getBranchList({
        PageNumber: 1,
        PageSize: 10000
      });
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ branchList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { branchList } = this.state;
    let userInfo = JSON.parse(localStorage.info);
    let branchId = userInfo.BranchId;
    console.log(branchId);
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch.bind(this)}
      >
        <Row gutter={24}>
          {branchId == 0 ? (
            <Col span={10} key={1}>
              <FormItem label="支部名称">
                {getFieldDecorator("BranchId")(
                  <Select style={{ maxWidth: "152px" }}>
                    <Option value="">全部</Option>
                    {branchList.map((o, i) => {
                      return (
                        <Option key={i} value={o.BranchId}>
                          {o.BranchName}
                        </Option>
                      );
                    })}
                  </Select>
                )}
              </FormItem>
            </Col>
          ) : null}
          <Col span={10} key={2}>
            <FormItem label="党员名称">
              {getFieldDecorator(`UserName`)(<Input />)}
            </FormItem>
          </Col>
          <Col
            span={4}
            style={{
              textAlign: "cnter",
              paddingLeft: "7px",
              paddingTop: "3px"
            }}
          >
            <Button type="primary" htmlType="submit">
              搜索
            </Button>
          </Col>
        </Row>
      </Form>
    );
  }
}

const WrapperSearchForm = Form.create({
  mapPropsToFields: props => {
    console.log(props);
    return {
      BranchId: Form.createFormField({
        value: props.obj.BranchId ? props.obj.BranchId : ""
      }),
      UserName: Form.createFormField({
        value: props.obj.UserName
      })
    };
  }
})(SearchForm);
class SetNextPartyMember extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      memberList: [], // 列表数组
      memberList2: [], // 弹窗列表数组
      current2: 1, // 当前页弹窗
      pageConfig: {}, // 当前页配置
      pageConfig2: {}, // 当前页配置
      obj: {},
      visible: false
    };
    this.columns = [
      {
        title: "党员名称",
        dataIndex: "UserName",
        key: "UserName"
      },
      {
        title: "所属支部",
        dataIndex: "BranchName",
        key: "BranchName"
      },
      {
        title: "联系电话",
        dataIndex: "Phone",
        key: "Phone"
      },
      {
        title: "党员类型",
        dataIndex: "MemberType",
        key: "MemberType",
        render: text => <span>{text == "0" ? "党员" : "预备党员"}</span>
      },
      {
        title: "操作",
        key: "oper",
        render: obj => <a onClick={() => this.handleDelete(obj.UserId)}>删除</a>
      }
    ];
    this.columns2 = [
      {
        title: "党员名称",
        dataIndex: "UserName",
        key: "UserName"
      },
      {
        title: "所属支部",
        dataIndex: "BranchName",
        key: "BranchName"
      },
      {
        title: "联系电话",
        dataIndex: "Phone",
        key: "Phone"
      },
      {
        title: "党员类型",
        dataIndex: "MemberType",
        key: "MemberType",
        render: text => <span>{text == "0" ? "党员" : "预备党员"}</span>
      }
    ];
    this.getListConfig = {
      PageNumber: 1,
      PageSize: 5
    };
  }
  componentWillMount() {
    const { id } = this.props.params;
    this.getCurrentList({ UserId: id });
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  getCurrentList = async params => {
    try {
      let { User, Message } = await getListById(params);
      console.log("List - ", User);
      User.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ memberList: User, Message });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };

  // 删除
  handleDelete = async UserId => {
    let { memberList } = this.state;
    let newKeys = memberList.filter(v => v.UserId != UserId);
    this.setState({ memberList: newKeys });
  };
  // 保存下级
  saveMember = async () => {
    try {
      const { id } = this.props.params;
      const { memberList } = this.state;
      let newList = [];
      memberList.forEach(v => {
        newList.push(v.UserId);
      });
      let param = {
        UserId: id,
        UserIds: newList
      };

      let { Code } = await setNext(param);
      if (Code == "00") {
        message.success("设置成功");
        hashHistory.replace("/partymemberlist");
      } else {
        message.error("设置失败");
      }
    } catch (e) {
      throw new Error(e);
    }
  };
  handleOk() {
    const { selectedRowKeys } = this.state;
    // 需要加上以前以选择的用户
    let newList = this.state.memberList;
    selectedRowKeys.map(o => {
      //remove exists user
      var obj = JSON.parse(o);
      var existsUsers = newList.find(x => x.UserId == obj.UserId);
      if (existsUsers == null) {
        newList.push(obj)
      }
    });
    newList.forEach((v, i) => {
      v.key = i;
    });
    this.setState({ memberList: newList, visible: false });
  }
  getAllMember() {
    this.setState({ visible: true });
    this.getCurrentList2({ ...this.getListConfig });
  }

  // 加载当前页
  getCurrentList2 = async params => {
    const { id } = this.props.params;
    const { PageNumber } = params;
    try {
      let { List, TotalCount, Message } = await getList(params);
      // 排除掉当前设置的用户
      console.log("List - ", List);
      console.log("current user", id)
      let newList = List.filter(v => v.UserId != id);
      newList.forEach(v => {
        v.key = JSON.stringify(v);
      });
      this.setState({ memberList2: newList, current2: PageNumber, Message });
      this.updatePageConfig2(TotalCount);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 更新分页配置
  updatePageConfig2(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.PageSize,
      current: this.state.current2,
      onChange: async (page, pagesize) => {
        this.getCurrentList2({
          ...this.getListConfig,
          PageNumber: page
        });
      }
    };
    this.setState({ pageConfig2: pageConfig });
  }
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const {
      selectedRowKeys,
      pageConfig,
      pageConfig2,
      visible,
      memberList,
      Message,
      memberList2,
      obj
    } = this.state;
    const { id, name } = this.props.params;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    let userInfo = JSON.parse(localStorage.info);
    let branchId = userInfo.BranchId;
    return (
      <Nav>
        <Form className="ant-advanced-search-form" layout="inline">
          <Row gutter={24}>
            <Col span={6} key={2}>
              <FormItem label="上级领导">
                <span>{name}</span>
              </FormItem>
            </Col>
          </Row>
          <Row gutter={24}>
            <Col span={6} key={2}>
              <FormItem label="选择下级">
                <Button
                  type="primary"
                  className="btn btn--primary"
                  onClick={() => this.getAllMember()}
                >
                  选择
                </Button>
              </FormItem>
            </Col>
          </Row>
        </Form>
        <Table
          dataSource={memberList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />

        <Row type="flex" justify="center" gutter={24}>
          {/* <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col> */}
          <Col>
            <Button className="btn btn--primary" onClick={() => this.saveMember()}>
              保存
            </Button>
          </Col>
          <Col>
            <Button className="btn ">
              <a onClick={() => hashHistory.replace(`/partymemberlist`)}>
                取消
              </a>
            </Button>
          </Col>
        </Row>
        <Modal
          title="选择下级"
          visible={visible}
          width={700}
          onOk={this.handleOk.bind(this)}
          onCancel={() => this.setState({ visible: false })}
        >
          <WrapperSearchForm
            getCurrentList={this.getCurrentList2.bind(this)}
            obj={obj}
            setObj={this.changeSearch.bind(this)}
          />
          <Table
            dataSource={memberList2}
            columns={this.columns2}
            rowSelection={rowSelection}
            pagination={pageConfig2}
            locale={{ emptyText: Message }}
            bordered
            style={{ marginBottom: "0" }}
          />
        </Modal>
      </Nav>
    );
  }
}

export default SetNextPartyMember;
