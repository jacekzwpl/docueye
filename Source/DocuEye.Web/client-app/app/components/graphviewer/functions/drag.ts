import * as d3 from "d3";

export const drag = (simulation: any): any => {
    const dragstarted = (event: any, d: any) => {
        if (!event.active) simulation.alphaTarget(0.3).restart();
        d.fx = d.x;
        d.fy = d.y;
    };

    const dragged = (event: any, d: any) => {
        d.fx = event.x;
        d.fy = event.y;
    };

    const dragended = (event: any, d: any) => {
        if (!event.active) simulation.alphaTarget(0);
        d.fx = null;
        d.fy = null;
    };

    return d3
        .drag()
        .on("start", dragstarted)
        .on("drag", dragged)
        .on("end", dragended);
};