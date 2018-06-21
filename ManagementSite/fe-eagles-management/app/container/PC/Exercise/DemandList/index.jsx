import React, { Component } from 'react';
import { List, Modal, Input } from 'antd';
import { connect } from "react-redux";
import ScrollBar from 'react-perfect-scrollbar';
import { getDemandList, searchDemand } from "../../../../services/demandService";
import { chooseDemand } from "../../../../actions/PC/projectAction";
import FzSearch from "../../../../components/PC/FzSearch";
import 'react-perfect-scrollbar/dist/css/styles.css';
import './list.less';
import Dotdotdot from 'react-dotdotdot';


const Search = Input.Search;

@connect(
  state => {
    return {
      project: state.projectReducer,
      user: state.userReducer,
    }
  },
  { chooseDemand }
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
      demandMembersList: []
    };
    this.getListConfig = {
      requestPage: 1,
      pageSize: 5,
      keyword: '',
      status: 1,//已发布
      type: 0,
    };
  }

  componentWillMount() {
    // let { projectMembers } = this.props.project
    // let projectMembersList = [...projectMembers]
    // this.setState({ projectMembersList })
    let config = { ...this.getListConfig, projectId: this.props.projectId }
    this.getDemand(config);
  }

  getDemand = async (params) => {
    try {
      let { data, hasMore, isReceive } = this.state;
      if (isReceive) {
        this.setState({ isReceive: false });
        let { requestPage } = params;
        params = { ...params, requestPage: requestPage++ };
        if (hasMore === true) {
          let res = await getDemandList(params);
          let { list, hasMore } = res;
          list.forEach(v => v.key = v.id);
          data = [...data, ...list];
          console.log('data - ', data);
          this.setState({
            data,
            current: requestPage,
            hasMore: Boolean(hasMore),
            isReceive: true
          });
        } else {
          console.log('没有数据可以加载了');
          return
        }
      }
    } catch (e) {
      throw new Error(e);
    }
  }
  // 下拉提示列表 关键字匹配
  fetchList = async (value) => {
    try {
      let keyword = encodeURI(value);
      let res = await getDemandList({ ...this.getListConfig, keyword });
      let { list } = res;
      return list.map(v => ({
        text: v.name,
        value: v.name,
      }));
    } catch (e) {
      throw new Error(e);
    }
  }

  // 回车搜索列表  关键字匹配
  searchList = async (value) => {
    try {
      let keyword = encodeURI(value);
      let res = await getDemandList({ ...this.getListConfig, keyword });
      const { list, hasMore } = res;
      list.forEach(v => v.key = v.id);
      this.setState({
        keyword,
        hasMore: Boolean(hasMore),
        data: list,
        isReceive: true,
        current: 2
      });
      // this._scrollRef.scrollTop = 0
      let scrollbarDom = document.querySelector('.scrollbar-container');
      scrollbarDom.scrollTop = 0;
    } catch (e) {
      throw new Error(e);
    }
  }
  handleCancle = () => {
    this.props.onCancel();
  }
  // 选择
  handleChoose = (demand) => {
    console.log('handleChoose - ', demand, this.props.project)
    // 获取之前的需求创建者
    let { demandData, projectMembers } = this.props.project;
    let { creatorId } = demand;//当前需求创建者的id
    let index = projectMembers.findIndex(member => member.user_id === creatorId)
    let member = [...projectMembers];
    console.log('当前创建者id - ', creatorId)
    console.log('是否存在 - index', index)
    let demandAuthor = {
      name: demand.creatorName,
      user_id: demand.creatorId,
      avatar: demand.creatorAvatar,
      open_id: demand.open_id,
      create: true,
    }
    // 将上一条需求用户删除
    // let { userId } = this.props.user;
    // if (projectMembers.find(member => member.user_id === demand.creatorId)) { //当前选择需求用户不在已选用户里
    /**
     * 当前需求创建用户已经包含在成员列表里，删除成员列表，并将当前需求用户加入
     * 如果不包含，则将上一次需求选择的用户删除，将这次选择的需求创建者添加到成员里
     */
    // index > -1 ? member.splice(index, 1, demandAuthor) : member = [...projectMembers, demandAuthor];
    // let newMber = member.filter(v => v.create === demandData.create)
    // }

    // if (index > -1) { //已经存在了，成员不变
    //   member.splice(index, 1, demandAuthor)
    //   // member = newMber
    // } else {
    //   let newMber = projectMembers.filter(v => v.create !== true);
    //   member = [...newMber, demandAuthor]
    // }

    let newMber = projectMembers.filter(v => v.create !== true);
    member = [...newMber, demandAuthor]
    this.props.chooseDemand({
      requirementId: demand.id,
      requirementName: demand.name,
      projectMembers: member,
      demandData: demand,
      open_id: demand.open_id,
    });
    this.handleCancle();
  }
  render() {
    const { data } = this.state;
    return (
      <Modal
        visible={true}
        title={'需求列表'}
        onCancel={this.handleCancle}
        footer={null}
        zIndex={1000}
        className='demandlist__modal'
      >
        <FzSearch
          fetchList={this.fetchList}
          search={this.searchList}
        />
        <div className="demandlist__box">
          <ScrollBar
            ref={(ref) => { this._scrollRef = ref; }}
            onYReachEnd={() => {
              this.getDemand({
                ...this.getListConfig,
                requestPage: this.state.current,
                keyword: this.state.keyword
              })
            }}
            option={{ wheelSpeed: 0.3 }}
          >
            <div className="demandlist__content">
              <List
                itemLayout="vertical"
                dataSource={data}
                locale={{ emptyText: '暂无数据' }}
                renderItem={demand => (
                  <List.Item
                    key={demand.id}
                    onClick={() => this.handleChoose(demand)}
                  >
                    <List.Item.Meta
                      title={demand.name}
                      description={
                        <div>
                          <p className='demand_desc'>
                            <Dotdotdot clamp={2}>
                              {demand.description}
                            </Dotdotdot>
                          </p>
                          <p className='demand_author'>{demand.creatorName}</p>
                        </div>
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
