export const orderDeploymentDiagramNodes = (nodes: any[], parentId: string | null) => {
    let orderedNodes: any[] = [];
    nodes.filter((obj) => { return obj.parentNode === parentId })
        .forEach((node) => {
            orderedNodes.push(node);
            let childNodes: any[] = orderDeploymentDiagramNodes(
                nodes,
                node.id
            );
            orderedNodes = orderedNodes.concat(childNodes);
        });
    return orderedNodes;
}