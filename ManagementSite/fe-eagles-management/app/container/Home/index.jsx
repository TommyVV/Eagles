import React, { Component } from "react";
import Nav from "../Nav";
import { getTreeInfo } from "../../services/orgService";
import { Tree, Icon ,message} from "antd";
const TreeNode = Tree.TreeNode;

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
  render() {
    const { tree } = this.state;
    const { BranchInfos } = tree;
    return (
      <Nav>
        {BranchInfos ? (
          <Tree  showIcon defaultExpandAll  icon={<Icon type="tag" />}>
            <TreeNode title={tree.OrgName} key={tree.OrgId}>
              {BranchInfos.map((o, i) => {
                return (
                  <TreeNode
                    title={o.BranchName}
                    key={o.BranchId}
                    icon={<Icon type="tag-o" />}
                  />
                );
              })}
            </TreeNode>
          </Tree>
        ) : null}
      </Nav>
    );
  }
}

export default HomePage;
