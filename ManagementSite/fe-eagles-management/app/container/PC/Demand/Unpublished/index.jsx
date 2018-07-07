import React from 'react';
import { hashHistory } from 'react-router';
import { Table, Row, Col, message, Modal } from 'antd';
import ContentManage from '../index.jsx';
import EditDemand from "../../../../components/PC/EditDemand";
import DeleteDemand from "../../../../components/PC/DeleteDemand";
import util from "../../../../utils/util";
import { searchDemand, deleteDemand, getDemandInfo } from "../../../../services/demandService";

const confirm = Modal.confirm;

export default class Published extends React.Component {

	constructor(props) {
		super(props);
		this.state = {
			selectedRowKeys: [],// 需求id数组
			demandList: [],// 需求列表数组
			keyword: '',// 关键字
			current: 1,// 当前页
			pageConfig: {}// 当前页配置
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
		}];
		this.getListConfig = {
			requestPage: 1,
			pageSize: 6,
			keyword: '',
			status: 0,//未发布
		}
	}
	componentWillMount() { //首次获取第一页数据
		this.getCurrentList(this.getListConfig)
	}
	// 选择需求时触发的改变
	onSelectChange = (selectedRowKeys) => {
		this.setState({ selectedRowKeys })
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
	// 删除需求
	handleDelete = (requireIdList) => {
		confirm({
			title: '是否确认删除?',
			okText: '确认',
			cancelText: '取消',
			onOk: async () => {
				try {
					await deleteDemand({ requireIdList })
					await this.getCurrentList({
						...this.getListConfig,
						requestPage: this.state.current,
						keyword: this.state.keyword
					})
					this.setState({ selectedRowKeys: [] })
				} catch (e) {
					message.error('删除失败')
					throw new Error(e)
				}
			}
		});
	}
	// 编辑分享，根据id查询详情
	handleEdit = async (requireId) => {
		try {
			let { selectedRowKeys } = this.state;
			if (selectedRowKeys.length > 1) {
				return message.error('不能同时编辑多个项目')
			}
			if (selectedRowKeys.length === 0) {
				return message.error('请选择需要编辑的项目')
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
			message.error('获取失败')
			throw new Error(e)
		}
	}
	render() {
		const { selectedRowKeys, pageConfig, demandList } = this.state
		const rowSelection = {
			selectedRowKeys,
			onChange: this.onSelectChange,
		};
		return (
			<ContentManage fetchList={this.fetchList} search={this.searchList}>
				<Table dataSource={demandList} columns={this.columns} rowSelection={rowSelection} pagination={pageConfig} locale={{ emptyText: '暂无数据' }}  />
				<Row type='flex' justify='center' gutter={24} className={demandList.length === 0 ? 'init' : ''}>
					<Col>
						<EditDemand  edit={this.handleEdit} />
					</Col>
					<Col>
						<DeleteDemand selectedRowKeys={selectedRowKeys} delete={this.handleDelete} />
					</Col>
				</Row>
			</ContentManage>
		);
	}
}
