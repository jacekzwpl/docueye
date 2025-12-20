import { MarkerType } from "reactflow";
import type { AutomaticLayout, Element, ElementView, RelationshipView, ViewConfiguration } from "../../../api/docueye-api";
import { snackbarUtils } from "../../../snackbar/snackbarUtils";
import { getTerminologyTerm } from "../../../terminology/getTerminologyTerm";
import { drawDeploymentNode } from "./drawDeploymentNode";
import { getElementStyle } from "./getElementStyle";
import { getLayoutedElements } from "./getLayoutedElements";
import { getParentGroup } from "./getParentGroup";
import { getRelationshipStyle } from "./getRelationshipStyle";
import { orderDeploymentDiagramNodes } from "./orderDeploymentDiagramNodes";

export const prepareDeploymentDiagramElements = (elements: ElementView[], relations: RelationshipView[], viewConfiguration?: ViewConfiguration | null, contextElement?: Element, autolayout?: AutomaticLayout) => {
    let newNodes: any[] = [];
    let newGroups: any[] = [];
    let instancesGroups: any[] = [];
    const newEdges: any[] = [];
    let orderedNodes: any[] = [];

    const parentNodes = elements.filter((element) => !element.parentId && element.type === "DeploymentNode");
    parentNodes.forEach((parentNode) => {
        const { foundedNodes, foundedGroups } = drawDeploymentNode(parentNode, null, elements, viewConfiguration);
        newNodes = newNodes.concat(foundedNodes);
        newGroups = newGroups.concat(foundedGroups);
    });
    const instances = elements.filter((element) => element.type === "ContainerInstance" || element.type === "SoftwareSystemInstance" || element.type === "InfrastructureNode");
    instances.forEach((element) => {
        const { groups, nearest } = getParentGroup(element, null, "DeploymentView", element.parentId ? element.parentId : "", viewConfiguration?.groupSeparator ?? "|");
        newNodes.push({
            id: element.id,
            position: { x: 0, y: 0 },
            data: {
                name: element.name,
                instanceOfId: element.instanceOfId,
                typeName: getTerminologyTerm(element.type, viewConfiguration?.terminology),
                description: element.description,
                diagramId: element.diagramId,
                url: element.url,
                technology: element.technology,
                style: getElementStyle(element, viewConfiguration)
            },
            type: 'custom',
            parentNode: nearest === null ? element.parentId : nearest.id,
            extent: nearest !== null || element.parentId ? 'parent' : undefined
        });
        instancesGroups = instancesGroups.concat(groups);
    });

    //Add instances groups
    instancesGroups.forEach((group) => {
        const exitingGroup = newNodes.find((obj) => { return obj.id === group.id });
        if (!exitingGroup) {
            newNodes.push(group);
        }
    });
    // Add deployment nodes group
    newGroups.forEach((group) => {
        const exitingGroup = newNodes.find((obj) => { return obj.id === group.id });
        if (!exitingGroup) {
            newNodes.push(group);
        }
    });

    //Order Elements
    orderedNodes = orderDeploymentDiagramNodes(newNodes, null);


    relations.forEach(relation => {
        let isLayoutedEdge = true;
        const sourceNode = elements.find((obj) => { return obj.id === relation.sourceId });
        const destNode = elements.find((obj) => { return obj.id === relation.destinationId });

        // Hack until will be fix https://github.com/dagrejs/dagre/pull/429
        // Exclude relationships for deployment nodes from layout
        if (sourceNode?.type === "DeploymentNode" || destNode?.type === "DeploymentNode") {
            isLayoutedEdge = false;
        }

        const relationStyle = getRelationshipStyle(relation, viewConfiguration);
        newEdges.push({
            id: relation.id,
            source: relation.sourceId,
            target: relation.destinationId,
            label: relation.description,
            labelWidth: relationStyle.width,
            data: {technology: relation.technology},
            isLayoutedEdge: isLayoutedEdge,
            type: 'floating', //relation.type, //'floating', //'floating',//'default',
            markerEnd: {
                type: MarkerType.ArrowClosed,
                width: 20,
                height: 20,
                color: relationStyle.color
            },
            style: {
                strokeWidth: relationStyle.thickness,
                stroke: relationStyle.color,
                fontSize: relationStyle.fontSize,
                opacity: relationStyle.opacity / 100
            }
        });

    });
    if(newNodes.length === 0) {
        snackbarUtils.info("This diagram is empty.");
        return { layoutedNodes: [], layoutedEdges: [] }
    }
    const { nodes: layoutedNodes, edges: layoutedEdges } = getLayoutedElements(
        orderedNodes,
        newEdges.filter((obj) => { return obj.isLayoutedEdge === true }),
        autolayout?.rankDirection ?? "TopBottom"
    );
    return {
        layoutedNodes: layoutedNodes,
        layoutedEdges: layoutedEdges.concat(
            newEdges.filter((obj) => { return obj.isLayoutedEdge === false })
        )
    }
}