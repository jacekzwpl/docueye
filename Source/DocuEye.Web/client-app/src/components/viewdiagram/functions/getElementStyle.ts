import { ElementStyle, ElementView, ViewConfiguration } from "../../../api/docueye-api";

export const getElementStyle = (element: ElementView, viewConfiguration?: ViewConfiguration | null) => {
    const style: any = {
        background: '#999999',
        color: '#000000',
        shape: null,
        stroke: null,
        strokeWidth: null,
        border: 'none',
        fontSize: 11,
        metadata: true,
        description: true,
        opacity: 100,
        icon: null
    };

    element.tags?.forEach((tag) => {
        const elementStyles: ElementStyle[] | undefined = viewConfiguration?.elementStyles?.filter((obj) => {
            return obj.tag === tag;
        });
        elementStyles?.forEach((elementStyle) => {

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
        })

    });
    return style;
}