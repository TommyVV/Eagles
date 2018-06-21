import React, { Component } from 'react';
import { Form, Input, Row, Col, Button, message, Icon } from 'antd';
import ImageUpload from '../../../../components/PC/ImageUpload';
import FileUpload from '../../../../components/PC/FileUpload';
import { getDemandInfo, saveDemand, reviewDemand } from "../../../../services/demandService";
import { hashHistory } from 'react-router';
import Nav from '../../Nav';
import util from "../../../../utils/util";
import './style.less';

const FormItem = Form.Item;
const { TextArea } = Input;

class Base extends Component {
	constructor(props) {
		super(props);
		this.state = {
			effectiveTime: '',//时间
			hasSubmit: false,
			hasSave: false,
		}
	}
	// 在验证前处理多行文本的回车换行符
	handelTransform = (value) => {
		if (value) {
			const reg = /\s/g
			return value.replace(reg, '')
		}
	}
	// 保存
	save = (e) => {
		e.preventDefault();
		this.props.form.validateFields(async (err, values) => {
			if (!err) {
				if (this.state.hasSave) {
					return false;
				}
				this.setState({ hasSave: true })
				try {
					await saveDemand(values)
					console.log('Received values of form: ', values);
					message.success('保存成功')
					hashHistory.replace('/demandmanage/unpublished')
				} catch (e) {
					this.setState({ hasSave: false })
					message.error('保存失败')
					throw new Error(e)
				}
			} else {
				message.error('请检查信息是否填写完毕')
			}
		});
	}
	// 审核
	review = (e) => {
		e.preventDefault();
		this.props.form.validateFields(async (err, values) => {
			if (!err) {
				if (this.state.hasSubmit) {
					return false;
				}
				this.setState({ hasSubmit: true })
				try {
					await reviewDemand(values)
					console.log('Received values of form: ', values );
					message.success('已提交审核')
					hashHistory.replace('/demandmanage/audit')
				} catch (e) {
					message.error('提交审核失败')
					this.setState({ hasSubmit: false })
					throw new Error(e)
				}
			} else {
				message.error('请检查信息是否填写完毕')
			}
		});
	}
	render() {
		const { getFieldDecorator } = this.props.form;
		const { hasSubmit, hasSave } = this.state;
		const antIcon = <Icon type="loading" spin />;
		const isSubmit = hasSubmit && antIcon;
		const isSave = hasSave && antIcon;
		const formItemLayout = {
			labelCol: {
				xl: { span: 3 },
			},
			wrapperCol: {
				xl: { span: 18 },
			},
		};
		return (
			<Form className='demand_form'>
				<h2>发布需求</h2>
				<FormItem
					{...formItemLayout}
					label=""
					style={{ display: 'none' }}
				>
					{getFieldDecorator('id', {
						rules: [{
						}],
					})(
						<Input />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="项目名称"
				>
					{getFieldDecorator('name', {
						rules: [{
							required: true,
							message: '必填，30字以内!',
							pattern: /^(?!.{31}|\s*$)/g,
						}],
					})(
						<Input placeholder='必填，30字以内' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="发布公司全称"
				>
					{getFieldDecorator('company', {
						rules: [{
							required: true,
							message: '必填，20字以内!',
							pattern: /^(?!.{21}|\s*$)/g,
						}],
					})(
						<Input placeholder='必填，20字以内' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="项目地点"
				>
					{getFieldDecorator('address', {
						rules: [{
							required: true,
							message: '必填，20字以内!',
							pattern: /^(?!.{21}|\s*$)/g,
						}],
					})(
						<Input placeholder='必填，20字以内' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="项目周期"
				>
					{getFieldDecorator('period', {
						rules: [{
							required: true,
							message: '必填，10字以内!',
							pattern: /^(?!.{11}|\s*$)/g,
						}],
					})(
						<Input placeholder='必填，10字以内' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="项目规模"
				>
					{getFieldDecorator('scope', {
						rules: [{
							required: true,
							message: '必填，200字以内!',
							transform: (value) => this.handelTransform(value),
							pattern: /^(?!.{201}|\s*$)/g,
						}],
					})(
						<TextArea placeholder="必填，200字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="质量标准"
				>
					{getFieldDecorator('standard', {
						rules: [{
							pattern: /^(?!.{51}|\s*$)/g,
							message: '必填，50字以内!',
							transform: (value) => this.handelTransform(value),
						}],
					})(
						<TextArea placeholder="选填，50字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="有效时间"
				>
					{getFieldDecorator('effectiveTime', {
						rules: [{
							required: true, 
							pattern: /^(?!.{11}|\s*$)/g,
							message: '必填，10字以内!',
						}],
					})(
						<Input placeholder="必填，10字以内" />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="项目资金来源"
				>
					{getFieldDecorator('sourcesFunds', {
						rules: [{
							pattern: /^(?!.{51}|\s*$)/g,
							message: '选填，50字以内!',
						}],
					})(
						<Input placeholder='选填，50字以内' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="服务方资格要求"
				>
					{getFieldDecorator('serviceEligibility', {
						rules: [{
							pattern: /^(?!.{101}|\s*$)/g,
							message: '选填，100字以内!',
							transform: (value) => this.handelTransform(value),
						}],
					})(
						<TextArea placeholder="选填，100字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="项目描述"
				>
					{getFieldDecorator('description', {
						rules: [{
							pattern: /^(?!.{201}|\s*$)/g,
							message: '选填，200字以内!',
							transform: (value) => this.handelTransform(value),
						}],
					})(
						<TextArea placeholder="选填，200字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="联系人"
				>
					{getFieldDecorator('contacts', {
						rules: [{
							required: true,
							message: '必填，20字以内!',
							pattern: /^(?!.{21}|\s*$)/g,
						}],
					})(
						<Input placeholder="必填，20字以内" />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="移动电话"
				>
					{getFieldDecorator('mobilePhone', {
						rules: [{
							message: '选填，请输入正确的手机号!',
							pattern: /^1[3-9]\d{9}$/,
						}],
					})(
						<Input placeholder="选填，请输入移动电话，优先使用轻推注册手机号" />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="办公电话"
				>
					{getFieldDecorator('officePhone', {
						rules: [{
							message: '选填，20字以内!',
							pattern: /^(?!.{21}|\s*$)/g,
						}],
					})(
						<Input placeholder="选填，20字以内" />
					)}
				</FormItem>
				<FormItem>
					<Row type='flex' justify='center' gutter={24}>
						<Col>
							<Button type='primary' onClick={this.review} className='btn btn--primary'>{isSubmit}提交审核</Button>
						</Col>
						<Col>
							<Button onClick={this.save} className='btn'>{isSave}保存</Button>
						</Col>
					</Row>
				</FormItem>
			</Form>
		);
	}
}
// address	项目地点	string
// company	发布公司全称	string
// contacts	联系人	string
// createTime	创建时间	object
// creatorAvatar	创建者头像	string
// creatorId	创建者id	string
// creatorName	创建者名称	string
// description	项目描述	string
// effectiveTime	有效时间	object
// id	需求id	string
// mobilePhone	移动电话	string
// name	需求项目名称	string
// officePhone	办公电话	string
// period	项目周期	string
// scope	项目规模	string
// serviceEligibility	服务方资格要求	string
// sourcesFunds	资金来源	string
// standard	质量标准	string
// message	状态信息	string	
const FormMap = Form.create({
	mapPropsToFields: (props) => {
		console.log('需求数据回显 - ', props)
		const demand = props.demand;
		return {
			id: Form.createFormField({
				value: demand.id
			}),
			address: Form.createFormField({
				value: demand.address
			}),
			company: Form.createFormField({
				value: demand.company
			}),
			contacts: Form.createFormField({
				value: demand.contacts
			}),
			description: Form.createFormField({
				value: demand.description
			}),
			effectiveTime: Form.createFormField({
				value: demand.effectiveTime
			}),
			mobilePhone: Form.createFormField({
				value: demand.mobilePhone
			}),
			name: Form.createFormField({
				value: demand.name
			}),
			officePhone: Form.createFormField({
				value: demand.officePhone
			}),
			period: Form.createFormField({
				value: demand.period
			}),
			scope: Form.createFormField({
				value: demand.scope
			}),
			serviceEligibility: Form.createFormField({
				value: demand.serviceEligibility
			}),
			sourcesFunds: Form.createFormField({
				value: demand.sourcesFunds
			}),
			standard: Form.createFormField({
				value: demand.standard
			}),
		}
	}
})(Base);

class Demand extends Component {
	constructor(props) {
		super(props);
		this.state = {
			requirementData: {}//需求数据详情
		}
	}

	componentWillMount() {
		let { requireId } = this.props.params
		if (requireId) {
			this.getInfo(requireId)
		}
	}

	getInfo = async (requireId) => {
		try {
			let res = await getDemandInfo({ requireId, handle: 1 })
			console.log('需求详情 - ', res)
			let { requirementData } = res
			this.setState({ requirementData })
		} catch (e) {
			message.error('获取详情失败')
			throw new Error(e)
		}
	}

	render() {
		const { requirementData } = this.state
		return (
			<Nav>
				<FormMap demand={requirementData} />
			</Nav>
		);
	}
}

export default Demand;
