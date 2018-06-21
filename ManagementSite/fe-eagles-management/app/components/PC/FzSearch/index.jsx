import React from 'react'
import { Input, Select, Spin, Button, Icon, Form } from 'antd';
const Search = Input.Search;
import debounce from 'lodash.debounce';
import './style.less';

const Option = Select.Option;

/**
 * 接受两个函数
 *    获取数据列表 （text，value）
 *    搜索函数  （value）
 * 接受配置
 *    定义样式的配置
 */
export default class FzSearch extends React.Component {
  constructor(props) {
    super(props)
    this.lastFetchId = 0
    this.fetchList = debounce(this.fetchList, 300)
    this.state = {
      data: [],
      value: [],
      fetching: false,
    }
  }

  fetchList = async (value) => {
    try {
      this.lastFetchId += 1
      const fetchId = this.lastFetchId
      this.setState({ data: [], fetching: true })
      let data = this.props.fetchList && await this.props.fetchList(value) || []
      console.log('搜索 - ', data)
      if (fetchId !== this.lastFetchId) { // for fetch callback order
        return
      }
      this.setState({ data, fetching: false })
    } catch (e) {
      console.log(e)
    }
  }

  handleChange = (value) => {
    console.log('value - ', value)
    // this.props.getItem(value)
    this.setState({
      value,
      data: [],
      fetching: false,
    })
  }

  handleSearch = () => {
    let { value } = this.state
    this.props.search(value)
  }


  render() {
    const { fetching, data, value } = this.state;
    const { formLayout } = this.props;
    const formItemLayout = formLayout === 'horizontal' ? {
      labelCol: { span: 4 },
      wrapperCol: { span: 14 },
    } : null;
    const buttonItemLayout = formLayout === 'horizontal' ? {
      wrapperCol: { span: 14, offset: 4 },
    } : null;
    const antIcon = <Icon type="loading" style={{ fontSize: 20 }} spin />;
    return (
      <Form onSubmit={this.handleSubmit} layout='inline' className='fz-form' >
        <Select
          className='fz-search'
          mode="combobox"
          value={value}
          placeholder="搜索"
          notFoundContent={fetching ? <Spin indicator={antIcon} /> : null}
          filterOption={false}
          onSearch={this.fetchList}
          onChange={this.handleChange}
        >
          {data.map(d => <Option key={d.value}>{d.text}</Option>)}
        </Select>
        <span className='fz-icon' onClick={this.handleSearch}>
          <Icon type="search" />
        </span>
        <Button type="primary" htmlType="submit" onClick={this.handleSearch} className='fz-submit'></Button>
      </Form>
    )
  }
}
