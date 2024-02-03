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
        .force("charge", d3.forceManyBody().strength(-1650))
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
        .selectAll("circle")
        .data(nodes)
        .join("circle")
        .attr("stroke", (d: any) => {
            return d.style.stroke;
        })
        .attr("stroke-width", (d: any) => {
            return d.style.strokeWidth;
        })
        .attr("r", 20)
        .attr("fill", (d: any) => {
            return d.style.background;
        })
        .call(drag(simulation));

    //const label = svg.append("g").selectAll("text")
    
        //node//.append("g")
        //.selectAll("text")
        //.data(nodes)
        //.enter()
        //.append("text")
        //.attr("cy", function(d) {
        //    return 0;
        //})
        //.attr("cx", function(d) {
        //    return 25;
        //})
        //.attr('text-anchor', 'middle')
        //.attr('dominant-baseline', 'central')
        ///.text(d => { return "Ala ma kota" })
        //.call(drag(simulation));

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

        // update label positions
        //label
        //    .attr("x", d => { return d.x + 80; })
        //    .attr("y", d => { return d.y; })

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