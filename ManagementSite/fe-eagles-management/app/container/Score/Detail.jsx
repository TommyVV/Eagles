import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Tag,
  Icon,
  Tooltip
} from "antd";
import { connect } from "react-redux";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/scoreService";
import { saveInfo, clearInfo } from "../../actions/scoreAction";
import { scoreType } from "../../constants/config/appconfig";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      orgReducer: state.orgReducer
    };
  },
  { saveInfo, clearInfo }
)
class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      inputVisible: false, //输入框可见性
      inputValue: "", //输入框值
      maxTag: 10 //最大标签数
    };
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { score } = this.props;
          let params = {
            Info: {
              ...score,
              ...values
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = score.ScoreSetUpId ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/intergrallist");
          } else {
            let tip = score.ScoreSetUpId ? "保存失败" : "创建失败";
            message.error(tip);
          }
        } catch (e) {
          throw new Error(e);
        }
      } else {
        message.error("请检查字段是否正确");
      }
    });
  };
  change(value) {
    // 保存数据
    let { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    this.props.saveInfo({ ...values, RewardType: value });
  }
  saveInputRef = input => (this.input = input);
  handleInputChange = e => {
    this.setState({ inputValue: e.target.value });
  };
  showInput = () => {
    this.setState({ inputVisible: true }, () => this.input.focus());
  };
  // 关闭标签
  handleClose = removedTag => {
    let { getFieldsValue } = this.props.form;
    let values = getFieldsValue(); // 改变发布身份前获取填写的数据
    const Keyword = this.props.score.Keyword.filter(tag => tag !== removedTag);
    console.log("删除标签 - ", Keyword);
    this.props.saveInfo({ ...values, Keyword });
  };
  // 标签确认
  handleInputConfirm = () => {
    const { inputValue } = this.state;
    let { Keyword } = this.props.score;
    if (inputValue && Keyword.indexOf(inputValue) === -1) {
      Keyword = [...Keyword, inputValue];
    }
    console.log("添加标签 - ", Keyword);
    let { getFieldsValue } = this.props.form;
    let values = getFieldsValue(); // 改变发布身份前获取填写的数据
    this.props.saveInfo({ ...values, Keyword });
    this.setState({
      inputVisible: false,
      inputValue: ""
    });
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { score } = this.props;
    const { Keyword } = score;
    const { inputVisible, inputValue, maxTag } = this.state;
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 6 }
      }
    };
    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("ScoreSetUpId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="积分类型">
          {getFieldDecorator("RewardType")(
            <Select onChange={this.change.bind(this)}>
              {scoreType.map((o, i) => {
                return (
                  <Option value={o.value} key={i}>
                    {o.text}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="积分数">
          {getFieldDecorator("Score", {
            rules: [
              {
                required: true,
                message: "必填，请输入积分数"
              }
            ]
          })(<Input placeholder="必填，请输入积分数" />)}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="自定义设置时关键字"
          style={{ display: score.RewardType == "2" ? "block" : "none" }}
        >
          {getFieldDecorator("keyword")(
            <div>
              {Keyword.length > 0
                ? Keyword.map((tag, index) => {
                    const isLongTag = tag.length > 20;
                    const tagElem = (
                      <Tag
                        key={tag}
                        closable={true}
                        afterClose={() => this.handleClose(tag)}
                      >
                        {isLongTag ? `${tag.slice(0, 20)}...` : tag}
                      </Tag>
                    );
                    return isLongTag ? (
                      <Tooltip title={tag} key={tag}>
                        {tagElem}
                      </Tooltip>
                    ) : (
                      tagElem
                    );
                  })
                : null}
              {inputVisible && (
                <Input
                  ref={this.saveInputRef}
                  type="text"
                  size="small"
                  style={{ width: 78 }}
                  value={inputValue}
                  onChange={this.handleInputChange}
                  onBlur={this.handleInputConfirm}
                  onPressEnter={this.handleInputConfirm}
                />
              )}
              {!inputVisible &&
                Keyword.length < maxTag && (
                  <Tag
                    onClick={this.showInput}
                    style={{ background: "#fff", borderStyle: "dashed" }}
                  >
                    <Icon type="plus" /> 添加标签
                  </Tag>
                )}
            </div>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="字数"
          style={{ display: score.RewardType == "1" ? "block" : "none" }}
        >
          {getFieldDecorator("WordCount")(
            <Input placeholder="请输入字数" />
          )}
        </FormItem>
        {/* <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("state")(
            <Select>
              <Option value="0">可用</Option>
              <Option value="1">不可用</Option>
            </Select>
          )}
        </FormItem> */}
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {!this.props.score.ScoreSetUpId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/operatorlist")}
              >
                取消
              </Button>
            </Col>
          </Row>
        </FormItem>
      </Form>
    );
  }
}

const FormMap = Form.create({
  mapPropsToFields: props => {
    console.log("详情数据回显 - ", props);
    const { score } = props;
    return {
      ScoreSetUpId: Form.createFormField({
        value: score.ScoreSetUpId
      }),
      Score: Form.createFormField({
        value: score.Score
      }),
      RewardType: Form.createFormField({
        value: score.RewardType ? score.RewardType + "" : "0"
      }),
      WordCount: Form.createFormField({
        value: score.WordCount
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      scoreReducer: state.scoreReducer
    };
  },
  { saveInfo, clearInfo }
)
class ScoreDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      score: {}
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.props.clearInfo();
    }
  }

  // 根据id查询详情
  getInfo = async ScoreSetUpId => {
    try {
      const { Info } = await getInfoById({ ScoreSetUpId });
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 改成拿权限组列表
  // getAreaList = async () => {
  //   try {
  //     const { AreaInfos } = await getAllArea();
  //     this.setState({ AreaInfos });
  //   } catch (e) {
  //     message.error("获取失败");
  //     throw new Error(e);
  //   }
  // };
  render() {
    return (
      <Nav>
        <FormMap score={this.props.scoreReducer} />
      </Nav>
    );
  }
}

export default ScoreDetail;
