import React, { Component } from 'react';
import { Article } from 'react-qtui';
import './style.less';

class RenderImage extends Component {

  static defaultProps = {
    imgList: []
  }
  
  render() {
    const { imgList } = this.props
    console.log('imgList - ',this.props)
    const List = imgList.length > 0 ? imgList.map((img, index) => (
      <img src={img.fileUrl} alt="" key={img.fileId} />
    )) : null
    return (
      <Article className='images--list'>
        {List}
      </Article>
    );
  }
}

export default RenderImage;