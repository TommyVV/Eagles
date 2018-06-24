import React, { Component } from 'react';
import { Button, Table, Row, Col, message, Avatar, Modal, Icon } from 'antd';
import Nav from '../../Nav';
import { connect } from "react-redux";
import FzSearch from "../../../../components/PC/FzSearch";
import { getProjectInfoById, getFileList, deleteProjectFile } from "../../../../services/projectService";
import { downloadFile, downloadBatchFile } from "../../../../services/fileService";
import { Toast } from 'react-qtui';
import FileUpload from "../FileUpload";
import util from "../../../../utils/util";
import "./detail.less";
import { serverConfig } from "../../../../constants/ServerConfigure";

const confirm = Modal.confirm;

@connect(
  state => {
    return {
      user: state.userReducer,
    }
  },
  null
)
class Detail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [],//文件选择id
      basicData: [],//基础数据
      membersData: [],//项目成员
      projectFileList: [],//文件列表
      current: 1,// 当前页
      pageConfig: {},// 当前页配置
      projectId: '',//项目ID
      selectedRows: [],//选择
      showLoding: false,
    };
    this.columns = [{
      title: '名称',
      dataIndex: 'fileName',
    }, {
      title: '上传者',
      dataIndex: 'uploadUserName',
    }, {
      title: '上传时间',
      dataIndex: 'createTime',
      render: text => <span>{util.timeStampConvent(text, 'yyyy-MM-dd hh:mm:ss')}</span>
    }];
    this.getListConfig = {
      requestPage: 1,
      pageSize: 5,
      keyword: ''
    };
  }

  componentWillMount() {
    let { projectId } = this.props.params;
    if (projectId) {
      this.getInfo(projectId);
      this.getFileListPage(this.getListConfig);
      this.setState({ projectId });
    }
  }

  // 根据id查询详情
  getInfo = async (projectId) => {
    try {
      let author = {
        name: this.props.user.userName,
        user_id: this.props.user.userId,
        avatar: this.props.user.avatar
      };
      let { basicData, membersData } = await getProjectInfoById({ projectId });
      console.log({ basicData, membersData });
      this.setState({ basicData, membersData: [author, ...membersData] });
    } catch (e) {
      message.error('获取详情失败');
      throw new Error(e);
    }
  }

  // 查询文件列表
  getFileListPage = async (params) => {
    try {
      let { keyword, requestPage } = params;
      let config = { ...this.getListConfig, requestPage, keyword, projectId: this.props.params.projectId };
      let { totalSize, projectFileList } = await getFileList(config);
      console.log('projectFileList - ', projectFileList);
      projectFileList.forEach(v => v.key = v.fileId);
      this.setState({ projectFileList, current: requestPage });
      this.updatePageConfig(totalSize);
    } catch (e) {
      message.error('获取失败');
      throw new Error(e);
    }
  }

  // 更新分页配置
  updatePageConfig(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.pageSize,
      current: this.state.current,
      onChange: async (page, pagesize) => {
        this.getFileListPage({
          ...this.getListConfig,
          requestPage: page,
          keyword: this.state.keyword
        })
      }
    };
    this.setState({ pageConfig });
  }

  // 选择文件分页
  onSelectChange = (selectedRowKeys, selectedRows) => {
    this.setState({ selectedRowKeys, selectedRows });
  }

  // 下拉提示列表 关键字匹配
  fetchList = async (value) => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword, projectId: this.props.params.projectId };
      let { projectFileList } = await getFileList(params);
      return projectFileList.map(v => ({
        text: v.fileName,
        value: v.fileName,
      }));
    } catch (e) {
      throw new Error(e);
    }
  }

  // 回车搜索列表  关键字匹配
  searchList = async (value) => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword, projectId: this.props.params.projectId };
      let { projectFileList, totalSize } = await getFileList(params);
      projectFileList.forEach(v => v.key = v.fileId);
      this.setState({
        keyword,
        projectFileList,
        current: 1
      });
      this.updatePageConfig(totalSize);
    } catch (e) {
      message.error('获取失败');
      throw new Error(e);
    }
  }
  // 上传附件成功或者删除
  handleFile = () => {
    let _this = this;
    let { current, projectId, keyword } = this.state;
    return {
      move() {
        let config = { ..._this.getListConfig, requestPage: current, keyword, projectId };
        _this.getFileListPage(config);
      },
      done() {
        let config = { ..._this.getListConfig, projectId };
        _this.setState({ showLoding: false });
        _this.getFileListPage(config);
      },
      start() {
        _this.setState({ showLoding: true });
      }
    }
  }
  // 删除文件
  handleDelete = async () => {
    let { selectedRowKeys, projectId } = this.state;
    if (selectedRowKeys.length === 0) {
      return message.error('请选择要删除的文件');
    }
    confirm({
      title: '是否确认删除文件?',
      okText: '确认',
      cancelText: '取消',
      onOk: async () => {
        try {
          let { code } = await deleteProjectFile({ fileIdList: selectedRowKeys, projectId });
          if (code === 0) {
            message.success('删除文件成功');
            this.handleFile().move();
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error('删除文件失败');
          }
        } catch (e) {
          message.error('删除文件失败')
          throw new Error(e)
        }
      }
    });
  }
  //下载文件
  handleDownload = async () => {
    let { selectedRowKeys, selectedRows } = this.state;
    if (selectedRowKeys.length === 0) {
      return message.error('请选择要下载的文件');
    }
    try {
      const { token } = JSON.parse(localStorage.info);
      let url = serverConfig.API_SERVER + serverConfig.FILE.BATCH + selectedRowKeys.join(',') + `&token=${token}`;
      if (selectedRowKeys.length === 1) {//只有一个文件
        url = serverConfig.API_SERVER + "/file/only/download?fileId=" + selectedRowKeys[0] +  "&token=" + token
      }
      // '/file/batch/download?fileIdList=90974c17-943c-43d6-b8c1-3b41f445e36d,fc6050f8-29b8-4aac-a8ad-c745f78d0e8a',
      // let url = serverConfig.API_SERVER + serverConfig.FILE.BATCH + selectedRowKeys.join(',') + `&token=${token}`;
      console.log(url)
      // let res = await downloadBatchFile(url);
      let link = document.createElement('a');
      link.className = 'batch_download_link';
      link.href = url;
      document.body.appendChild(link);
      let aLinkDom = document.querySelector('.batch_download_link');
      aLinkDom.click();
      document.body.removeChild(aLinkDom);
    } catch (e) {
      throw new Error(e);
    }
  }

  render() {
    let { basicData, membersData, selectedRowKeys, projectFileList, pageConfig, projectId, showLoding } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange,
    };
    return (
      <Nav>
        <FzSearch
          fetchList={this.fetchList}
          search={this.searchList}
        />
        <div className='project__detail'>
          <h2 className='project__detail--title'>{basicData.projectName}</h2>
          <div className='project__detail--demand'>
            <h3>项目需求</h3>
            <p>{basicData.requirementName || '暂无关联需求'}</p>
          </div>
          <div className='project__detail--files'>
            <h3>项目文件</h3>
            <Table dataSource={projectFileList} columns={this.columns} rowSelection={rowSelection} pagination={pageConfig} locale={{ emptyText: '暂无数据' }} />
          </div>
          <div className={projectFileList.length === 0 ? 'init project__detail--member' : 'project__detail--member'}>
            <h3 className='pro_member'>项目成员</h3>
            <Row>
              {membersData.map(member => (
                <Col key={member.user_id} span={2} className='member-box'>
                  <Avatar src={member.avatar} />
                  <p className='member-name'>{member.name}</p>
                </Col>
              ))}
            </Row>
          </div>
          <Row type='flex' justify='center' className='edit' gutter={24}>
            <Col className='pro-upload'>
              <FileUpload handleFile={this.handleFile} projectId={projectId} />
            </Col>
            <Col>
              <Button onClick={this.handleDownload} className='btn'>下载文件</Button>
            </Col>
            <Col>
              <Button onClick={this.handleDelete} className='btn'>删除文件</Button>
            </Col>
          </Row>
          {
            showLoding ?
              <div className="upload_loading_box">
                <div className='loding_container'>
                  <Toast icon="loading" show={!this.state.initDone}>
                    正在上传
                  </Toast>
                </div>
              </div>
              : null
          }
        </div>
      </Nav>
    );
  }
}

export default Detail;