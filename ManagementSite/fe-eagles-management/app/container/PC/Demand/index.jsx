import React from 'react';
import { Row, Col } from 'antd';
import { hashHistory } from 'react-router';
import demandMap from "./demandMap";
import Nav from '../Nav';
import FzSearch from "../../../components/PC/FzSearch";

export default class ContentManage extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			key: '1'
		}
	}
	componentWillMount() {
		let { hash } = window.location;
		let index = hash.indexOf('?');
		hash = hash.slice(0, index);
		let arr = hash.split('/');
		let current = arr[2];
		let key = demandMap.find(item => item.pathname === current).key;
		this.setState({ key })
	}
	render() {
		const { key } = this.state;
		const { fetchList, search } = this.props;
		return (
			<Nav>
				<FzSearch
					fetchList={fetchList}
					search={search}
				/>
				<Row>
					<Col className='nav-tab'>
						<ul>
							<li>
								<a onClick={() => hashHistory.replace('/demandmanage/published')} className={key === '1' ? 'active' : null}>
									已发布
								</a>
							</li>
							<li>
								<a onClick={() => hashHistory.replace('/demandmanage/unpublished')} className={key === '2' ? 'active' : null}>
									未发布
								</a>
							</li>
							<li>
								<a onClick={() => hashHistory.replace('/demandmanage/audit')} className={key === '3' ? 'active' : null}>
									审核中
								</a>
							</li>
							<li>
								<a onClick={() => hashHistory.replace('/demandmanage/unaudit')} className={key === '4' ? 'active' : null}>
									未通过审核
								</a>
							</li>
							<li>
								<a onClick={() => hashHistory.replace('/demandmanage/closed')} className={key === '5' ? 'active' : null}>
									已关闭
								</a>
							</li>
						</ul>
					</Col>
				</Row>
				{this.props.children}
			</Nav>
		);
	}
}