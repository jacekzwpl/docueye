import { forwardRef, useCallback, useEffect, useImperativeHandle, useState } from "react";
import { ReactFlow, Background, Controls, useEdgesState, useNodesState, useReactFlow } from "@xyflow/react";
import DocuEyeApi from "../../api";
import type { IViewDiagramProps } from './IViewDiagramProps';
import { nodeTypes } from "./nodes";
import '@xyflow/react/dist/style.css';
import './edges/floatingedges.css'
import { edgeTypes } from "./edges";
import type { AxiosResponse } from "axios";
import type { ComponentViewDiagram, ContainerViewDiagram, DeploymentViewDiagram, DynamicViewDiagram, Element, FilteredViewDiagram, ImageView, SystemContextViewDiagram, SystemLandscapeViewDiagram, ViewConfiguration } from "../../api/docueye-api";
import Loader from "../loader";
import { prepareDiagramElements } from "./functions/prepareDiagramElements";
import { prepareDynamicDiagramElements } from "./functions/prepareDynamicDiagramElements";
import { prepareDeploymentDiagramElements } from "./functions/prepareDeploymentDiagramElements";
import ImageViewer from "./ImageViewer";
import { snackbarUtils } from "~/snackbar/snackbarUtils";
import mermaid from "mermaid";

 const diagram = `graph TD
        A[Client] --> B[Load Balancer]
        B --> C[Server01]
        B --> D[Server02]
    `;
 const diagram2 = `graph TD
        A1[Client] --> B1[Load Balancer]
        B1 --> C1[Server01]
        B1 --> D1[Server02]
    `;

