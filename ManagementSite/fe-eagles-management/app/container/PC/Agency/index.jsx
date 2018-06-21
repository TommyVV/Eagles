import React, { Component, PureComponent } from 'react';
import { connect } from "react-redux";
import { Form, Input, Row, Col, Button, message, Tag, Tooltip, Icon, Avatar } from 'antd';
import ImageUpload from '../../../components/PC/ImageUpload';
// import AvatarUpload from '../../../components/PC/AvatarUpload/';
import { serverConfig } from "../../../constants/ServerConfigure";
import { saveAgency, reviewAgency, getAgencyInfoById, getAgencyIdById } from "../../../services/agencyService";
import { saveAgencyInfo, saveLabel, saveFileUrl, chooseMember, removeMemberFn, clearInfo } from "../../../actions/PC/agendyAction";
import { showModal, hideModal } from "../../../actions/PC/appAction";
import Member from "../../../components/PC/Member";
import MemberList from "../../../components/PC/MemberList";
import Crop from "../../../components/PC/Crop";
import Nav from '../Nav';
import './style.less';
import { deleteFile } from "../../../services/fileService";

const { TextArea } = Input;
const FormItem = Form.Item;
const ORG = serverConfig.ORG;

@connect(
	state => {
		return {
			user: state.userReducer,
			agency: state.agencyReducer
		}
	},
	{ saveAgencyInfo, saveLabel, saveFileUrl, chooseMember, removeMemberFn, showModal, hideModal, clearInfo }
)
class Base extends PureComponent {
	constructor(props) {
		super(props);
		this.state = {
			isAudit: false, //是否正在审核
			tags: [],//标签
			flag: true,
			inputVisible: false,//输入框可见性
			inputValue: '',//输入框值
			maxTag: 10,//最大标签数
			showMemberList: false,
			showCrop: false,
		}
	}

	componentWillReceiveProps(props) {
		let tags = props.agency.label !== '' ? props.agency.label.split(';') : []
		this.setState({ tags })
	}

	componentWillUnmount() {
		if (this.state.flag) {
			let { uploadDeleteList } = this.props.agency;
			uploadDeleteList.length > 0 && this.handleDeleteFile(uploadDeleteList)
		}
		this.props.clearInfo()
	}

