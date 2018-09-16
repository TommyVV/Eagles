import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Table
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/meettingService";
import "./style.less";

const FormItem = Form.Item;

class MeetDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [],
      detail: {}
    };

    this.columns = [
      {
        title: "参与人员姓名",
        dataIndex: "UserName",
        key: "UserName"
      },
      {
        title: "联系电话",
        dataIndex: "Phone",
        key: "Phone"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a onClick={() => this.handleDelete(obj.Phone)}>删除</a>
            </div>
          );
        }
      }
    ];
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿新闻详情
    }
  }

  // 根据id查询详情
  getInfo = async MeetingId => {
    try {
      let { List } = await getInfoById({
        MeetingId
      });
      console.log("List", List);
      let member = List[0].Participants;
      member.forEach(v => {
        v.key = v.Phone;
      });
      console.log(List[0]);
      this.setState({
        detail: List[0]
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  onSelectChange = selectedRowKeys => {
    console.log(selectedRowKeys);
    this.setState({ selectedRowKeys });
  };
  // 删除项目
  handleDelete = async Phone => {
    try {
      let { detail, selectedRowKeys } = this.state;
      let member = detail.Participants;
      delete member[
        member.findIndex(i => {
          if (i.Phone == Phone) {
            delete selectedRowKeys[selectedRowKeys.findIndex(i => i == Phone)];
            selectedRowKeys = selectedRowKeys.filter(item => item); // 删掉复选的id
            return true;
          }
        })
      ];
      detail.Participants = member.filter(item => item);
      this.setState({
        detail,
        selectedRowKeys
      });
    } catch (e) {
      throw new Error(e);
    }
  };
  // 编辑分享内容
  handleDeleteBitch = () => {
    let { detail, selectedRowKeys } = this.state;
    if (selectedRowKeys.length == 0) {
      return message.error("请选择删除的人员");
    }
    let member = detail.Participants;
    selectedRowKeys.map((v, index) => {
      const user = JSON.parse(v);
      delete member[
        member.findIndex(i => {
          if (i.Phone == user.Phone) {
            delete selectedRowKeys[index];
            selectedRowKeys = selectedRowKeys.filter(item => item); // 删掉复选的id
            console.log(selectedRowKeys);
            return true;
          }
        })
      ];
      member = member.filter(item => item);
      console.log(member);
    });
    detail.Participants = member.filter(item => item); // 过滤掉空元素
    console.log(detail);
    this.setState({
      detail,
      selectedRowKeys
    });
  };
  save = async () => {
    try {
      const { id } = this.props.params;
      const { detail } = this.state;
      const member = detail.Participants;
      let List = [];
      member.map((obj, index) => {
        List.push({
          MeetUserName: obj.UserName,
          MeetUserPhone: obj.Phone,
          ErrorMessage: ""
        });
      });
      let params = {
        MeetingId: id,
        List
      };
      let { Code } = await createOrEdit(params);
      if (Code === "00") {
        message.success("保存成功");
        hashHistory.replace("/meetlist");
      } else {
        message.error("保存失败");
      }
    } catch (e) {
      throw new Error(e);
    }
  };
  render() {
    const { detail } = this.state;
    let { id, name } = this.props.params;
    const { selectedRowKeys } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    let keys = detail.Participants;
    keys && keys.map((v, i) => (v.key = JSON.stringify({ ...v, key: i })));
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 6 }
      }
    };
    const formItemLayoutTable = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 16 }
      }
    };
    return (
      <Nav>
        <Form onSubmit={this.handleSubmit}>
          <FormItem {...formItemLayout} label="会议名称">
            <Input disabled value={detail.MeetingName} />
          </FormItem>
          <FormItem {...formItemLayout} label="会议发起人">
            <Input disabled value={detail.MeetingInitiator} />
          </FormItem>
          <FormItem {...formItemLayoutTable} label="参会人员">
            <Table
              dataSource={keys}
              columns={this.columns}
              rowSelection={rowSelection}
              locale={{ emptyText: "未查询到符合条件的信息" }}
              bordered
            />
          </FormItem>
          <FormItem>
            <Row gutter={24}>
              <Col span={2} offset={2}>
                <Button
                  onClick={this.handleDeleteBitch}
                  type="primary"
                  className="btn btn--primary"
                >
                  批量删除
                </Button>
              </Col>
              <Col span={2} offset={1}>
                <Button
                  className="btn btn--primary"
                  type="primary"
                  onClick={this.save}
                >
                  保存
                </Button>
              </Col>
              <Col span={2} offset={1}>
                <Button
                  className="btn"
                  onClick={() => hashHistory.replace("/meetlist")}
                >
                  取消
                </Button>
              </Col>
            </Row>
          </FormItem>
        </Form>
      </Nav>
    );
  }
}

export default MeetDetail;
