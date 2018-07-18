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
import { getList, del } from "../../services/imageService";
import { pageMap } from "../../constants/config/appconfig";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class ImageList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      imageList: [] // 列表数组
    };
    this.columns = [
      {
        title: "机构编号",
        dataIndex: "OrgId"
      },
      {
        title: "机构名称",
        dataIndex: "OrgName"
      },
      {
        title: "页面类型",
        dataIndex: "PageId",
        render: PageId => {
          return pageMap.map((o, i) => {
            if (o.value === PageId + "") {
              console.log(o.text);
              return <span key={o.value}>{o.text}</span>;
            }
          });
        }
      },
      {
        title: "图片",
        dataIndex: "Img",
        render: image => {
          return (
            <div>
              <img style={{ width: "80px", padding: "10px 0" }} src={image} />
            </div>
          );
        }
      },
      {
        title: "操作",
        id: "1",
        render: obj => {
          return (
            <div>
              <a onClick={() => hashHistory.replace(`/image/detail/${obj.Id}`)}>
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.Id)}
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
      PageSize: 10
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
        v.key = v.Id;
      });
      this.setState({ imageList: List, current: PageNumber });
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
  handleDelete = async Id => {
    confirm({
      title: `是否确认删除?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({ Id });
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
    const { pageConfig, imageList } = this.state;
    return (
      <Nav>
        <Table
          dataSource={imageList}
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
              onClick={() => hashHistory.replace(`/image/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ImageList;