	// 在验证前处理多行文本的回车符
	handelTransform = (value) => {
		if (value) {
			const reg = /\s/g
			return value.replace(reg, '')
		}
	}
	// creatorAvatar	创建者头像	string
	// creatorId	创建者id	string
	// creatorName	创建者名称	string
	// 保存
	save = (e) => {
		e.preventDefault();
		this.props.form.validateFields(async (err, values) => {
			if (!err) {
				try {
					let params = {
						...this.props.agency,
						...values,
						creatorAvatar: this.props.user.avatar,
						creatorId: this.props.user.userId,
						creatorName: this.props.user.userName
					}
					console.log('Received params of form: ', params);
					this.props.showModal();
					let res = await saveAgency(params)
					let { code } = res
					if (code === 0) {
						this.props.saveAgencyInfo({ ...params, approvalStatus: 0 })
						this.setState({ flag: false })
						this.props.hideModal();
						let { deleteList } = this.props.agency;
						deleteList.length > 0 && this.handleDeleteFile(deleteList)
						message.success('保存机构成功')
					} else {
						this.props.hideModal();
						message.error('保存机构失败')
					}
				} catch (e) {
					message.error('保存机构失败')
					this.props.hideModal();
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
				try {
					let params = {
						...values,
						creatorAvatar: this.props.user.avatar,
						creatorId: this.props.user.userId,
						creatorName: this.props.user.userName
					}
					console.log('Received params of form: ', params);
					let { approvalStatus } = this.props.agency;
					this.props.showModal();
					this.props.saveAgencyInfo(params)
					// this.props.saveAgencyInfo({ ...params, approvalStatus: 2 })
					let res = await reviewAgency(params)
					let { code } = res
					if (code === 0) {
						this.setState({ flag: false })
						this.props.saveAgencyInfo({ approvalStatus: 2 })
						this.props.hideModal();
						let { deleteList } = this.props.agency;
						deleteList.length > 0 && this.handleDeleteFile(deleteList)
						message.success('已提交审核')
					} else {
						this.props.hideModal();
						// this.props.saveAgencyInfo({ approvalStatus })
						message.error('提交审核失败')
					}
				} catch (e) {
					message.error('提交审核失败')
					this.props.hideModal();
					throw new Error(e)
				}
			} else {
				message.error('请检查信息是否填写完毕')
			}
		});
	}
	// ------------- 标签相关 -------------
	// 关闭标签
	handleClose = (removedTag) => {
		let { getFieldsValue } = this.props.form
		let values = getFieldsValue() // 改变发布身份前获取填写的数据
		const tags = this.state.tags.filter(tag => tag !== removedTag);
		console.log('删除标签 - ', tags);
		this.props.saveLabel({ ...values, label: tags.join(';') })
	}
	showInput = () => {
		this.setState({ inputVisible: true }, () => this.input.focus());
	}
	handleInputChange = (e) => {
		this.setState({ inputValue: e.target.value });
	}
	// 标签确认
	handleInputConfirm = () => {
		const state = this.state;
		const inputValue = state.inputValue;
		let tags = state.tags;
		if (inputValue && tags.indexOf(inputValue) === -1) {
			tags = [...tags, inputValue];
		}
		console.log('添加标签 - ', tags);
		let { getFieldsValue } = this.props.form
		let values = getFieldsValue() // 改变发布身份前获取填写的数据
		this.props.saveLabel({ ...values, label: tags.join(';') })
		this.setState({
			inputVisible: false,
			inputValue: '',
		});
	}
	handleDeleteFile = async (fileIdList) => {
		try {
			await deleteFile({ fileIdList })
		} catch (e) {
			throw new Error(e)
		}
	}
	saveInputRef = input => this.input = input

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
				let { deleteList } = _this.props.agency;
				deleteList.push(fileId);
				let count = attr + 'Count'
				_this.props.saveFileUrl({ [attr]: noUndefindArray.join(';'), deleteList, [count]: list.length });
			},
			done(list, map, fileId) { // list 为当前图片list 、map为uid和fileId的关联关系
				if (attr === 'avatar') {
					_this.props.saveFileUrl({ [attr]: list });
					return;
				}
				let idList = [];
				let { uploadDeleteList } = _this.props.agency;
				list.forEach(file => {
					if (file.status) {//从编辑中获取fileId
						idList.push(map.get(file.uid))
					}
					idList.push(file.fileId)
				})
				let noUndefindArray = idList.filter(v => v)
				uploadDeleteList.push(fileId)
				let count = attr + 'Count'
				_this.props.saveFileUrl({
					[attr]: noUndefindArray.join(';'),
					[count]: noUndefindArray.length,
					uploadDeleteList
				});
			}
		}
	}
	// 传递图片前将数据保存
	saveInfo = () => {
		let { getFieldsValue } = this.props.form
		let values = getFieldsValue()
		this.props.saveAgencyInfo(values)
		// console.log('上传图片记录表单数据 - ', values, this.props.share)
	}

	cheackStatus = (status, handleHistoryList) => {
		let current = ''
		switch (status) {
			case 0:
				current = '未发布'
				break;
			case 1:
				current = '已发布'
				break;
			case 2:
				current = '审核中'
				break;
			case 3:
				current = '未通过审核'
				break;
			default:
				current = '暂无机构详情'
				break;
		}
		return current;
	}

	showModal = (attr) => {
		this.setState({
			[attr]: true,
		});
	}

	handleCancel = (attr) => {
		this.setState({
			[attr]: false,
		});
	}

	// 移除已选择用户
	removeMember = (current) => {
		this.saveInfo();
		let member = [...this.props.agency.memberList]
		let memberMap = new Map()
		member.forEach(v => memberMap.set(v.user_id, v))
		memberMap.delete(current.user_id)
		let newMember = []
		let managerIds = []
		for (let [key, value] of memberMap) {
			newMember.push(value)
			managerIds.push(key)
		}
		this.props.removeMemberFn({ memberList: newMember, managerIds: managerIds.join(';') })
	}

	// 确认选择用户
	confirmFn = (list) => {
		this.saveInfo();
		let managerIds = []
		list.forEach(v => managerIds.push(v.user_id))
		this.props.chooseMember({ memberList: list, managerIds: managerIds.join(';') })
	}

	render() {
		const { getFieldDecorator } = this.props.form;
		const { tags, inputVisible, inputValue, maxTag, showMemberList, showCrop } = this.state;
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
		const status = this.cheackStatus(this.props.agency.approvalStatus, this.props.agency.handleHistoryList)
		const uploadButton = (
			<div>
				{/* <Icon type={loading ? 'loading' : 'plus'} /> */}
				<Icon type={'plus'} />
				<div className="ant-upload-text">点击添加头像</div>
			</div>
		);
		return (
			<Form className='agency-form'>
				{/*---------------------------------------------------------基本信息---------------------------------------------------------*/}
				<p className='status'>
					<span className='status_title'>机构状态：{status}</span>
					<div className='status_desc'>
						{status === '未通过审核' ? <span className='status_rejectReason' dangerouslySetInnerHTML={{ __html: this.props.agency.handleHistoryList[this.props.agency.handleHistoryList.length - 1].rejectReason.replace(/\n|\r\n/g, '<br />') }} /> : null}
					</div>
				</p>
				<h2>基本信息</h2>
				<FormItem
					{...formItemLayout}
					label=""
					style={{ display: 'none' }}
				>
					{getFieldDecorator('orgId')(
						<Input />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="公司名称"
				>
					{getFieldDecorator('companyName', {
						rules: [{
							required: true,
							message: '必填，100字以内!',
							pattern: /^(?!.{101}|\s*$)/g
						}],
					})(
						<Input placeholder='必填，100字以内!' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="公司简称"
				>
					{getFieldDecorator('abbreviation', {
						rules: [{
							required: true,
							message: '必填，6字以内!',
							pattern: /^(?!.{7}|\s*$)/g
						}],
					})(
						<Input placeholder='必填，6字以内!' />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="标签"
				>
					{getFieldDecorator('label', {
						rules: [{
							required: true,
							message: `必填，最多${maxTag}个标签!`,
							// pattern: /^.{1,50}$/,
						}],
					})(
						<div>
							{tags.length > 0 ? tags.map((tag, index) => {
								const isLongTag = tag.length > 20;
								const tagElem = (
									<Tag key={tag} closable={true} afterClose={() => this.handleClose(tag)}>
										{isLongTag ? `${tag.slice(0, 20)}...` : tag}
									</Tag>
								);
								return isLongTag ? <Tooltip title={tag} key={tag}>{tagElem}</Tooltip> : tagElem;
							}) : null}
							{inputVisible && (
								<Input
									ref={this.saveInputRef}
									type="text"
									size="small"
									style={{ width: 78 }}
									value={inputValue}
									onChange={this.handleInputChange}
									onBlur={this.handleInputConfirm}
									onPressEnter={this.handleInputConfirm}
								/>
							)}
							{!inputVisible && tags.length < maxTag && (
								<Tag
									onClick={this.showInput}
									style={{ background: '#fff', borderStyle: 'dashed' }}
								>
									<Icon type="plus" /> 添加标签
          			</Tag>
							)}
						</div>
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="头像"
				>
					{getFieldDecorator('avatar')(
						// <AvatarUpload handleFile={() => this.handleFile('avatar')} />
						<span className="avatar-uploader  self-style">
							<div className="ant-upload ant-upload-select ant-upload-select-picture-card">
								<span className="ant-upload" onClick={() => this.showModal('showCrop')}>
									{
										this.props.agency.avatar ? <img src={this.props.agency.avatar} alt="" className='crop-avatar' />
											:
											<div>
												<Icon type='plus' />
												<div className="ant-upload-text">点击添加头像</div>
											</div>
									}
								</span>
							</div>
						</span>
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="公司资质"
				>
					{getFieldDecorator('qualification', {
						rules: [{
							required: true,
							message: '必填，500字以内',
							transform: (value) => this.handelTransform(value),
							pattern: /^.{1,500}$/,
						}],
					})(
						<TextArea placeholder="必填，500字以内" autosize={{ minRows: 6, maxRows: 100 }} />
					)}
				</FormItem>
				{/*---------------------------------------------------------业务信息---------------------------------------------------------*/}
				<h2>业务信息</h2>
				<FormItem
					{...formItemLayout}
					label="业务名称"
				>
					{getFieldDecorator('businessName', {
						rules: [{
							required: true,
							message: '必填，100字以内!',
							pattern: /^(?!.{101}|\s*$)/g
						}],
					})(
						<Input placeholder="必填，100字以内" />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="服务内容"
				>
					{getFieldDecorator('serviceContent', {
						rules: [{
							required: true,
							message: '必填，1000字以内!',
							transform: (value) => this.handelTransform(value),
							pattern: /^(?!.{1001}|\s*$)/g
						}],
					})(
						<TextArea placeholder="必填，1000字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...ImageLayout}
				>
					{getFieldDecorator('serviceContentImg')(
						<ImageUpload fileList={this.props.agency.serviceContentImgList} handleFile={() => this.handleFile('serviceContentImg')} count={this.props.agency.serviceContentImgCount} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="能力保障"
				>
					{getFieldDecorator('capacityProtection', {
						rules: [{
							required: true,
							message: '选填，500字以内!',
							transform: (value) => this.handelTransform(value),
							pattern: /^(?!.{501}|\s*$)/g
						}],
					})(
						<TextArea placeholder="选填，500字以内" autosize={{ minRows: 6, maxRows: 100 }} />
					)}
				</FormItem>
				<FormItem
					{...ImageLayout}
					label=""
				>
					{getFieldDecorator('capacityProtectionImg')(
						<ImageUpload fileList={this.props.agency.capacityProtectionImgList} handleFile={() => this.handleFile('capacityProtectionImg')} count={this.props.agency.capacityProtectionImgCount} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="突出业绩"
				>
					{getFieldDecorator('outstandingPerformance', {
						rules: [{
							required: true,
							message: '选填，500字以内!',
							transform: (value) => this.handelTransform(value),
							pattern: /^(?!.{501}|\s*$)/g
						}],
					})(
						<TextArea placeholder="选填，500字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...ImageLayout}
					label=""
				>
					{getFieldDecorator('outstandingPerformanceImg')(
						<ImageUpload fileList={this.props.agency.outstandingPerformanceImgList} handleFile={() => this.handleFile('outstandingPerformanceImg')} count={this.props.agency.outstandingPerformanceImgCount} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="典型案例"
				>
					{getFieldDecorator('typicalCase', {
						rules: [{
							required: true,
							message: '选填，500字以内!',
							transform: (value) => this.handelTransform(value),
							pattern: /^(?!.{501}|\s*$)/g
						}],
					})(
						<TextArea placeholder="选填，500字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...ImageLayout}
					label=""
				>
					{getFieldDecorator('typicalCaseImg')(
						<ImageUpload fileList={this.props.agency.typicalCaseImgList} handleFile={() => this.handleFile('typicalCaseImg')} count={this.props.agency.typicalCaseImgCount} />
					)}
				</FormItem>
				<FormItem
					{...formItemLayout}
					label="其他信息"
				>
					{getFieldDecorator('otherInformation', {
						rules: [{
							required: true,
							message: '选填，500字以内!',
							transform: (value) => this.handelTransform(value),
							pattern: /^(?!.{501}|\s*$)/g
						}],
					})(
						<TextArea placeholder="选填，500字以内" autosize={{ minRows: 6, maxRows: 10 }} />
					)}
				</FormItem>
				<FormItem
					{...ImageLayout}
					label=""
				>
					{getFieldDecorator('otherInformationImg')(
						<ImageUpload fileList={this.props.agency.otherInformationImgList} handleFile={() => this.handleFile('otherInformationImg')} count={this.props.agency.otherInformationImgCount} />
					)}
				</FormItem>
				{/*---------------------------------------------------------管理者设置---------------------------------------------------------*/}
				<FormItem
					label=""
				>
					{getFieldDecorator('managerIds')(
						<h2>管理者设置 <small>（提交审核后管理者设置即刻生效）</small></h2>
					)}
					<Member memberList={this.props.agency.memberList} removeMember={this.removeMember} show={() => this.showModal('showMemberList')} />
					{/* <Row>
						{this.props.agency.memberList.map(member => (
							<Col key={member.memberId} span={3} className='member-box'>
								<Avatar src={member.memberAvatar} />
								<p className='member-name'>{member.memberName}</p>
							</Col>
						))}
						<Col span={3} >
							<Icon type="plus-circle-o" className='add-member'/>
						</Col>
					</Row> */}
				</FormItem>
				<FormItem>
					<Row type='flex' justify='center' gutter={24}>
						<Col>
							<Button type='primary' onClick={this.review} className='btn btn--primary' disabled={this.props.agency.approvalStatus === 2 ? true : false} >提交审核</Button>
						</Col>
						<Col>
							<Button onClick={this.save} className='btn' disabled={this.props.agency.approvalStatus === 2 ? true : false}>保存</Button>
						</Col>
					</Row>
				</FormItem>
				{showMemberList ? <MemberList onCancel={() => this.handleCancel('showMemberList')} confirmFn={this.confirmFn} member={this.props.agency.memberList} /> : null}
				{showCrop ? <Crop handleFile={() => this.handleFile('avatar')} onCancel={() => this.handleCancel('showCrop')} /> : null}
			</Form>
		);
	}
}
// abbreviation: '', //公司简称 string
// avatar: '', //头像 string
// businessName: '', //业务名称 string
// capacityProtection: '', //能力保障 string
// capacityProtectionImg: '', //能力保障图片 string 机构保存的图片全以;隔开的字符串
// companyName: '', //公司名称 string
// creatorAvatar: '', //创建者头像 string
// creatorId: '', //创建者id string
// creatorName: '', //创建者名称 string
// label: '', //标签 string
// managerIds: '', //管理者id列表 string 多条数据使用“；” 隔开
// otherInformation: '', //其他信息 string
// otherInformationImg: '', //其他信息图片 string
// outstandingPerformance: '', //突出业绩 string
// outstandingPerformanceImg: '', //突出业绩图片 string
// qualification: '', //公司资质 string
// serviceContent: '', //服务内容 string
// serviceContentImg: '', //服务内容图片 string
// token: '', //身份令牌 string
// typicalCase: '', //典型案例 string
// typicalCaseImg: '', //典型案例图片 string
const FormMap = Form.create({
	mapPropsToFields: (props) => {
		console.log('机构详情数据回显 - ', props)
		const agency = props.agency;
		return {
			orgId: Form.createFormField({
				value: agency.orgId
			}),
			abbreviation: Form.createFormField({
				value: agency.abbreviation
			}),
			avatar: Form.createFormField({
				value: agency.avatar
			}),
			businessName: Form.createFormField({
				value: agency.businessName
			}),
			capacityProtection: Form.createFormField({
				value: agency.capacityProtection
			}),
			capacityProtectionImg: Form.createFormField({
				value: agency.capacityProtectionImg
			}),
			companyName: Form.createFormField({
				value: agency.companyName
			}),
			label: Form.createFormField({
				value: agency.label
			}),
			managerIds: Form.createFormField({
				value: agency.managerIds
			}),
			otherInformation: Form.createFormField({
				value: agency.otherInformation
			}),
			otherInformationImg: Form.createFormField({
				value: agency.otherInformationImg
			}),
			outstandingPerformance: Form.createFormField({
				value: agency.outstandingPerformance
			}),
			outstandingPerformanceImg: Form.createFormField({
				value: agency.outstandingPerformanceImg
			}),
			qualification: Form.createFormField({
				value: agency.qualification
			}),
			serviceContent: Form.createFormField({
				value: agency.serviceContent
			}),
			serviceContentImg: Form.createFormField({
				value: agency.serviceContentImg
			}),
			typicalCase: Form.createFormField({
				value: agency.typicalCase
			}),
			typicalCaseImg: Form.createFormField({
				value: agency.typicalCaseImg
			}),
		}
	}
})(Base);

@connect(
	state => {
		return {
			user: state.userReducer,
			agency: state.agencyReducer,
		}
	},
	{ saveAgencyInfo }
)
class Advisory extends Component {
	constructor(props) {
		super(props);
	}

	componentWillMount() {
		this.getOrgInfo()
	}


	// 获取当前用户组织机构信息
	getOrgInfo = async () => {
		try {
			let res = await getAgencyIdById() //通过token查询组织
			let { code, data } = res
			console.log('获取当前用户组织机构信息 - ', res)
			let author = {
				name: this.props.user.userName,
				user_id: this.props.user.userId,
				avatar: this.props.user.avatar
			}
			this.props.saveAgencyInfo({ memberList: [author], managerIds: author.user_id })
			if (code === 0) {
				if (data.length > 1) {// 存在机构
					let { identityId } = data[1]
					let res = await getAgencyInfoById({ orgId: identityId, handle: 0 })//通过orgId查询组织详情
					console.log('通过orgId查询组织详情 - ', res)
					let { memberList, capacityProtectionImgList, otherInformationImgList, outstandingPerformanceImgList, serviceContentImgList, typicalCaseImgList } = res.data
					let capacityProtectionImg = []  //能力保障图片
					let otherInformationImg = [] //其他信息图片
					let outstandingPerformanceImg = [] //突出业绩图片
					let serviceContentImg = [] //服务内容图片
					let typicalCaseImg = [] //典型案例图片
					capacityProtectionImgList.length > 0 && capacityProtectionImgList.forEach(file => {
						capacityProtectionImg.push(file.fileId)
					}) // 能力保障图片文件url
					otherInformationImgList.length > 0 && otherInformationImgList.forEach(file => {
						otherInformationImg.push(file.fileId)
					}) // 其他信息图片文件url
					outstandingPerformanceImgList.length > 0 && outstandingPerformanceImgList.forEach(file => {
						outstandingPerformanceImg.push(file.fileId)
					}) // 突出业绩图片文件url
					serviceContentImgList.length > 0 && serviceContentImgList.forEach(file => {
						serviceContentImg.push(file.fileId)
					}) // 服务内容图片文件url
					typicalCaseImgList.length > 0 && typicalCaseImgList.forEach(file => {
						typicalCaseImg.push(file.fileId)
					}) // 典型案例图片文件url
					this.props.saveAgencyInfo({
						...res.data,
						capacityProtectionImg: capacityProtectionImg.join(';'),
						capacityProtectionImgCount: capacityProtectionImg.length,
						otherInformationImg: otherInformationImg.join(';'),
						otherInformationImgCount: otherInformationImg.length,
						outstandingPerformanceImg: outstandingPerformanceImg.join(';'),
						serviceContentImg: serviceContentImg.join(';'),
						serviceContentImgCount: serviceContentImg.length,
						typicalCaseImg: typicalCaseImg.join(';'),
						typicalCaseImgCount: typicalCaseImg.length,
					})
					// this.props.saveAgencyInfo(res.data)
				}
			} else {
				message.error('获取机构详情失败')
			}
		} catch (e) {
			throw new Error(e)
		}
	}
	render() {
		return (
			<Nav>
				<FormMap agency={this.props.agency} />
			</Nav>
		);
	}
}

export default Advisory;
