import type { Element } from "../../../api/docueye-api";

export const getParentGroup = (
    element: Element,
    contextElement: Element | null | undefined,
    viewType: string,
    groupParentId: string = "",
    groupSeparator: string = "|"
) => {

    //For ContainerView show groups only for containers
    if (viewType === "ContainerView" && element.type !== "Container") {
        return { groups: [], contextGroup: null, nearest: null };
    }
    //For ComponentView show groups only for Component
    if (viewType === "ComponentView" && element.type !== "Component") {
        return { groups: [], contextGroup: null, nearest: null };
    }

    let contextGroup: any = null;
    if (contextElement && element.parentId && contextElement.id === element.parentId) {
        contextGroup = {
            id: contextElement.id,
            data: { label: contextElement.name },
            position: { x: 0, y: 0 },
            type: 'customGroup',
            style: {
                borderStyle: 'dashed',
                borderWidth: 1,
                borderColor: '#03a1fc',
                color: '#03a1fc',
                width: 200,
                height: 200
            }
        }
    }

    let discoverGroups: boolean = true;

    //For DynamicView exclude groups for elements that are out of diagram context
    if (viewType === "DynamicView" && contextGroup === null) {
        discoverGroups = false;
    }

    const groupNodes: any[] = [];
    if (element.group && discoverGroups) {
        const groupNames = element.group.split(groupSeparator);

        groupNames.forEach((groupName, index) => {
            let parentNodeId: any = groupParentId === "" ? null : groupParentId;
            if (contextGroup && index === 0) {
                parentNodeId = contextGroup.id;
            } else if (index > 0) {
                parentNodeId = `${groupParentId}${(index - 1)}${groupNames[index - 1]}`;
            }

            groupNodes.push({
                id: `${groupParentId}${index}${groupName}`,
                data: { label: groupName },
                position: { x: 0, y: 0 },
                type: 'customGroup',
                style: {
                    borderStyle: 'dashed',
                    borderWidth: 1,
                    width: 200,
                    height: 200
                },
                parentId: parentNodeId,
                extent: parentNodeId ? "parent" : undefined
            })
        })
    }

    const nearest = groupNodes.length > 0 ? groupNodes[groupNodes.length - 1] : contextGroup;

    return { groups: groupNodes, contextGroup: contextGroup, nearest: nearest };
}