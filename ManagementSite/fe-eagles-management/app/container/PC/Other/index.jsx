import React from 'react';
import { connect } from "react-redux";
import './style.less'

@connect(
  state => {
    return {
      user: state.userReducer,
    }
  },
  null
)
export default class OtherPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      otherUrl: ''
    }
  }
  componentWillMount() {
    let { appId } = this.props.params;
    let { appDataList } = this.props.user;
    appDataList.map((app, index) => {
      if (app.id == appId) {
        document.title = app.name;
        this.setState({
          otherUrl: app.pcUrl
        });
      }
    });
  }
  render() {
    const { otherUrl } = this.state;
    return (
      <div className="other-page">
        <iframe  src={otherUrl}></iframe>
      </div>
    );
  }
}