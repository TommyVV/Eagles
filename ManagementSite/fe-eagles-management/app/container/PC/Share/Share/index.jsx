import React, { Component } from 'react';
import { connect } from "react-redux";
import { hashHistory } from 'react-router';
import { Form, Input, Row, Col, Button, TreeSelect, message, Spin, Icon } from 'antd';
import { changeIdentity, clearInfo, saveShareInfo, saveFileUrl, removeFileUrl } from "../../../../actions/PC/shareAction";
import ImageUpload from '../../../../components/PC/ImageUpload';
import FileUpload from '../../../../components/PC/FileUpload';
import sendRequest from "../../../../utils/requestUtil";
import { serverConfig } from "../../../../constants/ServerConfigure";
import { getShareInfo } from "../../../../services/shareService";
import { deleteFile } from "../../../../services/fileService";
import Nav from '../../Nav';
import './share.less';

const FormItem = Form.Item;
const { TextArea } = Input;
const SHARE = serverConfig.SHARE;

@connect(
	state => {
		return {
			user: state.userReducer,
			share: state.shareReducer
		}
	},
	{ changeIdentity, saveShareInfo, saveFileUrl, clearInfo, removeFileUrl }
)
class Base extends Component {
	constructor(props) {
		super(props);
		this.state = {
			orgList: [],
			hasSubmit: false,
			hasSave: false,
			flag: true,
			orgListMap: new Map()
		}
	}
	// 在验证前处理多行文本的回车符
	handelTransform = (value) => {
		if (value) {
			const reg = /\s/g
			return value.replace(reg, '')
		}
	}

	componentWillMount() {
		this.getOrgList();
	}

	// 请求组织结构列表
	getOrgList = async () => {
		try {
			let res = await sendRequest({
				url: SHARE.ORG,
			})
			let { code } = res.data;
			if (code === 0) {
				let { data } = res.data;
				let orgList = [];
				let orgListMap = new Map();
				data.forEach(v => {
					if (v.identityType === 0) {
						orgList.push({ label: '个人', value: v.identityName })
					} else {
						orgList.push({ label: v.identityName, value: v.identityName })
					}
					orgListMap.set(v.identityName, v)
				});
				this.setState({ orgList, orgListMap })
				console.log('请求组织结构列表 - ', data)
			} else {
				message.error('请求组织结构列表失败')
			}
		} catch (e) {
			throw new Error(e)
		}
	}

	// 保存 - 分享
	save = (e) => {
		e.preventDefault();
		this.props.form.validateFields(async (err, values) => {
			if (!err) {
				if (this.state.hasSave) {
					return false;
				}
				this.setState({ hasSave: true })
				let identityName = values.identityName
				// 初始化的时候映射用户名
				if (identityName === '个人') {
					identityName = this.props.user.userName
				}
				let { img, attachment } = this.props.share
				let params = { ...this.props.share, ...values, identityName }
				console.log('params - ', params)
				try {
					let res = await sendRequest({
						method: 'post',
						url: SHARE.SAVE,
						params: params
					})
					let { code } = res.data;
					if (code === 0) {
						this.setState({ flag: false })
						let { deleteList } = this.props.share;
						deleteList.length > 0 && this.handleDeleteFile(deleteList)
						message.success('保存成功');
						hashHistory.replace('/sharemanage/unpublished')
					} else {
						this.setState({ hasSave: false })
						message.error('保存失败');
					}
				} catch (e) {
					throw new Error(e);
				}
			} else {
				message.error('请检查信息是否填写完毕')
			}
		});
	}

	// 审核 - 分享
	review = (e) => {
		e.preventDefault();
		this.props.form.validateFields(async (err, values) => {
			if (!err) {
				if (this.state.hasSubmit) {
					return false;
				}
				this.setState({ hasSubmit: true })
				let identityName = values.identityName
				// 初始化的时候映射用户名
				if (identityName === '个人') {
					identityName = this.props.user.userName
				}
				let { img, attachment } = this.props.share
				let params = { ...this.props.share, ...values, identityName }
				console.log('params - ', params)
				try {
					let res = await sendRequest({
						method: 'post',
						url: SHARE.REVIEW,
						params: params
					})
					let { code } = res.data;
					if (code === 0) {
						this.setState({ flag: false })
						let { deleteList } = this.props.share;
						deleteList.length > 0 && this.handleDeleteFile(deleteList)
						message.success('已提交审核');
						hashHistory.replace('/sharemanage/audit')
					} else {
						message.error('提交审核失败');
						this.setState({ hasSubmit: false })
					}
				} catch (e) {
					throw new Error(e);
				}
			} else {
				message.error('请检查信息是否填写完毕')
			}
		});
	}

