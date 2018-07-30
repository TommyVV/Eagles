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
import { getListById, getList, setNext } from "../../services/memberService";
import { hashHistory } from "react-router";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

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
      let { User } = await getListById(params);
      console.log("List - ", User);
      User.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ memberList: User });
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
    let newList = [];
    selectedRowKeys.map(o => newList.push(JSON.parse(o)));
    newList.forEach((v, i) => {
      v.key = i;
    });
    this.setState({ memberList: newList, visible: false });
  }
  getAllMember() {
    this.setState({ visible: true });
    this.getCurrentList2({ ...this.pageConfig });
  }
  // 加载当前页
  getCurrentList2 = async params => {
    const { id } = this.props.params;
    const { PageNumber } = params;
    try {
      let { List, TotalCount } = await getList(params);
      // 排除掉当前设置的用户
      console.log("List - ", List);
      let newList = List.filter(v => v.UserId != id);
      newList.forEach(v => {
        v.key = JSON.stringify(v);
      });
      this.setState({ memberList2: newList, current2: PageNumber });
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
  render() {
    const {
      selectedRowKeys,
      pageConfig,
      pageConfig2,
      visible,
      memberList,
      memberList2
    } = this.state;
    const { id, name } = this.props.params;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
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
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row type="flex" justify="center" gutter={24}>
          {/* <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col> */}
          <Col>
            <Button className="btn btn--primary">
              <a onClick={() => this.saveMember()}>保存</a>
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
          <Table
            dataSource={memberList2}
            columns={this.columns2}
            rowSelection={rowSelection}
            pagination={pageConfig2}
            locale={{ emptyText: "暂无数据" }}
            bordered
            style={{ marginBottom: "0" }}
          />
        </Modal>
      </Nav>
    );
  }
}

export default SetNextPartyMember;
