import type { ElementView, ViewConfiguration } from "../../../api/docueye-api";
import { getDeploymentNodeStyle } from "./getDeploymentNodeStyle";
import { getParentGroup } from "./getParentGroup";

export const drawDeploymentNode = (
    element: ElementView,
    parentElement: ElementView | null,
    all: ElementView[],
    viewConfiguration?: ViewConfiguration | null): { foundedNodes: any[], foundedGroups: any[] } => {

    let newNodes: any[] = [];
    let newGroups: any[] = [];
    const style = getDeploymentNodeStyle(element, viewConfiguration);
    const { groups, nearest } = getParentGroup(element, null, "DeploymentView", parentElement?.id ? parentElement.id : "", viewConfiguration?.groupSeparator ?? "|");
    newGroups = newGroups.concat(groups);
    newNodes.push({
        id: element.id,
        data: {
            name: element.name,
            technology: element.technology,
            style: style
        },
        position: { x: 0, y: 0 },
        type: 'deploymentNode',
        style: {
            background: style.background,
            borderStyle: style.border,
            borderWidth: style.strokeWidth ?? undefined,
            borderColor: style.stroke ?? undefined,
            width: 200,
            height: 200,
            opacity: style.opacity / 100
        },
        parentNode: nearest === null ? element.parentId : nearest.id,
        extent: nearest !== null || element.parentId ? 'parent' : undefined
    });

    const children = all.filter((node) => node.parentId === element.id && node.type === "DeploymentNode");
    children.forEach(child => {
        const { foundedNodes, foundedGroups } = drawDeploymentNode(child, element, all, viewConfiguration);
        newNodes = newNodes.concat(foundedNodes);
        newGroups = newGroups.concat(foundedGroups);
    });

    return { foundedNodes: newNodes, foundedGroups: newGroups };
}