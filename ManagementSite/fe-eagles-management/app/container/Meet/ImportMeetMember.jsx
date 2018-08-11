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
  Upload,
  Icon
} from "antd";
const FormItem = Form.Item;
import { hashHistory } from "react-router";
import { createOrEdit } from "../../services/meettingService";
import { meettingTempUrl } from "../../constants/config/appconfig";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class ImportMeetMember extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      memberList: [], // 列表数组
      fileList: []
    };
    this.columns = [
      {
        title: "参与人员姓名",
        dataIndex: "UserName"
      },
      {
        title: "联系电话",
        dataIndex: "Phone"
      },
      {
        title: "检验结果",
        dataIndex: "ErrorMessage"
      }
    ];
  }
  handleUpload = () => {
    const { fileList } = this.state;
    const file = fileList[0];
    const view = this;
    var reader = new FileReader();
    //将文件以文本形式读入页面
    reader.readAsText(file, "utf-8");
    let memberList = [];
    // reader.readAsText(file, "gb2312");
    reader.onload = function(e) {
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
                news["Phone"] = text;
            }
          });
          memberList.push({ ...news, key: Math.random() });
        }
      });
      view.setState({
        memberList
      });
    };
  };

  handleImport = async () => {
    try {
      let { memberList } = this.state;
      const { id } = this.props.params;
      let List = [];
      memberList.map((obj, index) => {
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
      message.error(e);
    }
  };

  render() {
    const { fileList, memberList } = this.state;
    const { name } = this.props.params;
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
        this.setState({
          fileList: [file]
        });
        return false;
      },
      fileList
    };
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

    return (
      <Nav>
        <Row gutter={24}>
          <Col span={24} key={111}>
            <FormItem {...formItemLayout} label="会议名称">
              <span>{name}</span>
            </FormItem>
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
            <span>仅支持txt文件，格式为XXXXX，请注意区分中英文符号</span>
          </Col>
          <Col span={3} key={6}>
            <Button type="button">
              <a href={meettingTempUrl} download="member.txt">
                下载模板
              </a>
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
            >
              <a onClick={this.handleImport}>确认导入</a>
            </Button>
          </Col>
          <Col>
            <Button className="btn ">
              <a onClick={() => hashHistory.replace(`/meetlist`)}>取消导入</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ImportMeetMember;
