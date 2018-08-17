import React, { Component } from "react";
const Fragment = React.Fragment;
import Nav from "../Nav";
import { getTreeInfo } from "../../services/orgService";
import { message } from "antd";
// const TreeNode = Tree.TreeNode;
const _ = require("lodash");
import "./style.less";

class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tree: {}
    };
  }
  componentDidMount() {
    this.getInfo();
  }
  // 根据id查询详情
  getInfo = async () => {
    try {
      const { Info } = await getTreeInfo();
      console.log(Info);
      this.setState({
        tree: Info
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  renderTree(tree) {
    if (tree) {
      const result = _.chunk(tree, 3);
      return result.map((arr, i) => {
        return (
          <table className="list" cellSpacing="0" key={i}>
            <tbody>
              <tr>
                <td colSpan="8" className="b-left v-spa" />
              </tr>
              <tr>
                <td className="h-spa b-left bbottom jd">
                  <span />
                </td>
                <td className="h-spa  bbottom ">
                  <span />
                </td>
                {arr.map((o, index) => {
                  return (
                    <Fragment key={Math.random()}>
                      <td className="h-spa bbottom" key={Math.random()} />
                      <td
                        rowSpan="2"
                        className="first-level"
                        key={Math.random()}
                      >
                        <div className="textover">{o.BranchName}</div>
                      </td>
                    </Fragment>
                  );
                })}
              </tr>
              <tr>
                {i == result.length - 1 ? null : (
                  <td className="spa b-left btop" />
                )}

                <td className="btop" />
                <td className="btop" />
              </tr>
            </tbody>
          </table>
        );
      });
    }
  }
  render() {
    const { tree } = this.state;
    const { BranchInfos } = tree;
    const renderTree = this.renderTree(BranchInfos);
    return (
      <Nav>
        <div className="area">
          <h1>党建整体情况</h1>
          {tree.OrgName ? (
            <table className="head" cellSpacing="0">
              <tbody>
                <tr>
                  <td className="h-spa bbottom" />
                  <td rowSpan="2" className="first-level">
                    <img
                      src="http://d.umq.cn/coupons/D9CD1A23EE14452087508EEAA5E605E4.png"
                      alt=""
                    />
                    <div className="img-d">{tree.OrgName}</div>
                  </td>
                </tr>
                <tr>
                  <td className="h-spa b-left btop" />
                </tr>
                <tr>
                  <td colSpan="8" className="b-left v-spa" />
                </tr>
              </tbody>
            </table>
          ) : null}

          {renderTree}
        </div>
      </Nav>
    );
  }
}

export default HomePage;
