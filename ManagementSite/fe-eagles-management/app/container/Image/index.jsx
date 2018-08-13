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
import { getOrgList } from "../../services/orgService";
import { pageMap } from "../../constants/config/appconfig";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;
class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      List: []
    };
  }
  componentWillMount() {
    this.getCurrentList();
  }
  // 加载当前页
  getCurrentList = async () => {
    try {
      let { List } = await getOrgList({
        PageNumber: 1,
        PageSize: 10000
      });
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  handleSearch = value => {
    let params = {
      PageNumber: 1,
      PageSize: 10,
      OrgId: value
    };
    const { getCurrentList, setObj } = this.props;
    getCurrentList(params);
    setObj({ OrgId: value });
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { List } = this.state;
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch.bind(this)}
      >
        <Row gutter={24}>
          <Col span={5} key={6}>
            <FormItem label="机构">
              {getFieldDecorator(`OrgId`)(
                <Select onChange={this.handleSearch.bind(this)}>
                  <Option value="0">全部</Option>
                  {List.map((o, i) => {
                    return (
                      <Option key={i} value={o.OrgId}>
                        {o.OrgName}
                      </Option>
                    );
                  })}
                </Select>
              )}
            </FormItem>
          </Col>
          {/* <Col span={6} key={2}>
            <FormItem label="权限组名称">
              {getFieldDecorator(`AuthorityGroupName`)(<Input />)}
            </FormItem>
          </Col> */}
          {/* <Col
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
          </Col> */}
        </Row>
      </Form>
    );
  }
}

const WrapperSearchForm = Form.create({
  mapPropsToFields: props => {
    console.log(this);
    console.log(props);
    return {
      // AuthorityGroupName: Form.createFormField({
      //   value: props.obj.AuthorityGroupName
      // }),
      OrgId: Form.createFormField({
        value: props.obj.OrgId ? props.obj.OrgId : "0"
      })
    };
  }
})(SearchForm);
class ImageList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      imageList: [], // 列表数组
      current: 1, // 当前页
      pageConfig: {}, // 当前页配置
      obj: {}
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
      PageSize: 10,
      OrgId: ""
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
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
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const { pageConfig, imageList, obj } = this.state;
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrentList.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
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
