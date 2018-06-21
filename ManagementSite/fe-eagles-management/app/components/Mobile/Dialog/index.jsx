import React,{ Component } from 'react';
import { Dialog } from 'react-qtui';

class MyDialog extends Component {
  constructor(props, context) {
    super(props, context);
    this.state = {
      showDialog: false,
      style: {
        button1: [
          {
            label: '确定',
            onClick: this.hideDialog.bind(this),
            type: 'primary'
          }
        ],
        button2: [
          {
            type: 'default',
            label: '取消',
            onClick: this.hideDialog.bind(this)
          },
          {
            type: 'primary',
            label: '确定',
            onClick: this.confirmDialog.bind(this)
          }
        ]
      },
      value: ''
    };
  }

  hideDialog() {
    const noConfirmFn = this.props.noCancelReserve;
    noConfirmFn();
  }

  confirmDialog() {
    const confirmFn = this.props.confirmCancel;
    confirmFn();
  }

  componentWillReceiveProps(nextProps) {
    this.setState({
      showDialog: nextProps.showDialog,
      value: nextProps.value
    });
  }

  render() {
    let type = this.props.type;
    let button = [];
    if (type == 'know') {
      button = this.state.style.button1;
    } else {
      button = this.state.style.button2;
    }
    return (
      <div>
        <Dialog
          type="ios"
          title={this.state.style.title}
          buttons={button}
          show={this.state.showDialog}
        >
          {this.state.value}
        </Dialog>
      </div>
    );
  }
}

export default MyDialog;
