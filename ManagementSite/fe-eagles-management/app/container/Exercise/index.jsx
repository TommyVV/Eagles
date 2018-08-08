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
import { hashHistory } from "react-router";
import moment from "moment";
import {
  getQuestionList,
  deleteQuestion
} from "../../services/questionService";
import { typeMap, stateMap } from "../../constants/config/appconfig";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
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
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
    const { getCurrentList, setObj } = this.props;
    let params = {
      PageNumber: 1,
      PageSize: 10
    };
    getCurrentList(params);
    setObj({});
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    return <Form className="ant-advanced-search-form" layout="inline" onSubmit={this.handleSearch}>
        <Row gutter={24}>
          <Col span={5} key={1}>
            <FormItem label="习题类型">
              {getFieldDecorator("ExercisesType")(<Select>
                  <Option value="0">全部</Option>
                  {typeMap.map((obj, index) => {
                    return <Option key={index} value={obj.ExercisesType}>
                        {obj.text}
                      </Option>;
                  })}
                </Select>)}
            </FormItem>
          </Col>
          <Col span={5} key={2}>
            <FormItem label="标题">
              {getFieldDecorator(`ExercisesName`)(<Input />)}
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
                <span style={{ display: "inline-block", width: "100%", textAlign: "center" }}>
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
          {/* <Col span={5} key={4}>
            <FormItem label="状态">
              {getFieldDecorator("Status")(
                <Select>
                  <Option value="0">全部</Option>
                  {stateMap.map((obj, index) => {
                    return (
                      <Option key={index} value={obj.Status}>
                        {obj.text}
                      </Option>
                    );
                  })}
                </Select>
              )}
            </FormItem>
          </Col> */}
          <Col span={5} style={{ textAlign: "left", paddingTop: "4px" }}>
            <Button type="primary" htmlType="submit">
              搜索
            </Button>
            <Button style={{ marginLeft: 8 }} onClick={this.handleReset}>
              清空
            </Button>
          </Col>
        </Row>
      </Form>;
  }
}

const WrapperSearchForm = Form.create({
  mapPropsToFields: props => {
    const { obj } = props;
    return {
      ExercisesName: Form.createFormField({
        value: obj.ExercisesName
      }),
      ExercisesType: Form.createFormField({
        value: obj.ExercisesType ? obj.ExercisesType + "" : "0"
      }),
      // Status: Form.createFormField({
      //   value: obj.Status ? obj.Status + "" : "0"
      // }),
      StartTime: Form.createFormField({
        value: obj.StartTime ? moment(obj.StartTime, "YYYY-MM-dd") : null
      }),
      EndTime: Form.createFormField({
        value: obj.EndTime ? moment(obj.EndTime, "YYYY-MM-dd") : null
      }),
    };
  }
})(SearchForm);

class Exercise extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // id数组
      questionList: [], // 列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {}, // 当前页配置
      obj: {}
    };
    this.columns = [
      {
        title: "试卷编号",
        dataIndex: "ExercisesId"
      },
      {
        title: "试卷名称",
        dataIndex: "ExercisesName"
      },
      {
        title: "习题类型",
        dataIndex: "ExercisesType",
        render: text => (
          <span>
            {typeMap.map(obj => {
              if (obj.ExercisesType == text) {
                return obj.text;
              }
            })}
          </span>
        )
      },
      // {
      //   title: "状态",
      //   dataIndex: "Status",
      //   render: text => (
      //     <span>
      //       {stateMap.map(obj => {
      //         if (obj.Status == text) {
      //           return obj.text;
      //         }
      //       })}
      //     </span>
      //   )
      // },
      {
        title: "发布时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/question/detail/${obj.ExercisesId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.ExercisesId)}
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
      StartTime: "",
      EndTime: "",
      ExercisesType: "",
      ExercisesName: "",
      Status: ""
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  getCurrentList = async params => {
    try {
      let { List, TotalCount } = await getQuestionList(params);
      console.log(List, TotalCount);
      List.forEach(v => (v.key = v.ExercisesId));
      let { PageNumber } = params;
      this.setState({ questionList: List, current: PageNumber });
      this.updatePageConfig(TotalCount);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 更新分页配置
  updatePageConfig(TotalCount) {
    let pageConfig = {
      total: TotalCount,
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
  handleDelete = async id => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await deleteQuestion({
            ExercisesId: id
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
            message.error("删除失败");
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  // 编辑
  handleEdit = async () => {
    try {
      let { selectedRowKeys } = this.state;
      console.log(selectedRowKeys);
      if (selectedRowKeys.length > 1) {
        return message.error("不能同时编辑多个项目");
      }
      if (selectedRowKeys.length === 0) {
        return message.error("请选择需要编辑的项目");
      }
      hashHistory.replace(`/project/create/${selectedRowKeys[0]}`);
    } catch (e) {
      throw new Error(e);
    }
  };
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  // 间接调用getCurrentList
  getCurrent(params) {
    this.getCurrentList(params);
  }
  render() {
    const { selectedRowKeys, pageConfig, questionList, obj } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrent.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={questionList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row type="flex" gutter={24}>
          <Col>
            <Button type="primary" className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/question/detail`)}>
                新增试卷
              </a>
            </Button>
          </Col>
          {/* <Col>
            <Button onClick={() => this.handleDelete()} className="btn">
              批量删除
            </Button>
          </Col> */}
        </Row>
      </Nav>
    );
  }
}

export default Exercise;
