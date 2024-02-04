import * as d3 from "d3";
import { drag } from "./drag";
import { nodeClick } from "./nodeClick";

export const createGraph = (container: any, nodesData: any, linksData: any) => {
    const containerRect = container.getBoundingClientRect();
    const height = containerRect.height;
    const width = containerRect.width;

    const links: any[] = linksData.map((d: any) => Object.assign({}, d));
    const nodes: any[] = nodesData.map((d: any) => Object.assign({}, d));

    const distance = Math.min(width, height) / 3;
    const simulation = d3
        .forceSimulation(nodes)
        .force("link", d3.forceLink(links).distance(distance).id((d: any) => d.id))
        .force("charge", d3.forceManyBody().strength(-distance))
    //.force("x", d3.forceX())
    //.force("y", d3.forceY());

    const svg = d3
        .select(container)
        .append("svg")
        .attr("viewBox", [-width / 2, -height / 2, width, height]);

    const arrowColors:any[] = [];
    links.forEach((link:any) => {
        const index = arrowColors.findIndex((arrowColor) => {return arrowColor.color === link.style.color})
        if(index === -1) {
            arrowColors.push({
                id: "arrow-" + link.style.color.replace("#", ""),
                color: link.style.color
            })
        }
    });

    svg.append("defs").selectAll("marker")
        .data(arrowColors)
        .join("marker")
        .attr("id", d => d.id)
        .attr("viewBox", "0 0 12 12")
        .attr("refX", 32)
        .attr("refY", 6)
        .attr("markerWidth", 12)
        .attr("markerHeight", 12)
        .attr("orient", "auto")
        .attr("fill", (d) => d.color)
        .append("path")
        .attr("d", "M0,0 L12,6 L0,12 L0,0");

    const link = svg
        .append("g")
        .selectAll("line")
        .data(links)
        .join("line")
        .attr("stroke", (d: any) => {
            return d.style.color;
        })
        .attr("marker-end", function (d) {
            return `url(#arrow-${d.style.color.replace("#","")})`;
        })
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
        .attr('cursor', 'pointer')
        .call(drag(simulation)).on('click', nodeClick)

    const label = svg.append("g")
        .attr("class", "labels")
        .selectAll("text")
        .data(nodes)
        .enter()
        .append("text")
        .attr('font-size', "11px")
        .text(d => { return d.data.name })
        .call(drag(simulation));

    const labelType = svg.append("g")
        .attr("class", "labels")
        .selectAll("text")
        .data(nodes)
        .enter()
        .append("text")
        .attr('font-size', "9px")
        .text(d => { return `[${d.data.type}]` })
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

        // update label positions
        label
            .attr("x", d => { return d.x + 20; })
            .attr("y", d => { return d.y; })

        labelType
            .attr("x", d => { return d.x + 20; })
            .attr("y", d => { return d.y + 10; })

    });
    return {
        destroy: () => {
            //simulation.stop();
            //console.log("cleaned up");
        },
        nodes: () => {
            return svg.node();
        }
    };
};