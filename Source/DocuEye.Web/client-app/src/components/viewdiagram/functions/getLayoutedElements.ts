import dagre from '@dagrejs/dagre';
import { getRankDirection } from './getRankDirection';

export const getLayoutedElements = (nodes: any[], edges: any[], direction = 'TopBottom') => {

    direction = getRankDirection(direction);

    const dagreGraph = new dagre.graphlib.Graph({
        compound: true,
        multigraph: true
    });
    dagreGraph.setDefaultEdgeLabel(() => ({}));

    const nodeWidth = 190;
    const nodeHeight = 130;

    const isHorizontal = direction === 'LR';
    dagreGraph.setGraph({
        rankdir: direction,
        nodesep: 80,
        ranksep: 20,
        edgesep: 20,
        marginx: 20,
        marginy: 20
    });

    nodes.forEach((node) => {
        dagreGraph.setNode(node.id, { width: nodeWidth, height: nodeHeight });
        if (node.parentNode) {
            dagreGraph.setParent(node.id, node.parentNode);
        }
    });

    edges.forEach((edge) => {
        dagreGraph.setEdge(edge.source, edge.target);
    });

    dagre.layout(dagreGraph);

    nodes.forEach((node) => {

        const nodeWithPosition = dagreGraph.node(node.id);
        //console.log(node, nodeWithPosition)
        node.targetPosition = isHorizontal ? 'left' : 'top';
        node.sourcePosition = isHorizontal ? 'right' : 'bottom';

        // We are shifting the dagre node position (anchor=center center) to the top left
        // so it matches the React Flow node anchor point (top left).
        if (node.parentNode) {
            const parentNodeWithPosition = dagreGraph.node(node.parentNode)
            //console.log(parentNodeWithPosition)
            const width = node.type === "customGroup" || node.type === "deploymentNode" ? nodeWithPosition.width : nodeWithPosition.width;
            const height = node.type === "customGroup" || node.type === "deploymentNode" ? nodeWithPosition.height : nodeWithPosition.height;
            node.position = {
                x: nodeWithPosition.x - (parentNodeWithPosition.x - parentNodeWithPosition.width / 2) - width / 2,
                y: nodeWithPosition.y - (parentNodeWithPosition.y - parentNodeWithPosition.height / 2) - height / 2,
            }
        } else {
            node.position = {
                x: (nodeWithPosition.x - nodeWithPosition.width / 2),
                y: (nodeWithPosition.y - nodeWithPosition.height / 2),
            }
        }

        if (node.type === "customGroup" || node.type === "deploymentNode") {
            node.style.width = nodeWithPosition.width;
            node.style.height = nodeWithPosition.height;
        }


        return node;
    });

    return { nodes, edges };
}