import React, { Component } from 'react';
import { Table, message } from 'antd';
import ContentManage from '../index.jsx';
import EditDemand from "../../../../components/PC/EditDemand";
import DeleteDemand from "../../../../components/PC/DeleteDemand";
import { getCloseDemandList } from "../../../../services/demandService";
import util from "../../../../utils/util";

class Published extends Component {
	constructor(props) {
		super(props);
		this.state = {
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
			render: text => <span>{util.timeStampConvent(text, 'yyyy-MM-dd hh:mm:ss')}</span>
		}];
		this.getListConfig = {
			requestPage: 1,
			pageSize: 6 ,
			keyword: '',
		}
	}
	componentWillMount() { //首次获取第一页数据
		this.getCurrentList(this.getListConfig)
	}
	// 加载当前页
	getCurrentList = async (params) => {
		try {
			let { keyword = '', requestPage } = params
			let res = await getCloseDemandList(params)
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
	// 下拉提示列表 关键字匹配
	fetchList = async (value) => {
		try {
			let keyword = encodeURI(value)
			let params = { ...this.getListConfig, keyword }
			let res = await getCloseDemandList(params)
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
			let res = await getCloseDemandList(params)
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
	render() {
		const { pageConfig, demandList } = this.state
		return (
			<ContentManage fetchList={this.fetchList} search={this.searchList}>
				<Table dataSource={demandList} columns={this.columns} pagination={pageConfig} locale={{ emptyText: '暂无数据' }} />
			</ContentManage>
		);
	}
}

export default Published;