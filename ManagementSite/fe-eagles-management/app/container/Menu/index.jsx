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
// const FormItem = Form.Item;
// const Option = Select.Option;
import { hashHistory } from "react-router";
import { getList, del } from "../../services/menuService";
// import { getAllArea } from "../../services/areaService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class MenuList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      menuList: [] // 列表数组
    };
    this.columns = [
      {
        title: "菜单名称",
        dataIndex: "MenuName"
      },
      {
        title: "菜单链接",
        dataIndex: "MenuLink"
      },
      // {
      //   title: "组织名称",
      //   dataIndex: "OrgName"
      // },
      {
        title: "二级菜单",
        render: obj => {
          return (
            <div>
              {obj.MenuLevel == "1" ? (
                <a
                  onClick={() => {
                    hashHistory.replace(`/menutwo/detail/${obj.MenuId}`);
                  }}
                >
                  维护二级菜单
                </a>
              ) : null}
            </div>
          );
        }
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              {obj.MenuLevel == 1 ? (
                <a
                  onClick={() =>
                    hashHistory.replace(`/menuone/detail/${obj.MenuId}`)
                  }
                >
                  编辑
                </a>
              ) : null}
              <a
                onClick={() => this.handleDelete(obj.MenuId)}
                style={{ paddingLeft: obj.MenuLevel == 1 ? "24px" : "0" }}
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
      MenuLevel: 1
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount, Message } = await getList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.MenuId;
      });
      this.setState({ menuList: List, current: PageNumber, Message });
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
  handleDelete = async MenuId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code, Message } = await del({
            MenuId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
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
  render() {
    const { pageConfig, menuList, Message } = this.state;
    // const formItemLayout = {
    //   labelCol: {
    //     xl: { span: 3 }
    //   },
    //   wrapperCol: {
    //     xl: { span: 10 }
    //   }
    // };
    return (
      <Nav>
        {/* <Row gutter={24}>
          <Col span={12}>
            <Form>
              <FormItem {...formItemLayout} label="选择组织">
                <Select>
                  <Option value="0">党组织一</Option>
                  <Option value="1">党组织二</Option>
                </Select>
              </FormItem>
            </Form>
          </Col>
        </Row> */}
        <Table
          dataSource={menuList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
        <Row
          type="flex"
          gutter={24}
        // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button className="btn btn--primary" type="primary">
              <a onClick={() => hashHistory.replace(`/menuone/detail`)}>新增</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default MenuList;
