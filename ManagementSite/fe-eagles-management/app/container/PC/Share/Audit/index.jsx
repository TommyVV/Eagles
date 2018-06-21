import React from 'react';
import { Table, message } from 'antd';
import ContentManage from '../index.jsx';
import sendRequest from "../../../../utils/requestUtil";
import { searchShare, deleteShare } from "../../../../services/shareService";
import util from "../../../../utils/util";

export default class Published extends React.Component {

	constructor(props) {
		super(props);
		this.state = {
			selectedRowKeys: [],// 分享id数组
			shareList: [],// 分享列表数组
			keyword: '',// 关键字
			current: 1,// 当前页
			pageConfig: {}// 当前页配置
		}
		this.columns = [{
			title: '名称',
			dataIndex: 'title',
		}, {
			title: '售价(元)',
			dataIndex: 'price',
			width: '182px',
			render: text => <span>{text === 0 ? '免费' : text}</span>
		}, {
			title: '发布身份',
				dataIndex: 'identityName',
			width: '182px'
		}, {
			title: '发布时间',
			dataIndex: 'updateTime',
			width: '182px',
			render: text => <span>{util.timeStampConvent(text, 'yyyy-MM-dd hh:mm:ss')}</span>
		}];
		this.getListConfig = {
			requestPage: 1,
			pageSize: 6,
			status: 2,//审核中
			keyword: ''
		}
	}


	componentWillMount() {
		this.getCurrentList(this.getListConfig)
	}
	// 加载当前页
	getCurrentList = async (params) => {
		try {
			let { keyword, requestPage } = params
			let config = { ...this.getListConfig, requestPage, keyword }
			let { totalSize, shareList } = await searchShare(config)
			console.log('shareList - ', shareList)
			shareList.forEach(v => v.key = v.id)
			this.setState({ shareList, current: requestPage })
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
			let { shareList } = await searchShare(params)
			return shareList.map(v => ({
				text: v.title,
				value: v.title,
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
			let { shareList, totalSize } = await searchShare(params)
			shareList.forEach(v => v.key = v.id)
			this.setState({
				keyword,
				shareList,
				current: 1
			})
			this.updatePageConfig(totalSize)
		} catch (e) {
			message.error('获取失败')
			throw new Error(e)
		}
	}
	render() {
		const { selectedRowKeys, pageConfig, shareList } = this.state
		return (
			<ContentManage fetchList={this.fetchList} search={this.searchList}>
				<Table dataSource={shareList} columns={this.columns} pagination={pageConfig} locale={{ emptyText: '暂无数据' }} />
			</ContentManage>
		);
	}
}
