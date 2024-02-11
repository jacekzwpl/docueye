import { MarkerType } from "reactflow";
import { DynamicRelationshipView, ElementView, Element, ViewConfiguration } from "../../../api/docueye-api";
import { snackbarUtils } from "../../../snackbar/snackbarUtils";
import { getTerminologyTerm } from "../../../terminology/getTerminologyTerm";
import { getElementStyle } from "./getElementStyle";
import { getLayoutedElements } from "./getLayoutedElements";
import { getParentGroup } from "./getParentGroup";
import { getRelationshipStyle } from "./getRelationshipStyle";

export const prepareDynamicDiagramElements = (elements: ElementView[], relations: DynamicRelationshipView[], viewConfiguration?: ViewConfiguration | null, contextElement?: Element) => {
    const newNodes: any[] = [];
    const newEdges: any[] = [];
    const groupNodes: any[] = [];

    elements.forEach(element => {
        const { groups, contextGroup, nearest } = getParentGroup(element, contextElement, "DynamicView", "", viewConfiguration?.groupSeparator ?? "|");
        newNodes.push({
            id: element.id,
            position: { x: 0, y: 0 },
            data: {
                name: element.name,
                typeName: getTerminologyTerm(element.type, viewConfiguration?.terminology),
                description: element.description,
                style: getElementStyle(element, viewConfiguration)
            },
            type: 'custom',
            parentNode: nearest !== null ? nearest.id : null,
            extent: nearest !== null ? 'parent' : undefined
        });
        if (contextGroup) {
            const exitingGroup = groupNodes.find((obj) => { return obj.id === contextGroup.id });
            if (!exitingGroup) {
                groupNodes.push(contextGroup);
            }
        }
        groups.forEach((group) => {
            const exitingGroup = groupNodes.find((obj) => { return obj.id === group.id });
            if (!exitingGroup) {
                groupNodes.push(group);
            }
        })
    });
    groupNodes.reverse().forEach((group) => {
        newNodes.unshift(group);
    });

    relations.forEach(relation => {
        const relationStyle = getRelationshipStyle(relation, viewConfiguration);
        newEdges.push({
            id: relation.id,
            source: relation.sourceId,
            target: relation.destinationId,
            label: relation.description,
            labelWidth: relationStyle.width,
            type: 'bidirectional',
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
        newNodes,
        newEdges
    );
    return { layoutedNodes: layoutedNodes, layoutedEdges: layoutedEdges }
}