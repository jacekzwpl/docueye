import type { RelationshipView, ViewConfiguration } from "../../../api/docueye-api";

export const getRelationshipStyle = (relationship: RelationshipView, viewConfiguration?: ViewConfiguration | null) => {
    const style: any = {
        thickness: undefined,
        color: "#b1b1b7",
        fontSize: 10,
        width: 200,
        dashed: false,
        routing: undefined,
        position: undefined,
        opacity: 100
    };

    relationship.tags?.forEach((tag) => {
        const relationshipStyle = viewConfiguration?.relationshipStyles?.find((obj) => {
            return obj.tag === tag;
        });
        if (relationshipStyle) {
            style.thickness = relationshipStyle.thickness ?? style.thickness;
            style.color = relationshipStyle.color ?? style.color;
            style.fontSize = relationshipStyle.fontSize ?? style.fontSize;
            style.width = relationshipStyle.width ?? style.width;
            //style.dashed =  relationshipStyle.dashed ?? style.dashed;
            //style.routing =  relationshipStyle.routing ?? style.routing;
            //style.position = relationshipStyle.position ?? style.position;
            style.opacity = relationshipStyle.opacity ?? style.opacity;
        }
    });
    return style;
}