const ViewDiagram = forwardRef((props: IViewDiagramProps, ref) => {
    const [selectedView, setSelectedView] = useState(props.selectedView);
    const [workspaceId, setWorkspaceId] = useState(props.workspaceId);
    const [viewConfiguration, setViewConfiguration] = useState(props.viewConfiguration);
    const [nodes, setNodes, onNodesChange] = useNodesState<any>([]);
    const [edges, setEdges, onEdgesChange] = useEdgesState<any>([]);
    const { setViewport } = useReactFlow();
    const [currentImageView, setCurrentImageView] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [rfInstance, setRfInstance] = useState<any>(null);
    const [diagramEngine, setDiagramEngine] = useState<"reactflow" | "mermaid">("reactflow");
    const [mermaidDiagram, setMermaidDiagram] = useState<string>("");

    const loadSystemLandscapeView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsSystemlandscapeIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<SystemLandscapeViewDiagram>) => {
                if (response.data.layoutData) {
                    loadSavedLayout(response.data.layoutData);
                } else if (response.data.elements && response.data.relationships) {
                    const { layoutedNodes, layoutedEdges } = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "", undefined, response.data.automaticLayout);
                    setNodes(layoutedNodes);
                    setEdges(layoutedEdges);
                }

            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setEdges, setNodes]);

    const loadSystemContextView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsSystemcontextIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<SystemContextViewDiagram>) => {
                if (response.data.layoutData) {
                    loadSavedLayout(response.data.layoutData);
                } else if (response.data.elements && response.data.relationships) {
                    const { layoutedNodes, layoutedEdges } = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "", undefined, response.data.automaticLayout);
                    setNodes(layoutedNodes);
                    setEdges(layoutedEdges);
                }
            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setEdges, setNodes]);

    const loadContainerView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsContainerIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<ContainerViewDiagram>) => {
                if (response.data.softwareSystemId) {
                    DocuEyeApi.ElementsApi
                        .apiWorkspacesWorkspaceIdElementsIdGet(workspaceId, response.data.softwareSystemId)
                        .then((elResponse: AxiosResponse<Element>) => {
                            if (response.data.layoutData) {
                                loadSavedLayout(response.data.layoutData);
                            } else if (response.data.elements && response.data.relationships) {
                                const { layoutedNodes, layoutedEdges } = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ContainerView", elResponse.data, response.data.automaticLayout);
                                setNodes(layoutedNodes);
                                setEdges(layoutedEdges);
                            }
                        });

                } else {
                    if (response.data.layoutData) {
                        loadSavedLayout(response.data.layoutData);
                    } else if (response.data.elements && response.data.relationships) {
                        const { layoutedNodes, layoutedEdges } = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ContainerView", undefined, response.data.automaticLayout);
                        setNodes(layoutedNodes);
                        setEdges(layoutedEdges);
                    }
                }

            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setEdges, setNodes]);

    const loadComponentView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsComponentIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<ComponentViewDiagram>) => {
                if (response.data.containerId) {
                    DocuEyeApi.ElementsApi
                        .apiWorkspacesWorkspaceIdElementsIdGet(workspaceId, response.data.containerId)
                        .then((elResponse: AxiosResponse<Element>) => {
                            if (response.data.layoutData) {
                                loadSavedLayout(response.data.layoutData);
                            } else if (response.data.elements && response.data.relationships) {
                                const { layoutedNodes, layoutedEdges } = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ComponentView", elResponse.data, response.data.automaticLayout);
                                setNodes(layoutedNodes);
                                setEdges(layoutedEdges);
                            }
                        });
                } else {
                    if (response.data.layoutData) {
                        loadSavedLayout(response.data.layoutData);
                    } else if (response.data.elements && response.data.relationships) {
                        const { layoutedNodes, layoutedEdges } = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ComponentView", undefined, response.data.automaticLayout);
                        setNodes(layoutedNodes);
                        setEdges(layoutedEdges);
                    }
                }

            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setEdges, setNodes]);

    const loadFilteredView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsFilteredIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<FilteredViewDiagram>) => {
                if (response.data.layoutData) {
                    loadSavedLayout(response.data.layoutData);
                } else if (response.data.elements && response.data.relationships) {
                    const { layoutedNodes, layoutedEdges } = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ComponentView", undefined, response.data.automaticLayout);
                    setNodes(layoutedNodes);
                    setEdges(layoutedEdges);
                }
            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setEdges, setNodes]);

    const loadDeploymentView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsDeploymentIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<DeploymentViewDiagram>) => {
                if (response.data.layoutData) {
                    loadSavedLayout(response.data.layoutData);
                } else if (response.data.elements && response.data.relationships) {
                    const { layoutedNodes, layoutedEdges } = prepareDeploymentDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, undefined, response.data.automaticLayout);
                    setNodes(layoutedNodes);
                    setEdges(layoutedEdges);

                }

            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setNodes, setEdges]);

    const loadDynamicView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsDynamicIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<DynamicViewDiagram>) => {
                if (response.data.elementId) {
                    DocuEyeApi.ElementsApi
                        .apiWorkspacesWorkspaceIdElementsIdGet(workspaceId, response.data.elementId)
                        .then((elResponse: AxiosResponse<Element>) => {
                            if (response.data.layoutData) {
                                loadSavedLayout(response.data.layoutData);
                            } else if (response.data.elements && response.data.relationships) {
                                const { layoutedNodes, layoutedEdges } = prepareDynamicDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, elResponse.data, response.data.automaticLayout);
                                setNodes(layoutedNodes);
                                setEdges(layoutedEdges);
                            }
                        });
                } else {
                    if (response.data.layoutData) {
                        loadSavedLayout(response.data.layoutData);
                    } else if (response.data.elements && response.data.relationships) {
                        const { layoutedNodes, layoutedEdges } = prepareDynamicDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, undefined, response.data.automaticLayout);
                        setNodes(layoutedNodes);
                        setEdges(layoutedEdges);
                    }
                }

            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setNodes, setEdges]);


    const loadImageView = useCallback((workspaceId: string, viewId: string) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsImageIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<ImageView>) => {
                if (response.data.content) {
                    setCurrentImageView(response.data.content);
                }
            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setCurrentImageView]);

    useEffect(() => {
        

        if (!selectedView || selectedView?.id !== props.selectedView?.id) {
            setSelectedView(props.selectedView);
            setWorkspaceId(props.workspaceId);
            setViewConfiguration(props.viewConfiguration);
        }
    }, [props, selectedView])

    useEffect(() => {
        mermaid.initialize({ startOnLoad: false });
        console.log("Diagram engine changed to", diagramEngine);
        if (diagramEngine === "mermaid" && mermaidDiagram) {
            const element = window.document.getElementById("test-mermaid")!;
            if(element.attributes.getNamedItem("data-processed") !== null) {
                element.attributes.removeNamedItem("data-processed");
            }
            setTimeout(async () => {
                console.log("Rendering mermaid diagram", mermaidDiagram);
                await mermaid.run({
                    nodes: [element]
                });
            }, 500);

        }
    }, [mermaidDiagram])

    useEffect(() => {
        setCurrentImageView(null);
        
        if (selectedView && workspaceId) {
            if (selectedView.viewType === "SystemLandscapeView") {
                setDiagramEngine("reactflow");
                loadSystemLandscapeView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "SystemContextView") {
                setDiagramEngine("mermaid");
                setMermaidDiagram(diagram2);

                loadSystemContextView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "ContainerView") {
                setDiagramEngine("reactflow");
                loadContainerView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "ComponentView") {
                setDiagramEngine("reactflow");
                loadComponentView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "FilteredView") {
                setDiagramEngine("reactflow");
                loadFilteredView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "DeploymentView") {
                setDiagramEngine("reactflow");
                loadDeploymentView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "DynamicView") {
                setDiagramEngine("mermaid");
                setMermaidDiagram(diagram);

                loadDynamicView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "ImageView") {
                loadImageView(workspaceId, selectedView.id);
            }
        }
    }, [
        selectedView, workspaceId, viewConfiguration,
        loadSystemLandscapeView,
        loadSystemContextView,
        loadContainerView,
        loadComponentView,
        loadFilteredView,
        loadDeploymentView,
        loadDynamicView,
        loadImageView,
        setCurrentImageView,
        setDiagramEngine,
        setMermaidDiagram
    ]);

    const loadSavedLayout = (layoutData: string) => {
        const layout = JSON.parse(layoutData);
        if (layout) {
            const { x = 0, y = 0, zoom = 1 } = layout.viewport;
            setNodes(layout.nodes || []);
            setEdges(layout.edges || []);
            setViewport({ x, y, zoom });
        }
    };


    useImperativeHandle(ref, () => ({



        saveLayout: () => {
            const flow = rfInstance.toObject();
            const layout = JSON.stringify(flow);
            setIsLoading(true);
            DocuEyeApi.ViewsApi.apiWorkspacesWorkspaceIdViewsLayoutIdPost(
                workspaceId!,
                selectedView!.id,
                {
                    layoutData: layout
                },
            ).then(() => {
                snackbarUtils.success("Layout saved");
            }).finally(() => {
                setIsLoading(false);
            });


        }
    }));


   
    return (
        <>
            {diagramEngine === "mermaid" && <pre id="test-mermaid">
                {mermaidDiagram}
            </pre>}
            {currentImageView !== null && <ImageViewer image={currentImageView} />}
            {currentImageView === null && diagramEngine === "reactflow" &&
                <ReactFlow
                    nodes={nodes}
                    edges={edges}
                    onNodesChange={onNodesChange}
                    onEdgesChange={onEdgesChange}
                    nodeTypes={nodeTypes}
                    edgeTypes={edgeTypes}
                    onInit={setRfInstance}
                    fitView
                >
                    <Background />
                    <Controls />
                </ReactFlow>}
            {isLoading && <Loader />}
        </>)
});

export default ViewDiagram;

