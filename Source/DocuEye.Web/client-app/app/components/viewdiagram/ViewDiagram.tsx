import { useCallback, useEffect, useState } from "react";
import  {ReactFlow, Background, Controls, useEdgesState, useNodesState } from "@xyflow/react";
import DocuEyeApi from "../../api";
import type { IViewDiagramProps } from './IViewDiagramProps';
import { nodeTypes } from "./nodes";
//import 'reactflow/dist/style.css';
import '@xyflow/react/dist/style.css';
import './edges/floatingedges.css'
import { edgeTypes } from "./edges";
import type { AxiosResponse } from "axios";
import type { ComponentView, ContainerView, DeploymentView, DynamicView, Element, FilteredView, ImageView, SystemContextView, SystemLandscapeView, ViewConfiguration } from "../../api/docueye-api";
import Loader from "../loader";
import { prepareDiagramElements } from "./functions/prepareDiagramElements";
import { prepareDynamicDiagramElements } from "./functions/prepareDynamicDiagramElements";
import { prepareDeploymentDiagramElements } from "./functions/prepareDeploymentDiagramElements";
import ImageViewer from "./ImageViewer";

const ViewDiagram = (props: IViewDiagramProps) => {
    const [selectedView, setSelectedView] = useState(props.selectedView);
    const [workspaceId, setWorkspaceId] = useState(props.workspaceId);
    const [viewConfiguration, setViewConfiguration] = useState(props.viewConfiguration);
    const [nodes, setNodes, onNodesChange] = useNodesState<any>([]);
    const [edges, setEdges, onEdgesChange] = useEdgesState<any>([]);
    const [currentImageView, setCurrentImageView] = useState<string | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const loadSystemLandscapeView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsSystemlandscapeIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<SystemLandscapeView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {layoutedNodes, layoutedEdges} = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "", undefined, response.data.automaticLayout);
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
            .then((response: AxiosResponse<SystemContextView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {layoutedNodes, layoutedEdges} = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "", undefined, response.data.automaticLayout);
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
            .then((response: AxiosResponse<ContainerView>) => {
                if (response.data.softwareSystemId) {
                    DocuEyeApi.ElementsApi
                        .apiWorkspacesWorkspaceIdElementsIdGet(workspaceId, response.data.softwareSystemId)
                        .then((elResponse: AxiosResponse<Element>) => {
                            if (response.data.elements && response.data.relationships) {
                                const {layoutedNodes, layoutedEdges} = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ContainerView", elResponse.data, response.data.automaticLayout);
                                console.log("layoutedNodes", layoutedNodes);
                                setNodes(layoutedNodes);
                                console.log("nodes", nodes);
                                setEdges(layoutedEdges);
                            }
                        });

                } else {
                    if (response.data.elements && response.data.relationships) {
                        const {layoutedNodes, layoutedEdges} = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ContainerView", undefined, response.data.automaticLayout);
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
            .then((response: AxiosResponse<ComponentView>) => {
                if (response.data.containerId) {
                    DocuEyeApi.ElementsApi
                        .apiWorkspacesWorkspaceIdElementsIdGet(workspaceId, response.data.containerId)
                        .then((elResponse: AxiosResponse<Element>) => {
                            if (response.data.elements && response.data.relationships) {
                                const {layoutedNodes, layoutedEdges} = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ComponentView", elResponse.data, response.data.automaticLayout);
                                setNodes(layoutedNodes);
                                setEdges(layoutedEdges);
                            }
                        });
                } else {
                    if (response.data.elements && response.data.relationships) {
                        const {layoutedNodes, layoutedEdges} = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ComponentView", undefined, response.data.automaticLayout);
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
            .then((response: AxiosResponse<FilteredView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {layoutedNodes, layoutedEdges} = prepareDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, "ComponentView", undefined, response.data.automaticLayout);
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
            .then((response: AxiosResponse<DeploymentView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {layoutedNodes, layoutedEdges} = prepareDeploymentDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, undefined, response.data.automaticLayout);
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
            .then((response: AxiosResponse<DynamicView>) => {
                if (response.data.elementId) { 
                    DocuEyeApi.ElementsApi
                        .apiWorkspacesWorkspaceIdElementsIdGet(workspaceId, response.data.elementId)
                        .then((elResponse: AxiosResponse<Element>) => {
                            if (response.data.elements && response.data.relationships) {
                                const {layoutedNodes, layoutedEdges} = prepareDynamicDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, elResponse.data, response.data.automaticLayout);
                                setNodes(layoutedNodes);
                                setEdges(layoutedEdges);
                            }
                        });
                }else {
                    if (response.data.elements && response.data.relationships) {
                        const {layoutedNodes, layoutedEdges} = prepareDynamicDiagramElements(response.data.elements, response.data.relationships, viewConfiguration, undefined, response.data.automaticLayout);
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
        if(!selectedView || selectedView?.id !== props.selectedView?.id) {
            setSelectedView(props.selectedView);
            setWorkspaceId(props.workspaceId);
            setViewConfiguration(props.viewConfiguration);
        }
    },[props, selectedView])

    useEffect(() => {
        setCurrentImageView(null);
        if (selectedView && workspaceId) {
            if (selectedView.viewType === "SystemLandscapeView") {
                
                loadSystemLandscapeView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "SystemContextView") {
                loadSystemContextView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "ContainerView") {
                loadContainerView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "ComponentView") {
                loadComponentView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "FilteredView") {
                loadFilteredView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "DeploymentView") {
                loadDeploymentView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "DynamicView") {
                loadDynamicView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

        if (selectedView && workspaceId) {
            if (selectedView.viewType === "ImageView") {
                loadImageView(workspaceId, selectedView.id);
            }
        }
    }, [
        selectedView,workspaceId,viewConfiguration,
        loadSystemLandscapeView,
        loadSystemContextView,
        loadContainerView,
        loadComponentView,
        loadFilteredView,
        loadDeploymentView,
        loadDynamicView,
        loadImageView,
        setCurrentImageView
    ]);

    return (
        <>
            {currentImageView !== null && <ImageViewer image={currentImageView} />}
            {currentImageView === null && 
            <ReactFlow
                nodes={nodes}
                edges={edges}
                onNodesChange={onNodesChange}
                onEdgesChange={onEdgesChange}
                nodeTypes={nodeTypes}
                edgeTypes={edgeTypes}
                fitView
            >
                <Background />
                <Controls />
            </ReactFlow>}
            {isLoading && <Loader />}
        </>)
};

export default ViewDiagram;