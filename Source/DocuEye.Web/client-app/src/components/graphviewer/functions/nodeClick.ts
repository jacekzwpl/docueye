import * as d3 from "d3";

export const nodeClick = (node:any) => {
    console.log(node, node.target.__data__);

    const lines = d3.selectAll("line")//.filter((d:any) => {return d.data.source === node.target.id})
    console.log(lines);
    //d3.selectAll("g")
  //.filter(function(d) { return d.data.uniqueID === myDatum.data.uniqueID; });
}