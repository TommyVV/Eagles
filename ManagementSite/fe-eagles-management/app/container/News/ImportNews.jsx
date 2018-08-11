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
import { hashHistory } from "react-router";
import { bitchCreate } from "../../services/newsService";
import { newsTempUrl } from "../../constants/config/appconfig";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class ImportNews extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      newsList: [], // 新闻列表数组
      fileList: []
    };
    this.columns = [
      {
        title: "新闻标题",
        dataIndex: "NewsName"
      },
      {
        title: "新闻链接",
        dataIndex: "ExternalUrl"
      },
      {
        title: "新闻图片链接",
        dataIndex: "NewsImg"
      },
      {
        title: "新闻来源",
        dataIndex: "Source"
      },
      {
        title: "验证结果",
        dataIndex: "result"
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
    let newsList = [];
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
                news["NewsName"] = text;
              case 1:
                news["ExternalUrl"] = text;
              case 2:
                news["NewsImg"] = text;
              case 3:
                news["Source"] = text;
            }
          });
          newsList.push({ ...news, key: Math.random() });
        }
      });
      view.setState({
        newsList
      });
    };
  };

  handleImport = async () => {
    try {
      let { newsList } = this.state;
      let { Code, Result, Message } = await bitchCreate({
        Info: newsList
      });
      if (Code === "00") {
        message.success("导入成功");
        this.setState({ memberList: Result.UserList });
      } else {
        message.error(Message);
        this.setState({ memberList: Result.UserList });
      }
    } catch (e) {
      message.error(e);
    }
  };

  render() {
    const { fileList, newsList } = this.state;
    const props = {
      name: "file",
      action: "",
      onRemove: file => {
        this.setState(({ fileList }) => {
          const index = fileList.indexOf(file);
          const newFileList = fileList.slice();
          newFileList.splice(index, 1);
          return { fileList: newFileList, newsList: [] };
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
      fileList: this.state.fileList
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
              <a href={newsTempUrl}>下载模板</a>
            </Button>
          </Col>
        </Row>
        <Table
          dataSource={newsList}
          columns={this.columns}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row type="flex" justify="center" gutter={24}>
          <Col>
            <Button
              type="primary"
              className="btn btn--primary"
              disabled={newsList.length === 0}
            >
              <a onClick={this.handleImport}>确认导入</a>
            </Button>
          </Col>
          <Col>
            <Button className="btn ">
              <a onClick={() => hashHistory.replace(`/newslist`)}>取消导入</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ImportNews;
