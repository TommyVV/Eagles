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
              {i != 0 ? (
                <Fragment key={Math.random()}>
                  <tr>
                    <td colSpan="8" className="b-left v-spa" />
                  </tr>
                  <tr>
                    <td colSpan="8" className="b-left v-spa" />
                  </tr>
                </Fragment>
              ) : (
                  <tr>
                    <td colSpan="8" className="b-left v-spa" />
                  </tr>
                )}

              <tr>
                {/* <td className="h-spa b-left bbottom jd">
                  <span />
                </td> */}
                {/* <td className="h-spa  bbottom ">
                  <span />
                </td> */}
                {arr.map((o, index) => {
                  return (
                    <Fragment key={Math.random()}>
                      <td className={index == 0 ? "h-spa b-left bbottom " : "h-spa  bbottom "} key={Math.random()} />
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
                {/* <td className="btop" /> */}
              </tr>
              <tr>
                {arr.map((o, index) => {
                  return (
                    <Fragment key={Math.random()}>
                      <td className={index == 0 && i < result.length - 1 ? "spa b-left" : "spa "}></td>
                      <td className="item">
                        <table cellSpacing="0">
                          <tbody>
                            <tr>
                              <td className="hs-spa b-left bbottom"></td>
                              <td rowSpan="2">
                                <div className="item-c">
                                  <span>书记</span>
                                  <span className="right">{o.BranchSecretary}</span>
                                </div>
                              </td>
                            </tr>
                            <tr>
                              <td className="b-left btop"></td>
                            </tr>
                            <tr>
                              <td className="hs-spa b-left bbottom"></td>
                              <td rowSpan="2">
                                <div className="item-c">
                                  <span>副书记</span>
                                  <span className="right">{o.BranchViceSecretary}</span>
                                </div>
                              </td>
                            </tr>
                            <tr>
                              <td className="b-left btop"></td>
                            </tr>
                            <tr>
                              <td className="hs-spa b-left bbottom"></td>
                              <td rowSpan="2">
                                <div className="item-c">
                                  <span>党员数</span>
                                  <span className="right">{o.BranchUserCount}</span>
                                </div>
                              </td>
                            </tr>
                            <tr>
                              <td className="btop"></td>
                            </tr>
                          </tbody>
                        </table>
                      </td>
                    </Fragment>
                  );
                })}


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
                  <td className="bbottom"></td>
                  <td className="bbottom"></td>
                  <td className="bbottom"></td>
                  <td className="bbottom"></td>
                  <td className="bbottom"></td>
                </tr>
                <tr>
                  <td className="h-spa b-left btop"></td>
                  <td className="btop"></td>
                  <td className="bright btop"></td>
                  <td className="btop bleft"></td>
                  <td className="btop"></td>
                  <td className="btop bright"></td>
                  <td className="bleft"></td>
                </tr>
                <tr>
                  <td className="h-spa b-left" colSpan="2"></td>
                  <td className="h-spa"></td>
                  <td colSpan="2" className="second-level">
                    <span className="aleft">书记</span>
                    <span className="aright">{tree.OrgSecretary}</span>
                  </td>
                  <td className="h-spa"></td>
                  <td colSpan="2" className="second-level">
                    <span className="aleft">副书记</span>
                    <span className="aright">{tree.OrgViceSecretary}</span>
                  </td>
                </tr>
                <tr>
                  <td colSpan="8" className="b-left v-spa"></td>
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
