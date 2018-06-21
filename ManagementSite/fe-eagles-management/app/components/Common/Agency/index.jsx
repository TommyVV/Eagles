import React, { Component } from 'react';
import IoreShare from "./IoreShare";
import Header from "./Header";
import Container from "./Container";
import './style.less';

class AgencyDetails extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tab: 0
    }
  }
  render() {
    return (
      <div>
        <Header {...this.props} />
        <Container {...this.props}>
          <IoreShare />
        </Container>
      </div>
    );
  }
}

export default AgencyDetails;