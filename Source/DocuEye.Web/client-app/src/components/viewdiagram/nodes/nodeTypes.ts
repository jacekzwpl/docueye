import CustomNode from "./CustomNode";
import DeploymentNode from "./DeploymentNode";
import GroupNode from "./GroupNode";

export const nodeTypes = {
    custom: CustomNode,
    customGroup: GroupNode,
    deploymentNode: DeploymentNode
};