import React, { Component } from "react";
import {
  Table,
  Row,
  Col,
  Button,
  message,
  Form,
  Input,
  DatePicker
} from "antd";
const FormItem = Form.Item;
import Nav from "../Nav";
import "./style.less";
import { getList } from "../../services/auditService";
import moment from "moment";
import "moment/locale/zh-cn";
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
      PageSize: 10,
      AuditName: "",
      StartTime: "",
      EndTime: "",
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
            <FormItem label="标题">
              {getFieldDecorator(`AuditName`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={8} key={3}>
            <FormItem label="发布时间">
              <Col span={11}>
                <FormItem>
                  {getFieldDecorator("StartTime")(
                    <DatePicker placeholder="开始时间" />
                  )}
                </FormItem>
              </Col>
              <Col span={2}>
                <span
                  style={{
                    display: "inline-block",
                    width: "100%",
                    textAlign: "center"
                  }}
                >
                  -
                </span>
              </Col>
              <Col span={11}>
                <FormItem>
                  {getFieldDecorator("EndTime")(
                    <DatePicker placeholder="结束时间" />
                  )}
                </FormItem>
              </Col>
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
      AuditName: Form.createFormField({
        value: props.obj.AuditName
      }),
      StartTime: Form.createFormField({
        value: props.obj.StartTime ? moment(props.obj.StartTime, "YYYY-MM-dd") : null
      }),
      EndTime: Form.createFormField({
        value: props.obj.EndTime ? moment(props.obj.EndTime, "YYYY-MM-dd") : null
      })
    };
  }
})(SearchForm);


class AuditList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [], // 项目列表数组
      current: 1, // 当前页
      pageConfig: {}, // 当前页配置
      obj: {}
    };
    this.columns = [
      {
        title: "行号",
        dataIndex: "key",
      },
      {
        title: "标题",
        dataIndex: "AuditName",
      },
      {
        title: "审核类型",
        dataIndex: "NewsType",
      },
      {
        title: "审核人",
        dataIndex: "UserId",
      },
      {
        title: "审核时间",
        dataIndex: "CreateTime",
      },
      {
        title: "审核状态",
        dataIndex: "AuditStatus",
        render: text => <span>{text == "0" ? "通过" : "拒绝"}</span>,
      },
      {
        title: "原因",
        dataIndex: "Reason",
      },
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      AuditName: "",
      StartTime: "",
      EndTime: "",
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 间接调用getCurrentList
  getCurrent(params) {
    this.getCurrentList(params);
  }
  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount } = await getList(params);
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i + 1;
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
      PageSize: this.getListConfig.PageSize,
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

  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const {
      pageConfig,
      List,
      obj
    } = this.state;
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
          // rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
      </Nav>
    );
  }
}

export default AuditList;
