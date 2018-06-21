import React, { Component } from 'react';
import { List, Avatar, Modal, Row, Col, Button, Icon } from 'antd';
import { getHciUser } from "../../../services/loginService";
import ScrollBar from 'react-perfect-scrollbar';
import FzSearch from "../FzSearch";
import { connect } from "react-redux";
import 'react-perfect-scrollbar/dist/css/styles.css'
import './list.less'

/**
 * 接受已选成员  projectMembers
 * 确认选择的回调函数  confirmFn
 */
@connect(
  state => {
    return {
      user:state.userReducer,
    }
  },
  null
)
class DemandList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      data: [],
      loading: false,
      hasMore: true,
      current: 1,// 当前页
      isReceive: true,
      projectMembersList: []
    }
    this.getListConfig = {
      requestPage: 1,
      pageSize: 5,
      keyword: '',
    }
  }

  componentWillMount() {
    // let { projectMembers } = this.props.project
    // let projectMembersList = [...projectMembers]

    let { member } = this.props
    let projectMembersList = [...member]
    this.setState({ projectMembersList })
    this.getMemberList(this.getListConfig)
  }

  getMemberList = async (params) => {
    try {
      let { data, hasMore, isReceive } = this.state
      if (isReceive) {
        this.setState({ isReceive: false })
        let { requestPage } = params
        params = { ...params, requestPage: requestPage++ }
        if (hasMore === true) {
          let res = await getHciUser(params)
          let { code } = res
          if (code === 0) {
            let { resultList, hasMore } = res.data
            resultList.forEach(item => {
              item.key = item.user_id
            })
            data = [...data, ...resultList]
            console.log('requestPage - ', requestPage, data)
            this.setState({
              data,
              current: requestPage,
              hasMore: Boolean(hasMore),
              isReceive: true
            })
          }
          console.log('下拉刷新 - ', res)
        } else {
          console.log('没有数据可以加载了')
          return
        }
      }
    } catch (e) {
      throw new Error(e)
    }
  }

  handleCancle = () => {
    this.props.onCancel()
  }

  handleOk = () => {
    let { projectMembersList } = this.state
    // this.props.chooseMember({ projectMembers: projectMembersList })
    this.props.confirmFn(projectMembersList)
    this.handleCancle()
  }

  showCheck = (attr) => {
    let { projectMembersList } = this.state
    projectMembersList.push(attr)
    this.setState({
      projectMembersList,
      [attr.user_id]: true,
    });
  }

  cancelCheck = (attr) => {
    
    let { projectMembersList } = this.state
    let { user_id } = attr

    if(user_id === this.props.user.userId){
      return false;
    }
    
    let index = projectMembersList.findIndex(v => v.user_id === user_id)
    projectMembersList.splice(index, 1)
    this.setState({
      projectMembersList,
      [attr.user_id]: false,
    });
  }
  // 下拉提示列表 关键字匹配
  fetchList = async (value) => {
    try {
      let keyword = encodeURI(value)
      let res = await getHciUser({ ...this.getListConfig, keyword })
      let { resultList } = res.data
      console.log(res, resultList)
      return resultList.map(v => ({
        text: v.name,
        value: v.name,
      }))
    } catch (e) {
      throw new Error(e)
    }
  }

  // 回车搜索列表  关键字匹配
  searchList = async (value) => {
    try {
      let keyword = encodeURI(value)
      let res = await getHciUser({ ...this.getListConfig, keyword })
      const { resultList, hasMore } = res.data
      resultList.forEach(v => v.key = v.user_id)
      this.setState({
        keyword,
        hasMore: Boolean(hasMore),
        data: resultList,
        isReceive: true,
        current: 2
      })
      // this._scrollRef.scrollTop = 0
      let scrollbarDom = document.querySelector('.scrollbar-container')
      scrollbarDom.scrollTop = 0
    } catch (e) {
      throw new Error(e)
    }
  }
  render() {
    let { data, projectMembersList } = this.state
    return (
      <Modal
        visible={true}
        title={'添加项目成员'}
        onOk={this.handleOk}
        onCancel={this.handleCancle}
        zIndex={1000}
        footer={[
          <Button key="back" onClick={this.handleCancle}>取消</Button>,
          <Button key="submit" type="primary" onClick={this.handleOk}>
            确认
          </Button>,
        ]}
        className='memberlist__modal'
      >
        <FzSearch
          fetchList={this.fetchList}
          search={this.searchList}
        />
        <div className="memberlist__box">
          <ScrollBar
            onYReachEnd={() => {
              this.getMemberList({
                ...this.getListConfig,
                requestPage: this.state.current,
              })
            }}
            option={{ wheelSpeed: 0.3 }}
          >
            <div className="memberlist__content">
              <List
                itemLayout="vertical"
                dataSource={data}
                locale={{ emptyText: '暂无数据' }}
                renderItem={item => (
                  <List.Item
                    key={item.id}
                  >
                    <List.Item.Meta
                      description={
                        <Row className='member_item'>
                          <Col span={2}>
                            <Avatar src={item.avatar} />
                          </Col>
                          <Col span={22} onClick={() => this.state[item.user_id] || projectMembersList.find(v => v.user_id === item.user_id) ? this.cancelCheck(item) : this.showCheck(item) }>
                            <span className='member-name'>{item.name}</span>
                            <span className='member-mobile'>{item.mobile}</span>
                            {
                              this.state[item.user_id] || projectMembersList.find(v => v.user_id === item.user_id) ?
                                <Icon type="check-circle-o" className={item.user_id === this.props.user.userId ? 'default' : 'check'}  />
                                : <span className='member-default' />
                            }
                          </Col>
                          {/* <Col span={22} onClick={() => this.state[item.user_id] || projectMembersList.find(v => v.user_id === item.user_id) ? this.cancelCheck(item) : this.showCheck(item)}>
                            <span className='member-name'>{item.name}</span>
                            <span className='member-mobile'>{item.mobile}</span>
                            {
                              this.state[item.user_id] || projectMembersList.find(v => v.user_id === item.user_id) ?
                                <Icon type="check-circle-o" className={item.user_id === this.props.user.userId ? 'default' : 'check'} onClick={() => this.cancelCheck(item)} />
                                : <span className='member-default' onClick={() => this.showCheck(item)} />
                            }
                          </Col> */}
                        </Row>
                      }
                    />
                  </List.Item>
                )}
              />
            </div>
          </ScrollBar >
        </div>
      </Modal>
    );
  }
}

export default DemandList;
