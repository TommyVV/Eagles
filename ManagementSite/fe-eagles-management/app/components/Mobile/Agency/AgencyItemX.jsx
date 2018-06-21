import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import './style.less';
import { deleteConcern, addConcern } from '../../../services/myService';
import { addAgencyHistory } from '../../../services/agencyService';


class AgencyItemX extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      agency: this.props.agency
    }
  }
  concernFn(e, id, isConcern) {
    e.stopPropagation();
    this.concernAgencyFn(id, isConcern);
  }
  // 关注咨询机构列表
  concernAgencyFn = async (id, isConcern) => {
    const param = { type: 1, typeId: id }; // type 1代表机构
    const { agency } = this.state;
    if (isConcern) {
      const res = await deleteConcern(param);
      if (res.code == 0) {
        agency.isFocus = 0; // 取消关注成功
        this.setState({ agency });
      }
    } else {
      const res = await addConcern(param);
      if (res.code == 0) {
        agency.isFocus = 1; // 关注成功
        this.setState({ agency });
      }
    }
  }
  // 跳转到详情
  jumpToDetail = async (id) => {
    try {
      await addAgencyHistory({ orgId: id });
      hashHistory.push(`agencydetail/${id}`);
    } catch (e) {
      console.log(e);
    }
  }
  render() {
    const { agency } = this.state;
    return (
      <div className="agency-item-x" onClick={() => this.jumpToDetail(agency.id)} >
        {/* <div className="clear-div">
          <i className="qtui-icon-clear"></i>
        </div> */}
        <img className="agency-img" src={agency.avatar} />
        <div className="agency-name">{agency.companyName}</div>
        <div className="agency-info">{agency.label}</div>
        {
          agency.isFocus == '0'
            ?
            <div className="concern-agency add-concern" onClick={(e) => this.concernFn(e, agency.id, false)}>关注</div>
            :
            <div className="concern-agency del-concern" onClick={(e) => this.concernFn(e, agency.id, true)}>已关注</div>
        }
      </div>
    );
  }
}

export default AgencyItemX;