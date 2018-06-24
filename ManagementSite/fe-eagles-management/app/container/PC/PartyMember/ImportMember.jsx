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
  DatePicker,
  Upload,
  Icon
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import {
  getProjectInfoById,
  getProjectList,
  deleteProject,
  updateProject
} from "../../../services/projectService";
import util from "../../../utils/util";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class ImportMember extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      projectList: [], // 项目列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "党员名称",
        dataIndex: "name",
        width: "25%"
      },
      {
        title: "所属支部",
        dataIndex: "branch",
        width: "25%"
      },
      {
        title: "联系电话",
        dataIndex: "phone",
        width: "25%"
      },
      {
        title: "党员类型",
        dataIndex: "type",
        width: "20%"
      }
    ];
    this.data = [
      {
        key: "1",
        name: "张三",
        branch: "第一支部",
        phone: "18512144992",
        type: "党员"
      },
      {
        key: "2",
        name: "张四",
        branch: "第二支部",
        phone: "18512144992",
        type: "预备党员"
      },
      {
        key: "3",
        name: "张五",
        branch: "第二支部",
        phone: "18512144992",
        type: "预备党员"
      }
    ];
    this.getListConfig = {
      requestPage: 1,
      pageSize: 6,
      keyword: ""
    };
  }
  componentWillMount() {
    // this.getCurrentList(this.getListConfig);
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  // getCurrentList = async params => {
  //   try {
  //     let { keyword, requestPage } = params;
  //     let config = { ...this.getListConfig, requestPage, keyword };
  //     let { totalSize, projectList } = await getProjectList(config);
  //     console.log("projectList - ", projectList);
  //     projectList.forEach(v => (v.key = v.projectId));
  //     this.setState({ projectList, current: requestPage });
  //     this.updatePageConfig(totalSize);
  //   } catch (e) {
  //     message.error("获取失败");
  //     throw new Error(e);
  //   }
  // };
  // 更新分页配置
  updatePageConfig(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.pageSize,
      current: this.state.current,
      onChange: async (page, pagesize) => {
        this.getCurrentList({
          ...this.getListConfig,
          requestPage: page,
          keyword: this.state.keyword
        });
      }
    };
    this.setState({ pageConfig });
  }
  // 下拉提示列表 关键字匹配
  fetchList = async value => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword };
      let { projectList } = await getProjectList(params);
      return projectList.map(v => ({
        text: v.projectName,
        value: v.projectName
      }));
    } catch (e) {
      throw new Error(e);
    }
  };
  // 回车搜索列表  关键字匹配
  searchList = async value => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword };
      let { projectList, totalSize } = await getProjectList(params);
      projectList.forEach(v => (v.key = v.projectId));
      this.setState({
        keyword,
        projectList,
        current: 1
      });
      this.updatePageConfig(totalSize);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 删除项目
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
  // 编辑项目
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

  render() {
    const { selectedRowKeys, pageConfig, projectList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    const props = {
      name: "file",
      action: "//jsonplaceholder.typicode.com/posts/",
      headers: {
        authorization: "authorization-text"
      },
      onChange(info) {
        if (info.file.status !== "uploading") {
          console.log(info.file, info.fileList);
        }
        if (info.file.status === "done") {
          message.success(`${info.file.name} file uploaded successfully`);
        } else if (info.file.status === "error") {
          message.error(`${info.file.name} file upload failed.`);
        }
      }
    };

    return (
      <Nav>
        <Row gutter={24}>
          <Col span={2} key={1}>
            <FormItem label="选择导入文件" />
          </Col>
          <Col span={3} key={2}>
            <Upload {...props}>
              <Button>
                <Icon type="upload" />上传
              </Button>
            </Upload>
          </Col>
          <Col span={3} key={3}>
            <Button>预览</Button>
          </Col>
        </Row>
        <Row gutter={24} >
          <Col span={2} key={4}>
            <FormItem label="规则说明" />
          </Col>
          <Col span={8} key={5} className="upload-tip">
            <span >支持txt文件.csv文件, 格式为XXXXX请注意区分中英文符号</span>
          </Col>
          <Col span={3} key={6}>
            <Button>下载模板</Button>
          </Col>
        </Row>
        <Table
          dataSource={this.data}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row
          type="flex"
          justify="center"
          gutter={24}
        >
          <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col>
          <Col>
            <Button type="primary" className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/partymember/detail`)}>
              确认导入
              </a>
            </Button>
          </Col>
          <Col>
            <Button className="btn ">
              <a onClick={() => hashHistory.replace(`/exercise/create`)}>
                取消导入
              </a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ImportMember;
