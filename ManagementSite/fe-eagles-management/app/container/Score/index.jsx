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
import { getList, del } from "../../services/scoreService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class ScoreList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      scoreList: [] // 列表数组
    };
    this.columns = [
      {
        title: "积分类型",
        dataIndex: "RewardType"
      },
      {
        title: "增加积分",
        dataIndex: "Score"
      },
      {
        title: "是否可用",
        dataIndex: "WordCount",
        render: text => <span>{text}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/intergral/detail/${obj.ScoreSetUpId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.ScoreSetUpId)}
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
      RewardType: ""
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = this.getListConfig;
    try {
      let { List, TotalCount } = await getList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.ScoreSetUpId;
      });
      this.setState({ scoreList: List, current: PageNumber });
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
  // 删除
  handleDelete = async ScoreSetUpId => {
    confirm({
      title: `是否确认删除?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({ ScoreSetUpId });
          if (Code === "00") {
            message.success(`删除成功`);
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error(`删除失败`);
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  render() {
    const { pageConfig, scoreList } = this.state;
    return (
      <Nav>
        <Table
          dataSource={scoreList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Row type="flex" gutter={24}>
          <Col>
            <Button
              className="btn btn--primary"
              type="primary"
              onClick={() => hashHistory.replace(`/intergral/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ScoreList;
