import React, { Component } from 'react';
import { ListView, PullToRefresh } from 'antd-mobile';
import CommentItem from "./CommentItem";
import SelectComment from "../../OP/SelectComment";
import { getCommentList } from "../../../services/commentService";
import './style.less';

let pageIndex = 1;// 页码
class Comment extends Component {
  constructor(props) {
    super(props);
    const dataSource = new ListView.DataSource({
      rowHasChanged: (row1, row2) => row1 !== row2
    });
    this.state = {
      dataSource,
      commentList: [],//每页拿到的数据
      totalSize: '',
      refreshing: true,
      isLoading: true,
      height: document.documentElement.clientHeight,
      hasMore: true,
      op: false,//运营
    };
    this.getListConfig = {
      pageSize: 10,
      requestPage: 1,
    }
  }
  componentDidUpdate() {
    // document.body.style.overflow = 'auto';
  }

  componentDidMount() {
    this.getCommentListFn(this.getListConfig);
    // const hei = this.state.height - ReactDOM.findDOMNode(this.lv).offsetTop;//设置高度
    // this.setState({
    //   height: hei,
    //   refreshing: false,
    //   isLoading: false,
    // });
  }
  // 拼接参数
  appendParam(param) {
    const { id, type } = this.props;
    // 0:分享的评论；1：项目需求的评论
    if (type == 'share') {
      param.type = 0;
      param.id = id;
    } else if (type == 'demand') {
      param.type = 1;
      param.id = id;
    }
    param.handle = 1; // 0是审核
    return param;
  }

  // 获取数据
  getCommentListFn = async (param, onRefresh) => {
    try {
      let { requestPage, pageSize } = param;
      param = this.appendParam(param);
      let { commentList, totalSize } = await getCommentList(param);
      let hasMore = totalSize > requestPage * pageSize ? true : false;
      let oldCommentList = this.state.commentList;
      let length = commentList.length;
      let list = [];
      if (onRefresh) { // 下拉刷新最新数据，删除数据源对应条数，在合并新的数据
        for (let index = 0; index < length; index++) {
          oldCommentList.shift();
        }
        list = [...commentList, ...oldCommentList];
      } else {
        list = [...oldCommentList, ...commentList];
      }
      this.setState({
        commentList: list,
        dataSource: this.state.dataSource.cloneWithRows(list),
        hasMore,
        totalSize,
        isLoading: false,
      });
    } catch (e) {
      throw new Error(e);
    }
  }
  // 加载最新数据
  onRefresh = async () => {
    this.setState({ refreshing: true, isLoading: true });
    await this.getShareList(this.getListConfig, true)
    this.setState({ refreshing: false });
  };
  // 滚动底部加载下一页
  onEndReached = async (event) => {
    
    if (!this.state.hasMore) {
      return;
    }
    this.setState({ isLoading: true });
    this.getCommentListFn({ ...this.getListConfig, requestPage: ++pageIndex }, false);
  };


  render() {
    const { op } = this.state;//是否显示选择评论
    const { commentList } = this.state;
    // let index = 0
    const row = commentList.length > 0 ? (rowData) => {
      // const obj = reviewShareVoList[index++];
      return (
        <CommentItem obj={rowData} op={op}>
          {op ? <SelectComment CommentId={rowData.CommentId} /> : null}
        </CommentItem>
      );
    } : () => {
      return (
        <div className="msg-area">
          <div className="msg-area-title">留言区</div>
          <div className="msg-area-content">
            还没有留言，快来抢个沙发吧～
          </div>
        </div>
      );
    };
    return (
      <div className={commentList.length ? 'comment__container comment-anchor ' : 'comment__container comment-anchor no-comment'}>
        <h2 className='comment__container--titile'>精选留言</h2>
        <ListView
          ref={el => this.lv = el}
          dataSource={this.state.dataSource}
          renderFooter={() => (
            this.state.isLoading ? <div style={{ textAlign: 'center' }}>加载中</div> :
              (
                commentList.length ?
                  <div style={{ textAlign: 'center', paddingBottom: '45px' }}>没有更多了</div> :
                  <div className="msg-area">还没有留言，快来抢个沙发吧～</div>
              )
          )}
          renderRow={row}
          useBodyScroll={true}
          pullToRefresh={
            op ? <PullToRefresh refreshing={this.state.refreshing} onRefresh={this.onRefresh} /> : null
          }
          onEndReached={this.onEndReached}
        />
      </div>
    );
  }
}

export default Comment;