import React, { Component } from "react";
import { serverConfig } from "../../../constants/config/ServerConfigure";
// 引入编辑器以及编辑器样式
import BraftEditor, { EditorState } from "braft-editor";
import "braft-editor/dist/index.css";

class Editor extends Component {
  constructor(props) {
    super(props);
    this.state = {
      editorState: EditorState.createFrom("")
    };
  }

  componentWillReceiveProps(pre, next) {
    console.log("pre:", pre);
    let content = pre.content;
    if (content) {
      console.log("componentWillReceiveProps:", content);
      this.setState({
        editorState:
          typeof content == "string" ? EditorState.createFrom(content) : content
      });
    }
  }
  changeContent = content => {
    const { onChange } = this.props;
    onChange(content);
  };
  render() {
    const { text } = this.props;
    const { editorState } = this.state;
    // 编辑器属性
    const editorProps = {
      height: 300,
      contentFormat: "html",
      placeholder: text,
      value: editorState,
      media: {
        pasteImage: true,
        validateFn: file => {
          return file.size < 1024 * 1024 * 5;
        },
        uploadFn: async param => {
          // const res=await uploadFile(file);
          console.log(param);
          let formData = new FormData();
          formData.append("file", param.file);
          var request = new XMLHttpRequest();
          request.open(
            "POST",
            serverConfig.API_SERVER + serverConfig.FILE.UPLOAD
          );
          request.send(formData);
          request.onreadystatechange = function() {
            if (request.readyState == 4 && request.status == 200) {
              let { Result } = JSON.parse(request.responseText);
              let { FileId, FileUrl, FileName } = Result.FileUploadResults[0];
              // 上传成功后调用param.success并传入上传后的文件地址
              param.success({
                url: FileUrl,
                meta: {
                  id: FileId,
                  title: FileName,
                  alt: FileName,
                  loop: false, // 指定音视频是否循环播放
                  autoPlay: false, // 指定音视频是否自动播放
                  controls: true // 指定音视频是否显示控制栏
                  // poster: "http://xxx/xx.png" // 指定视频播放器的封面
                }
              });
            }
          };
        },
        onInsert: files => {
          console.log(files);
        }
      },
      onChange: this.changeContent
    };
    return (
      <div className="editor-wrap">
        <BraftEditor
          {...editorProps}
          ref={instance => (this.editorInstance = instance)}
        />
      </div>
    );
  }
}

export default Editor;
