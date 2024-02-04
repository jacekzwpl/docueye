import * as d3 from "d3";

export const nodeClick = (node: any) => {

  const contextNodeId = node.target.__data__.id;

  const lines = d3.selectAll("line");
  const circles = d3.selectAll("circle");


  //Find previously selected node
  const selected: any = circles.nodes().find((circle: any) => {
    return circle.attributes['data-selected'] === "1"
  });
  let isResetView: boolean = false;
  //If context node was last context node
  if (selected && selected.__data__.id === contextNodeId) {
    node.target.attributes['data-selected'] = "0";
    isResetView = true;
  } else if(selected && selected.__data__.id === contextNodeId) {
    selected.attributes["data-selected"] = "0";
    node.target.attributes['data-selected'] = "1";
  }else {
    node.target.attributes['data-selected'] = "1";
  }


  if (!isResetView) {
    //Find lines that are connected to context node
    const connectedLines = lines.filter((line: any) => {
      return line.source.id === contextNodeId ||
        line.target.id === contextNodeId
    });

    //Find lines that ar not connected to context node
    const notConnectedLines = lines.filter((line: any) => {
      return line.source.id !== contextNodeId &&
        line.target.id !== contextNodeId
    });


    //Find circles that are not connected to context node
    const notConnectedCircles = circles.filter((circle: any) => {
      //check if circle is source or target of connected lines
      const foundedIndex = connectedLines.nodes().findIndex((line: any) => {
        return line.__data__.source.id === circle.data.id ||
          line.__data__.target.id === circle.data.id
      });
      return foundedIndex === -1 ? true : false;
    });

    //Find all circles that are connected to context node
    const connectedCircles = circles.filter((circle: any) => {
      const foundedIndex = connectedLines.nodes().findIndex((line: any) => {
        return line.__data__.source.id === circle.data.id ||
          line.__data__.target.id === circle.data.id || circle.data.id === contextNodeId
      });
      return foundedIndex >= 0 ? true : false;
    });

    notConnectedCircles.style('opacity', 0.1);
    notConnectedLines.style('opacity', 0.1);
    connectedCircles.style('opacity', 1);
    connectedLines.style('opacity', 1);
  } else {
    lines.style('opacity', 1);
    circles.style('opacity', 1);
  }


}