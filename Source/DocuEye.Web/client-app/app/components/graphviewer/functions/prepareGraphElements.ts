import type { ElementView, RelationshipView, ViewConfiguration } from "../../../api/docueye-api";
import { snackbarUtils } from "../../../snackbar/snackbarUtils";
import { getTerminologyTerm } from "../../../terminology/getTerminologyTerm";
import { getElementStyle } from "../../viewdiagram/functions/getElementStyle";
import { getRelationshipStyle } from "../../viewdiagram/functions/getRelationshipStyle";

export const prepareGraphElements = (elementViews: ElementView[], relationshipViews: RelationshipView[], viewConfiguration?: ViewConfiguration | null) => {
    const links: any[] = [];
    const elements: any[] = [];
    elementViews.forEach((element) => {
        element.type = getTerminologyTerm(element.type, viewConfiguration?.terminology);
        elements.push({
            id: element.id,
            data: element,
            style: getElementStyle(element, viewConfiguration)
        })
    })
    relationshipViews.forEach((relationship) => {
        links.push({
            source: relationship.sourceId,
            target: relationship.destinationId,
            style: getRelationshipStyle(relationship, viewConfiguration)
        })
    })
    if(elements.length === 0) {
        snackbarUtils.info("This graph is empty.");
    }
    return {elements, links}
}