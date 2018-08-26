import React, { Component } from "react";
import {
  Table,
  message,
  Modal,
  Form,
  Select,
} from "antd";
import { hashHistory } from "react-router";
import { getListBranch, getListOrg } from "../../services/publicArticleService";
import { articleMap, auditStatus } from "../../constants/config/appconfig";

import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class PublicArticleList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [], // 列表数组
      authMap: new Map(),
    };
    this.columns = [
      {
        title: "标题",
        dataIndex: "NewsTitle"
      },
      {
        title: "作者",
        dataIndex: "Name"
      },
      {
        title: "类型",
        dataIndex: "NewsTypeName",
        // render: NewsType => {
        //   return articleMap.map((o, i) => {
        //     if (o.value === NewsType + "") {
        //       console.log(o.text);
        //       return <span key={o.value}>{o.text}</span>;
        //     }
        //   });
        // }
      },
      {
        title: "发布时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      },
      {
        title: "审核状态",
        dataIndex: "Status",
        render: text => {
          return auditStatus.map(o => {
            return o.Status == text ? <span key="1">{o.text}</span> : null
          })
        }
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/article/detail/${obj.NewsId}/0/${
                    this.props.params.type
                    }`)
                }
                style={{ paddingRight: "24px" }}
              >
                详情
              </a>
              <a
                onClick={() =>
                  hashHistory.replace(`/article/detail/${obj.NewsId}/1/${
                    this.props.params.type
                    }`)
                }
                style={{
                  display:
                    (this.state.authMap.get("open0006") || this.state.authMap.get("open0003")) && obj.Status == "-1"
                      ? null
                      : "none"
                }}
              >
                审核
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
    const auth = JSON.parse(localStorage.info).authList;
    if (auth) {
      const authMap = new Map();
      auth.map((a, i) => {
        authMap.set(a.FunCode, a.FunCode);
      });
      this.setState({
        authMap
      });
    }
    const { type } = this.props.params;
    this.getCurrentList(this.getListConfig, type);
  }


  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      const { type } = this.props.params;
      let res = {};
      if (type == "2") {
        res = await getListBranch(params);
      } else if (type == "1") {
        res = await getListOrg(params);
      }
      console.log("res - ", res);
      if (res.Aritcles) {
        res.Aritcles.forEach(v => {
          v.key = v.NewsId;
          switch (v.NewsType) {
            case "0":
              v.NewsTypeName = "文章";
              break;
            case "1":
              v.NewsTypeName = "心得体会";
              break;
            case "2":
              v.NewsTypeName = "会议";
              break;
            case "3":
              v.NewsTypeName = "入党申请书";
              break
          }
        });
        this.setState({ List: res.Aritcles, current: PageNumber });
        this.updatePageConfig(res.TotalCount);
      }
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
  render() {
    const { pageConfig, List } = this.state;
    return (
      <Nav>
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

export default PublicArticleList;