	// 发布身份改变
	onChange = (identityName) => {
		let { orgListMap } = this.state;
		let { getFieldsValue } = this.props.form
		let values = getFieldsValue() // 改变发布身份前获取填写的数据
		let item = orgListMap.get(identityName);
		this.props.changeIdentity({ ...values, ...item });
		console.log('发布身份改变 - ', { ...values, ...item })
	}

	// 传递图片前将数据保存
	saveInfo = () => {
		let { getFieldsValue } = this.props.form
		let values = getFieldsValue()
		let { identityName } = values
		identityName === '个人' ? identityName = this.props.user.userName : identityName
		this.props.saveShareInfo({ ...values, identityName })
		// console.log('上传图片记录表单数据 - ', values, this.props.share)
	}

	// 上传附件成功或者删除
	handleFile = (attr) => {
		let _this = this
		this.saveInfo()
		return {
			move(list, map, fileId) {
				let idList = [];
				list.forEach(file => {
					if (file.status) {
						idList.push(map.get(file.uid))
					}
					idList.push(file.fileId)
				})
				let noUndefindArray = idList.filter(v => v)
				let { deleteList } = _this.props.share;
				deleteList.push(fileId);
				_this.props.saveFileUrl({ [attr]: noUndefindArray.join(';'), deleteList, count: list.length });
			},
			done(list, map, fileId) { // list 为当前图片list 、map为uid和fileId的关联关系
				let idList = [];
				let { uploadDeleteList } = _this.props.share;
				list.forEach(file => {
					if (file.status) {//从编辑中获取fileId
						idList.push(map.get(file.uid))
					}
					idList.push(file.fileId)
				})
				let noUndefindArray = idList.filter(v => v)
				uploadDeleteList.push(fileId)
				_this.props.saveFileUrl({
					[attr]: noUndefindArray.join(';'),
					count: noUndefindArray.length,
					uploadDeleteList
				});
			}
		}
	}

	componentWillUnmount() {
		if (this.state.flag) {
			let { uploadDeleteList } = this.props.share;
			uploadDeleteList.length > 0 && this.handleDeleteFile(uploadDeleteList)
		}
		this.props.clearInfo()
	}

	handleDeleteFile = async (fileIdList) => {
		try {
			await deleteFile({ fileIdList })
		} catch (e) {
			throw new Error(e)
		}
	}

