import React,{ Component } from 'react';
import './style.less';
class Search extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      searchValue: ''
    };
  }
  onClickHandle() {
    let clickBtn = this.props.clickBtn;
    clickBtn();
  }
  render() {
    return (
      <div className="qtui-search-bar search-bar">
        <form className="qtui-search-bar__form">
          <div className="qtui-search-bar__box">
            <i className="qtui-icon-search" />
            <input
              type="search"
              className="qtui-search-bar__input"
              id="searchInput"
              placeholder="搜索组织机构"
              onChange={e =>
                this.setState({ searchValue: e.target.value.trim() })}
              required
            />
            <a
              href="javascript:"
              className="qtui-icon-clear"
              id="searchClear"
            />
          </div>
        </form>
        <span className="search-btn" onClick={this.onClickHandle.bind(this)}>
          确定
        </span>
      </div>
    );
  }
}

export default Search;
