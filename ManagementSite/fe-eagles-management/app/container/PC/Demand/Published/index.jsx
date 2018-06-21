import React from 'react';
import { hashHistory } from 'react-router';
import { Table, Row, Col, Button, message, Modal } from 'antd';
import ContentManage from '../index.jsx';
import EditDemand from "../../../../components/PC/EditDemand";
import DeleteDemand from "../../../../components/PC/DeleteDemand";
import { connect } from "react-redux";
import { searchDemand, deleteDemand, getDemandInfo, closeDemand, releaseDemand } from "../../../../services/demandService";
import util from "../../../../utils/util";
import { saveProjectInfo } from "../../../../actions/PC/projectAction";
import "./published.less";

const confirm = Modal.confirm;

@connect(
	null,
	{ saveProjectInfo }
)
export default class Published extends React.Component {

	constructor(props) {
		super(props);
		this.state = {
			selectedRowKeys: [],// 需求id数组
			demandList: [],// 需求列表数组
			keyword: '',// 关键字
			current: 1,// 当前页
			pageConfig: {},// 当前页配置
			hasChoosed: [],//选择的需求详情
			isRelated: 0,//该是否关联项目 0 没有  1 有
		}
		this.columns = [{
			title: '名称',
			dataIndex: 'name',
		}, {
			title: '发布时间',
			dataIndex: 'publishTime',
			render: (text, record) => {
				return (
					<span>{util.timeStampConvent(text)}</span>
				);
			},
		}, {
			title: '关联项目',
			dataIndex: 'projectName',
			// render: (text, record) => {
			// 	return (
			// 		<span>{record.projectName ? record.projectName : '未关联项目'}</span>
			// 	);
			// },
		}, {
			title: '操作',
			render: (text, record) => {
				return (
					<span>{record.projectName ? <a href="javascript:;" onClick={() => this.handleRelease(record.id)}>解除关联</a> : null}</span>
				);
			},
		}];
		this.getListConfig = {
			requestPage: 1,
			pageSize: 6,
			keyword: '',
			status: 1,//已发布
		}
	}
	componentWillMount() { //首次获取第一页数据
		this.getCurrentList(this.getListConfig)
	}
	// 选择需求时触发的改变
	onSelectChange = (selectedRowKeys, current) => {
		let { isRelated } = this.state
		console.log('current - ', current)
		if (current.length === 0) {
			this.setState({ isRelated: 0 })
		}
		if (current.length === 1) {
			let { isRelated } = current[0]
			this.setState({ isRelated })
		}
		this.setState({ selectedRowKeys, hasChoosed: current })
	}
	// 加载当前页
	getCurrentList = async (params) => {
		try {
			let { keyword = '', requestPage } = params
			let res = await searchDemand(params)
			let { list, totalSize } = res
			list.forEach(v => v.key = v.id)
			this.setState({ demandList: list, current: requestPage })
			this.updatePageConfig(totalSize)
		} catch (e) {
			message.error('获取失败')
			throw new Error(e)
		}
	}
	// 更新分页配置
	updatePageConfig(totalSize) {
		let pageConfig = {
			total: totalSize,
			pageSize: this.getListConfig.pageSize,
			current: this.state.current,
			onChange: async (page, pagesize) => {
				this.getCurrentList({
					...this.getListConfig,
					requestPage: page,
					keyword: this.state.keyword
				})
			}
		}
		this.setState({ pageConfig })
	}
	// 解除关联关系
	handleRelease = async (requirementId) => {
		confirm({
			title: '是否确认解除?',
			okText: '确认',
			cancelText: '取消',
			onOk: async () => {
				try {
					let { code } = await releaseDemand({ requirementId })
					console.log('code - ',code)
					if (code === 0) {
						message.success('解除关联成功')
					}
					await this.getCurrentList({
						...this.getListConfig,
						requestPage: this.state.current,
						keyword: this.state.keyword
					})
				} catch (e) {
					console.log(e)
					message.error('解除关联失败')
					throw new Error(e)
				}
			}
		});
	}
	// 编辑分享，根据id查询详情
	handleEdit = async (requireId) => {
		try {
			let { selectedRowKeys, hasChoosed } = this.state;
			if (selectedRowKeys.length > 1) {
				return message.error('不能同时编辑多个项目')
			}
			if (selectedRowKeys.length === 0) {
				return message.error('请选择需要编辑的项目')
			}
			let { isRelated } = hasChoosed[0];
			if (isRelated === 1) {
				return message.error('该需求已关联项目，不能进行编辑');
			}
			hashHistory.replace(`/demand/${selectedRowKeys[0]}`)
		} catch (e) {
			message.error('获取详情失败')
			throw new Error(e)
		}
	}
	// 下拉提示列表 关键字匹配
	fetchList = async (value) => {
		try {
			let keyword = encodeURI(value)
			let params = { ...this.getListConfig, keyword }
			let res = await searchDemand(params)
			let { list } = res
			return list.map(v => ({
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
			let params = { ...this.getListConfig, keyword }
			let res = await searchDemand(params)
			const { list, totalSize } = res
			list.forEach(v => v.key = v.id)
			this.setState({
				keyword,
				demandList: list,
				current: 1
			})
			this.updatePageConfig(totalSize)
		} catch (e) {
			throw new Error(e)
		}
	}
	// 关闭需求
	handleClose = async () => {
		confirm({
			title: '是否确认关闭?',
			okText: '确认',
			cancelText: '取消',
			onOk: async () => {
				try {
					let { code } = await closeDemand({ requireIdList: this.state.selectedRowKeys })
					if (code === 0) {
						message.success('关闭成功')
					}
					await this.getCurrentList({
						...this.getListConfig,
						requestPage: this.state.current,
						keyword: this.state.keyword
					})
					this.setState({ selectedRowKeys: [] })
				} catch (e) {
					message.error('关闭失败')
					throw new Error(e)
				}
			}
		});
	}
	// 创建项目  或者  进入项目
	/**
	 * 1.没有选择需求时，进入创建项目页面
	 * 2.选择一个需求时
	 * 	a.已关联项目，进入项目详情页面
	 * 	b.未关联项目，进入创建项目页面，将当前需求映射到相应数据
	 * 3.选择多个需求时，给与报错提示
	 */
	createProject = () => {
		let { hasChoosed } = this.state
		console.log('hasChoosed - ', hasChoosed)
		if (hasChoosed.length === 0) {
			hashHistory.replace('/project/create')
		}
		if (hasChoosed.length === 1) {
			let { isRelated, projectId, id, name } = hasChoosed[0]
			if (isRelated === 0) {//未关联项目
				this.props.saveProjectInfo({ requirementId: id, requirementName: name })
				hashHistory.replace('/project/create')
			} else {//已关联项目
				hashHistory.replace(`/project/detail/${projectId}`)
			}
		}
		if (hasChoosed.length > 1) {
			message.error('不能选择多个需求')
		}
	}

	render() {
		const { selectedRowKeys, pageConfig, demandList, isRelated } = this.state
		const rowSelection = {
			selectedRowKeys,
			onChange: this.onSelectChange,
		};
		return (
			<ContentManage fetchList={this.fetchList} search={this.searchList}>
				<Table dataSource={demandList} columns={this.columns} rowSelection={rowSelection} pagination={pageConfig} locale={{ emptyText: '暂无数据' }} />
				<Row type='flex' justify='center' className='edit' gutter={24} className={demandList.length === 0 ? 'init' : ''}>
					<Col>
						<Button type='primary' className='btn btn--primary' onClick={this.createProject}>
							{isRelated === 0 ? '新建项目' : '进入项目'}
						</Button>
					</Col>
					<Col>
						<EditDemand edit={this.handleEdit} false />
					</Col>
					<Col>
						<Button onClick={this.handleClose} className='btn'>关闭</Button>
					</Col>
				</Row>
			</ContentManage >
		);
	}
}
