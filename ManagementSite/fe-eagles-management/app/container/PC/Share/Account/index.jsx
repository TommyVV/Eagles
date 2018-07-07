import React from 'react';
import { Table } from 'antd'
import ContentManage from '../index.jsx';
import AccountSetting from './AccountSetting'
export default class Account extends React.Component {

	constructor(props) {
		super(props);
		this.state = {
			selectedRowKeys: [],
		}
		this.columns = [{
			title: '名称',
			dataIndex: 'name',
			key: 'name',
		}, {
			title: '售价',
			dataIndex: 'price',
			key: 'price',
		}, {
			title: '累计',
			dataIndex: 'total',
			key: 'total',
		}, {
			title: '发布身份',
			dataIndex: 'create',
			key: 'create',
		}, {
			title: '发布时间',
			dataIndex: 'time',
			key: 'time',
		}, {
			title: '明细',
			dataIndex: 'reason',
			key: 'reason',
			render: (text, record) => {
				return (
					<a href="javascipt:;">明细</a>
				)
			},
		}];
	}
	onSelectChange = (selectedRowKeys) => {
		this.setState({ selectedRowKeys })
	}

	handleSubmit = () => {

	}
	render() {
		let { selectedRowKeys } = this.state
		const rowSelection = {
			selectedRowKeys,
			onChange: this.onSelectChange,
		};
		const dataSource = [{
			key: '1',
			name: '胡彦斌',
			price: 32,
			total: 520,
			create: '西湖区湖底公园1号',
			time: new Date().toString(),
		}, {
			key: '2',
			name: '胡彦祖',
			price: 42,
			total: 520,
			create: '西湖区湖底公园1号',
			time: new Date().toString(),
		}];
		return (
			<ContentManage>
				<h1>订单统计</h1>
				<h3>累计收入：1000元</h3>
				<Table dataSource={dataSource} columns={this.columns} rowSelection={rowSelection} />
				<AccountSetting />
			</ContentManage>
		);
	}
}