	render() {
		const { getFieldDecorator } = this.props.form;
		const { orgList, hasSubmit, hasSave } = this.state;
		const antIcon = <Icon type="loading" spin />;
		const isSubmit = hasSubmit && antIcon;
		const isSave = hasSave && antIcon;
		const formItemLayout = {
			labelCol: {
				xl: { span: 2 },
			},
			wrapperCol: {
				xl: { span: 18 },
			},
		};
		const ImageLayout = {
			wrapperCol: {
				xl: { span: 18, offset: 2 },
			},
		};
		return (
			<Form className='share-form'>
				<FormItem
					{...formItemLayout}
					label="ID"
					style={{ display: 'none' }}
				>
					{getFieldDecorator('shareId')(
						<Input />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="标题"
				>
					{getFieldDecorator('title', {
						rules: [{
							required: true,
							pattern: /^(?!.{51}|\s*$)/g,
							message: '必填，50字以内!',
						}],
					})(
						<Input placeholder='必填，50字以内!' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="售价"
				>
					{getFieldDecorator('price', {
						rules: [{
							// pattern: /^\d+((\.?)\d+)?$/,
							pattern: /^\d{0,6}$/,
							message: '只能为数字,最多6位!',
						}],
					})(
						<Input placeholder='非必填，不填为免费!' className='share__price' />
					)}（¥：元）
				  </FormItem>
				<FormItem
					{...formItemLayout}
					label="简介"
				>
					{getFieldDecorator('introduction', {
						rules: [{
							required: true,
							message: '必填，10000字以内!',
							transform: (value) => this.handelTransform(value),
							pattern: /^(?!.{10001}|\s*$)/g,
						}],
					})(
						<TextArea placeholder="必填，10000字以内!" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...ImageLayout}
				>
					{getFieldDecorator('img')(
						<ImageUpload max={10} fileList={this.props.share.imgList} handleFile={() => this.handleFile('img')} count={this.props.share.count} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="附件"
				>
					{getFieldDecorator('attachment')(
						<FileUpload max={5} fileList={this.props.share.attachmentList} handleFile={() => this.handleFile('attachment')} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="发布身份"
				>
					{getFieldDecorator('identityName', {
						rules: [{
							required: true,
							message: '请选择发布身份!',
						}],
						// initialValue: '个人'
						// initialValue: this.props.share.identityType === 0 ? '个人' : this.props.share.identityName
					})(
						<TreeSelect
							style={{ width: 300 }}
							dropdownStyle={{ maxHeight: 400, overflow: 'auto' }}
							treeData={orgList}
							onChange={this.onChange}
						/>
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
// attachment	附件	string
// creatorAvatar	创建者头像	string
// creatorId	创新者id	string
// creatorName	创建者名字	string
// identityId	发布身份id	string
// identityName	发布身份名字	string
// identityType	发布身份类型	number	0: 个人; 1: 组织机构;
// img	图片	string
// introduction	简介	string
// price	售价	string
// shareId		string	若share_id为null，则为分享发布；若不为null，则为分享管理的编辑
// title	标题	string
// token	身份令牌	string	
const FormMap = Form.create({
	mapPropsToFields: (props) => {
		// console.log('分享数据回显 - ', props)
		const share = props.share;
		return {
			shareId: Form.createFormField({
				value: share.shareId
			}),
			title: Form.createFormField({
				value: share.title
			}),
			price: Form.createFormField({
				value: share.price
			}),
			introduction: Form.createFormField({
				value: share.introduction
			}),
			img: Form.createFormField({
				value: share.img
			}),
			attachment: Form.createFormField({
				value: share.attachment
			}),
			identityName: Form.createFormField({
				value: share.identityType === 0 ? '个人' : share.identityName
			}),
		}
	}
})(Base);

@connect(
	state => {
		return {
			user: state.userReducer,
			share: state.shareReducer,
		}
	},
	{ saveShareInfo, changeIdentity }
)
class Share extends Component {
	constructor(props) {
		super(props);
	}

	componentWillMount() {
		let { shareId } = this.props.params;
		let user = this.props.user;
		if (shareId) { // 编辑
			this.handleEdit(shareId)
		} else { //发布 (设置一些信息信息为当前用户信息)
			this.props.changeIdentity({
				identityId: user.userId,
				identityName: user.userName,
				identityType: 0,
				creatorId: user.userId,
				creatorName: user.userName,
				creatorAvatar: user.avatar
			})
		}
	}

	// 编辑分享，根据id查询详情
	handleEdit = async (shareId) => {
		try {
			let { shareData } = await getShareInfo({ shareId, handle: 1 })
			console.log('shareData - ', shareData)
			let { imgList, attachmentList } = shareData
			let img = []
			let attachment = []
			imgList.length > 0 && imgList.forEach(file => {
				img.push(file.fileId)
			}) // 获取文件url
			attachmentList.length > 0 && attachmentList.forEach(file => {
				attachment.push(file.fileId)
			}) // 获取附件id
			this.props.saveShareInfo({
				...shareData,
				img: img.join(';'),
				attachment: attachment.join(';'),
				count: imgList.length
			})
		} catch (e) {
			message.error('获取详情失败')
			throw new Error(e)
		}
	}
	render() {
		return (
			<Nav>
				<FormMap share={this.props.share} />
			</Nav>
		);
	}
}

export default Share;