import * as d3 from "d3";
import { drag } from "./drag";

export const createGraph = (container: any, nodesData: any, linksData: any) => {
    const containerRect = container.getBoundingClientRect();
    const height = containerRect.height;
    const width = containerRect.width;

    const links: any[] = linksData.map((d: any) => Object.assign({}, d));
    const nodes: any[] = nodesData.map((d: any) => Object.assign({}, d));

    const simulation = d3
        .forceSimulation(nodes)
        .force("link", d3.forceLink(links).id((d: any) => d.id))
        .force("charge", d3.forceManyBody().strength(-150))
        .force("x", d3.forceX())
        .force("y", d3.forceY());

    const svg = d3
        .select(container)
        .append("svg")
        .attr("viewBox", [-width / 2, -height / 2, width, height]);

    const link = svg
        .append("g")
        .attr("stroke", "#999")
        .attr("stroke-opacity", 0.6)
        .selectAll("line")
        .data(links)
        .join("line")
        .attr("stroke-width", d => Math.sqrt(d.value));

    const node = svg
        .append("g")
        .attr("stroke", "#fff")
        .attr("stroke-width", 2)
        .selectAll("circle")
        .data(nodes)
        .join("circle")
        .attr("r", 12)
        .attr("fill", "#FF0000")
        .call(drag(simulation));

    simulation.on("tick", () => {
        //update link positions
        link
            .attr("x1", d => d.source.x)
            .attr("y1", d => d.source.y)
            .attr("x2", d => d.target.x)
            .attr("y2", d => d.target.y);

        // update node positions
        node
            .attr("cx", d => d.x)
            .attr("cy", d => d.y);

    });
    return {
        destroy: () => {
            //console.log(simulation);
            simulation.stop();
            //console.log("cleaned up");
        },
        nodes: () => {
            return svg.node();
        }
    };
};