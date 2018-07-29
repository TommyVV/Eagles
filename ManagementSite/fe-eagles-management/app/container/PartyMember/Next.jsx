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
      current2: 1, // 当前页弹窗
      pageConfig: {}, // 当前页配置
      pageConfig2: {}, // 当前页配置
      visible: false
    };
    this.columns = [
      {
        title: "党员名称",
        dataIndex: "UserName"
      },
      {
        title: "所属支部",
        dataIndex: "BranchName"
      },
      {
        title: "联系电话",
        dataIndex: "Phone"
      },
      {
        title: "党员类型",
        dataIndex: "MemberType",
        render: text => <span>{text}</span>
      }
    ];
    this.getListConfig = {
      PageNumber: 1,
      PageSize: 5
    };
  }
  componentWillMount() {
    const { id, name } = this.props.params;
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
  // 保存下级
  saveMember = async () => {
    try {
      const { selectedRowKeys, currentId } = this.state;
      console.log(selectedRowKeys);
      let param = {
        UserId: this.props.params.id,
        UserIds: selectedRowKeys
      };
      let { Code } = await setNext({
        OrderInfo: [
          {
            OrderId: currentId,
            Address: fields.Address.value,
            ExpressId: fields.ExpressId.value
          }
        ],
        Remark: "这个是什么备注字段？"
      });
    } catch (e) {
      throw new Error(e);
    }
  };
  handleOk() {
    const { selectedRowKeys } = this.state;
    this.setState({ memberList: JSON.parse(selectedRowKeys) });
  }
  getAllMember() {
    this.setState({ visible: true });
    this.getCurrentList2({ ...this.pageConfig });
  }
  // 加载当前页
  getCurrentList2 = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount } = await getList(params);
      // todo 应该排除掉当前设置的用户
      console.log("List - ", List);
      List.forEach(v => {
        v.key = JSON.stringify(v);
      });
      this.setState({ memberList: List, current2: PageNumber });
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
      memberList
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
          dataSource={this.data}
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
            dataSource={memberList}
            columns={this.columns}
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
