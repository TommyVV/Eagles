import React, { Component } from 'react';
import IoreItem from "../IoreItem";
import { Flex, List, ListView } from 'antd-mobile';
import { hashHistory } from 'react-router';
import { getAgencyShareList } from '../../../../services/agencyService';
import NoMessageTip from '../../NoMessageTip';
import "./style.less";

const Item = List.Item;



class IoreShare extends Component {
  constructor(props) {
    super(props);
    const dataSource = new ListView.DataSource({
      rowHasChanged: (row1, row2) => row1 !== row2,
    });
    this.state = {
      pageSize: 10,
      requestPage: 1,
      refreshing: true,
      isLoading: true,
      // listBoxHeight: document.documentElement.clientHeight,
      dataSource,
      shareInfo: {
        shareList: [],
        totalSize: 0
      }
    };
  }



  componentDidUpdate() {
    document.body.style.overflow = 'auto';
  }


  componentDidMount() {
    document.body.style.overflow = 'auto';
    // const hei = this.state.height - ReactDOM.findDOMNode(this.lv).offsetTop;
    // console.log(ReactDOM.findDOMNode(this.lv).offsetTop);
    // console.log(document.documentElement.clientHeight);

    // setTimeout(() => {
    //   this.rData = genData();
    //   console.log(genData())
    //   this.setState({
    //     dataSource: this.state.dataSource.cloneWithRows(genData()),
    //     height: hei,
    //     refreshing: false,
    //     isLoading: false,
    //   });
    // }, 300);
    const { agency } = this.props;
    this.getAgencyShare(agency.orgId)
  }
  componentDidUpdate() {
    document.body.style.overflow = 'auto';
  }
  // 取咨询机构的分享列表
  getAgencyShare = async (id) => {
    const { pageSize, requestPage } = this.state;
    const res = await getAgencyShareList({ orgId: id, pageSize, requestPage });
    const list = this.state.shareInfo.shareList.concat(res.data.shareInfoList);
    this.setState({
      shareInfo: {
        totalSize: res.data.totalSize,
        shareList: list
      },
      dataSource: this.state.dataSource.cloneWithRows(this.genData(list.length)),
      refreshing: false,
      isLoading: false,
    });
  }
  genData(len) {
    const dataArr = [];
    for (let i = 0; i < len; i++) {
      dataArr.push(`row - ${i}`);
    }
    return dataArr;
  }

  onEndReached = (event) => {
    console.log('test in');
    const { pageSize, requestPage, shareInfo } = this.state;
    if (requestPage * pageSize >= shareInfo.totalSize) {
      return;
    }
    this.setState({ isLoading: true, requestPage: requestPage + 1 });
    const { agency } = this.props;
    this.getAgencyShare(agency.orgId);
  }


  jumpToDetail = (id) => {
    hashHistory.push(`/sharedetail/${id}`);
  }

  render() {
    const { shareInfo, pageSize, isLoading } = this.state;

    let index = 0
    const row = (rowData, sectionID, rowID) => {
      const share = shareInfo.shareList[index++];
      return (
        <IoreItem share={share} jump={this.jumpToDetail} />
      );
    };
    return (
      <div className='iore-share'>
        <ListView
          key={'0'}
          ref={el => this.lv = el}
          dataSource={this.state.dataSource}
          renderFooter={() => (
            <NoMessageTip
              pageSize={pageSize}
              listLen={shareInfo.shareList.length}
              title="暂时没有知识分享"
              // description="还没有人发布"
              loading={isLoading}
            />
          )}
          renderRow={row}
          useBodyScroll={true}
          onEndReached={this.onEndReached}
          pageSize={this.state.shareInfo.shareList.length}
        />
      </div>
    );
  }
}
export default IoreShare;