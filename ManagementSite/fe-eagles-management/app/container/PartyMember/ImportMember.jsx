import React, { Component } from "react";
import {
  Table,
  Row,
  Col,
  Button,
  message,
  Modal,
  Form,
  Select,
  Upload,
  Icon
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import { bitchCreate } from "../../services/memberService";
import { getList } from "../../services/branchService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class ImportMember extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      memberList: [], // 列表数组
      fileList: [],
      branchList: [],
      currentBranch: ""
    };
    this.columns = [
      {
        title: "党员名称",
        dataIndex: "UserName",
        key: "UserName"
      },
      {
        title: "性别",
        dataIndex: "Sex",
        key: "Sex",
        render: Sex => {
          console.log(Sex)
          return Sex == 0 ? "男" : "女";
        },
      },
      {
        title: "联系电话",
        dataIndex: "Phone",
        key: "Phone"
      },
      {
        title: "党员类型",
        dataIndex: "MemberType",
        render: MemberType => {
          console.log(MemberType)
          return MemberType == 0 ? "正式党员" : "预备党员";
        },
        key: "MemberType"
      },
      {
        title: "验证结果",
        dataIndex: "ErrorReason",
        key: "ErrorReason"
      }
    ];
  }
  componentWillMount() {
    this.getCurrentList();
  }
  // 加载当前页
  getCurrentList = async () => {
    try {
      let { List } = await getList({
        PageNumber: 1,
        PageSize: 10000
      });
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ branchList: List, currentBranch: List[0].BranchId });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  handleUpload = () => {
    const { fileList } = this.state;
    const file = fileList[0];
    const view = this;
    var reader = new FileReader();
    let memberList = [];
    //将文件以文本形式读入页面
    reader.readAsText(file, "utf-8");
    // reader.readAsText(file, "gb2312");
    reader.onload = function (e) {
      var fileText = e.target.result.split("\n");
      fileText.map((data, index) => {
        if (data.length && index > 0) {
          data = data.split(",");
          let news = {};
          data.map((text, i) => {
            switch (i) {
              case 0:
                news["UserName"] = text;
              case 1:
                news["Sex"] = text;
              case 2:
                news["Phone"] = text;
              case 3:
                news["MemberType"] = text;
            }
          });
          memberList.push({ ...news, key: Math.random() });
        }
      });
      view.setState({ memberList });
    };
  };

  handleImport = async () => {
    try {
      let { memberList, currentBranch } = this.state;
      if (currentBranch) {
        let newKeys = [];
        memberList.map(o => {
          newKeys.push({
            UserName: o.UserName,
            Phone: o.Phone,
            Sex: o.Sex,
            MemberType: o.MemberType,
            ImportStatus: true,
            ErrorReason: ""
          });
        });
        let { Code, Result, Message } = await bitchCreate({
          UserList: newKeys,
          BranchId: currentBranch
        });
        let { UserList, ImportStart } = Result;
        if (Code === "00") {
          if (ImportStart == "0") {
            message.error("导入失败");
          } else if (ImportStart == "1") {
            message.success("导入成功");
          } else if (ImportStart == "2") {
            message.success("导入部分成功");
          }
        }
        UserList.forEach(v => {
          v.key = Math.random();
        });
        this.setState({ memberList: UserList })
      } else {
        message.error("请选择支部");
      }
    } catch (e) {
      message.error(e);
    }
  };
  changeSelect(value) {
    this.state.currentBranch = value;
  }
  render() {
    const { fileList, memberList, branchList } = this.state;
    const props = {
      name: "file",
      action: "",
      onRemove: file => {
        this.setState(({ fileList }) => {
          const index = fileList.indexOf(file);
          const newFileList = fileList.slice();
          newFileList.splice(index, 1);
          return { fileList: newFileList, memberList: [] };
        });
      },
      beforeUpload: file => {
        var txt = file.name.slice(file.name.lastIndexOf(".") + 1).toLowerCase();
        if ("txt" != txt) {
          message.error("只支持格式为txt的文件!");
          return false;
        }
        this.setState({
          fileList: [file]
        });
        return false;
      },
      fileList
    };

    return (
      <Nav>
        <Row gutter={24}>
          <Col span={2} key={111}>
            <FormItem label="选择支部" />
          </Col>
          <Col span={5} key={22}>
            {branchList.length ? (<Select
              onChange={this.changeSelect.bind(this)}
              style={{ width: "100%" }}
              defaultValue={branchList[0].BranchId}
            >
              {branchList.map((o, i) => {
                return (
                  <Option key={i} value={o.BranchId} >
                    {o.BranchName}
                  </Option>
                );
              })}
            </Select>) : null}

          </Col>
        </Row>
        <Row gutter={24}>
          <Col span={2} key={1}>
            <FormItem label="选择导入文件" />
          </Col>
          <Col span={3} key={2}>
            <Upload {...props}>
              <Button>
                <Icon type="upload" />
                选择
              </Button>
            </Upload>
          </Col>
          <Col span={3} key={3}>
            <Button
              className="upload-demo-start"
              type="primary"
              onClick={this.handleUpload}
              disabled={fileList.length === 0}
            >
              预览
            </Button>
          </Col>
        </Row>
        <Row gutter={24}>
          <Col span={2} key={4}>
            <FormItem label="规则说明" />
          </Col>
          <Col span={8} key={5} className="upload-tip">
            <span>仅支持txt文件，格式为XX.txt，请注意区分中英文符号</span>
          </Col>
          <Col span={3} key={6}>
            <Button type="button">
              <a href="../../file/member.txt" download="member.txt">下载模板</a>
            </Button>
          </Col>
        </Row>
        <Table
          dataSource={memberList}
          columns={this.columns}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row type="flex" justify="center" gutter={24}>
          <Col>
            <Button
              type="primary"
              className="btn btn--primary"
              disabled={memberList.length === 0}
              onClick={this.handleImport}
            >
              确认导入
            </Button>
          </Col>
          <Col>
            <Button
              className="btn "
              onClick={() => hashHistory.replace(`/partymemberlist`)}
            >
              取消导入
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ImportMember;
