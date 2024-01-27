//import { useEffect } from "react";

//declare var $: any;
//declare var structurizr: any;

export const Graph = () => {

/*
  //let diagram: any;
  const relationshipsBySourceAndDestination:any[] = [];

  useEffect(() => {
    $.ajax({url: 'workspace.json',
    success: function(data:any){
        console.log(data);
        //JSON.parse(data);
        structurizr.workspace = new structurizr.Workspace(data);

        structurizr.ui.loadThemes(() => {
            init();
        });

        
         //setTimeout( () => { 
          
         //}, 5000 );

    }});

  }, []);

  const init = () =>{

    const graph:any = { nodes: [], links: []};
    const elements:any[] = [];
    const elementIds:any[] = [];

    const view = structurizr.workspace.findViewByKey("Containers");

    view.elements.forEach(function(elementView:any) {
        const element = structurizr.workspace.findElementById(elementView.id);
        if (element) {
            elements.push(element);
            elementIds.push(element.id);
        }
    });

    elements.sort(function(a, b) {
        return a.name.localeCompare(b.name);
    });

    elements.forEach(function(element) {
        const elementStyle = structurizr.ui.findElementStyle(element, false);
        graph.nodes.push({
            id: element.id,
            name: element.name,
            description: element.description,
            style: elementStyle,
            type: element.type,
            tags: element.tags,
            element: element
        });

        registerRelationship(element.id, element.id); // this prevents the nodes in the graph linking to themselves

        const elementType = structurizr.workspace.getTerminologyFor(element);
        const html = '<span class="label smaller" style="background: ' + elementStyle.background + '; color: ' + elementStyle.color + '"> ' + structurizr.util.escapeHtml(elementType) + '</span> ' + structurizr.util.escapeHtml(element.name);
        //quickNavigation.addHandler(html, function() {
        //    highlightNode(element.id);
       // });

        if (element.relationships) {
            element.relationships.forEach(function(relationship:any) {
                if (elementIds.indexOf(relationship.sourceId) > -1 && elementIds.indexOf(relationship.destinationId) > -1) {
                    registerRelationship(relationship.sourceId, relationship.destinationId);

                    graph.links.push({
                        id: relationship.id,
                        source: relationship.sourceId,
                        target: relationship.destinationId,
                        type: 'Relationship',
                        description: relationship.description,
                        style: structurizr.ui.findRelationshipStyle(relationship),
                        relationship: relationship
                    });
                }
            });
        }
    });

  }

  const registerRelationship = (sourceId:any, destinationId:any) => {
    const key = sourceId + "," + destinationId;
    relationshipsBySourceAndDestination.push(key);
  }
*/
    return (
        <div id="diagram" style={{width: 1000, height: 600}}></div>
    );
};