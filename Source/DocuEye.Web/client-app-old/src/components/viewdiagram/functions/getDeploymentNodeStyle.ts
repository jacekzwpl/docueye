import { ElementView, ViewConfiguration } from "../../../api/docueye-api";

export const getDeploymentNodeStyle = (element: ElementView, viewConfiguration?: ViewConfiguration | null) => {
    const style: any = {
        background: undefined,
        color: '#000000',
        stroke: null,
        strokeWidth: 1,
        border: 'solid',
        fontSize: 11,
        metadata: true,
        description: false,
        opacity: 100,
        icon: null
    };

    element.tags?.forEach((tag) => {
        const elementStyle = viewConfiguration?.elementStyles?.find((obj) => {
            return obj.tag === tag;
        });
        if (elementStyle) {
            style.background = elementStyle.background ?? style.background;
            style.color = elementStyle.color ?? style.color;
            style.shape = elementStyle.shape ?? style.shape;
            style.stroke = elementStyle.stroke ?? style.stroke;
            style.strokeWidth = elementStyle.strokeWidth ?? style.strokeWidth;
            style.border = elementStyle.border ?? style.border;
            style.fontSize = elementStyle.fontSize ?? style.fontSize;
            style.metadata = elementStyle.metadata ?? style.metadata;
            style.description = elementStyle.description ?? style.description;
            style.opacity = elementStyle.opacity ?? style.opacity;
            style.icon = elementStyle.icon ?? style.icon;
        }
    });
    return style;
}