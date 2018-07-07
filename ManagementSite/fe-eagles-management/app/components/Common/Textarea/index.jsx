import React, { Component } from 'react';
import { FormCell, CellHeader, CellBody, Label, TextArea } from 'react-qtui';
import Utils from '../../../utils/util';

export default class FormTextarea extends Component {
    constructor(props, context) {
        super(props, context);
        this.state = {
            ...this.state,
            value: this.props.value,
            hasDefaultValue: false
        };
    }

    componentWillReceiveProps(nextProps) {
        const value = nextProps.value;
        this.state.value = value;
        const { isEdit } = this.props;
        if (isEdit && value) {
            this.state.hasDefaultValue = true;
        }
    }

    changeHandle(event) {
        const { type } = this.props;
        let value = Utils.filterEmoji(event.target.value);
        if (type == 'tel') {
            value = value.replace(/[^\d]/g, '');
        }
        this.setState({
            value
        });
        const changeFn = this.props.onChange;
        changeFn(this.props.objKey, value);
    }


    render() {
        const { label, placeholder, maxLength, isEdit } = this.props;
        const { value, hasDefaultValue } = this.state;
        return (
            <FormCell >
                <CellHeader>
                    <Label>{label}:</Label>
                </CellHeader>
                <CellBody>
                    {
                        isEdit ? (
                            value || hasDefaultValue ?
                                <TextArea onChange={this.changeHandle.bind(this)}
                                    placeholder={placeholder} rows="2"
                                    maxLength={maxLength} defaultValue={value} value={value} />
                                : null
                        ) :
                            <TextArea onChange={this.changeHandle.bind(this)}
                                placeholder={placeholder} rows="2"
                                maxLength={maxLength} value={value} />
                    }
                </CellBody>
            </FormCell>
        );
    }
}