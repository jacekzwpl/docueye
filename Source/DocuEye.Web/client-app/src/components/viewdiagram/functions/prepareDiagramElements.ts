import { MarkerType } from "reactflow";
import { Element, ElementView, RelationshipView, ViewConfiguration } from "../../../api/docueye-api";
import { getTerminologyTerm } from "../../../terminology/getTerminologyTerm";
import { getElementStyle } from "./getElementStyle";
import { getLayoutedElements } from "./getLayoutedElements";
import { getParentGroup } from "./getParentGroup";
import { getRelationshipStyle } from "./getRelationshipStyle";

export const prepareDiagramElements = (
    elements: ElementView[],
    relations: RelationshipView[],
    viewConfiguration: ViewConfiguration | null | undefined,
    viewType: string,
    contextElement?: Element) => {
    const newNodes: any[] = [];
    const newEdges: any[] = [];
    const groupNodes: any[] = [];
    //const groups: any[] = [];

    elements.forEach(element => {
        const { groups, contextGroup, nearest } = getParentGroup(element, contextElement, viewType, "", viewConfiguration?.groupSeparator ?? "|");
        newNodes.push({
            id: element.id,
            position: { x: 0, y: 0 },
            data: {
                name: element.name,
                typeName: getTerminologyTerm(element.type, viewConfiguration?.terminology),
                description: element.description,
                diagramId: element.diagramId,
                url: element.url,
                technology: element.technology,
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
            data: {technology: relation.technology},
            labelWidth: relationStyle.width,
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

    const { nodes: layoutedNodes, edges: layoutedEdges } = getLayoutedElements(
        newNodes,
        newEdges
    );
    return { layoutedNodes: layoutedNodes, layoutedEdges: layoutedEdges }
}