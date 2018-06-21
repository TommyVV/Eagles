import React, { Component } from 'react';
import qt from '../../../../static/lib/qingtui_jssdk-2.1';

export default class QtImage extends Component {
    constructor(props, context) {
        super(props, context);
    }

    // componentWillReceiveProps(nextProps) {
    //     this.state.value = nextProps.value;
    // }

    previewImg = (src, srcs) => {
        qt.previewImage({
            current: src,
            urls: srcs,
        });
    }

    render() {
        const { src, srcs, className } = this.props;
        return (
            <img src={src} onClick={() => this.previewImg(src, srcs)} className={className} />
        );
    }
}