import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import Utils from '../../../utils/util';
import './style.less';

class ProjectItem extends Component {
  constructor(props, context) {
    super(props, context);
  }

  //跳到详情页
  jumpToDetail(id) {
    hashHistory.push(`/projectdetail/${id}`)
  }

  // //跳到编辑页
  edit(id, e) {
    e.stopPropagation();
    console.log(id);
    this.props.edit(id);
  }

  // //删除项目
  del(project, e) {
    e.stopPropagation()
    this.props.del(project);
  }

  render() {
    const { project } = this.props;
    return (
      <div className="project-item" onClick={this.jumpToDetail.bind(this, project.projectId)}>
        <div className="project-name">{project.projectName}</div>
        <div className="project-type">
          <div className="project-time">{Utils.formatTime(project.createTime, 'yyyy-MM-dd')}</div>
          <div>
            <span className="project-btn del" onClick={this.del.bind(this, project)}>删除</span>
            <span className="project-btn edit" onClick={this.edit.bind(this, project.projectId)}>编辑</span>
          </div>
        </div>
      </div>
    );
  }
}

export default ProjectItem;
