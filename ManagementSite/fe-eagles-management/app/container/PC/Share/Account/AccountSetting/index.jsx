import React from 'react';
import { Radio, Form, Input, Button } from 'antd'
const FormItem = Form.Item;
class Base extends React.Component {

	constructor(props) {
		super(props);
		this.state = {
			value: '个人'
		}
	}
	onChange = (e) => {
		this.setState({ value: e.target.value });
	}
	render() {
		let { value } = this.state
		const { getFieldDecorator } = this.props.form;
		const formItemLayout = {
			labelCol: {
				xs: { span: 24 },
				sm: { span: 3 },
			},
			wrapperCol: {
				xs: { span: 24 },
				sm: { span: 3 },
			},
		};
		const tailFormItemLayout = {
			wrapperCol: {
				xs: {
					span: 24,
					offset: 0,
				},
				sm: {
					span: 3,
					offset: 3,
				},
			},
		};
		return (
			<div>
				<h1>账户设置</h1>
				<Radio.Group value={value} onChange={this.onChange} style={{ marginBottom: 16 }}>
					<Radio.Button value="个人">个人</Radio.Button>
					<Radio.Button value="机构">机构</Radio.Button>
				</Radio.Group>
				<Form onSubmit={this.handleSubmit}>
					<FormItem
						{...formItemLayout}
						label="支付宝账号"
					>
						{getFieldDecorator('zf_account', {
							rules: [{
								type: 'email', message: 'The input is not valid E-mail!',
							}, {
								required: true, message: 'Please input your E-mail!',
							}],
						})(
							<Input />
						)}
					</FormItem>
					<FormItem
						{...formItemLayout}
						label="微信账号"
					>
						{getFieldDecorator('wx_account', {
							rules: [{
								type: 'email', message: 'The input is not valid E-mail!',
							}, {
								required: true, message: 'Please input your E-mail!',
							}],
						})(
							<Input />
						)}
					</FormItem>
					<FormItem {...tailFormItemLayout}>
						<Button htmlType="submit">保存</Button>
					</FormItem>
				</Form>
			</div>
		);
	}
}
const AccountSetting = Form.create()(Base)
export default AccountSetting