import { Box } from "@mui/material";
import { AxiosResponse } from "axios";
import { useCallback, useEffect, useRef, useState } from "react";
import DocuEyeApi from "../../api";
import { ComponentView, ContainerView, FilteredView, SystemContextView, SystemLandscapeView, ViewConfiguration } from "../../api/docueye-api";
import { createGraph } from "./functions/createGraph";
import { IGraphViewerProps } from "./IGraphViewerProps";
import * as d3 from "d3";
import Loader from "../loader";
import { prepareGraphElements } from "./functions/prepareGraphElements";

export const GraphViewer = (props: IGraphViewerProps) => {
    const container = useRef<any>();
    //let destroyFn: () => void;
    const [graphResult,setGraphResult] = useState<any>(null);
    const [selectedView, setSelectedView] = useState(props.selectedView);
    const [workspaceId, setWorkspaceId] = useState(props.workspaceId);
    const [viewConfiguration, setViewConfiguration] = useState(props.viewConfiguration);

    const [isLoading, setIsLoading] = useState<boolean>(false);

    const loadSystemLandscapeView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        return DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsSystemlandscapeIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<SystemLandscapeView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {elements, links} = prepareGraphElements(response.data.elements, response.data.relationships, viewConfiguration);
                    const graphResult = createGraph(container.current, elements, links);
                    setGraphResult(graphResult);
                }
            }).finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading,setGraphResult]);

    const loadSystemContextView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsSystemcontextIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<SystemContextView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {elements, links} = prepareGraphElements(response.data.elements, response.data.relationships, viewConfiguration);
                    //const { destroy } = createGraph(container.current, elements, links);
                    //destroyFn = destroy;
                    const graphResult = createGraph(container.current, elements, links)
                    setGraphResult(graphResult);
                }
            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setGraphResult]);


    const loadContainerView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsContainerIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<ContainerView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {elements, links} = prepareGraphElements(response.data.elements, response.data.relationships, viewConfiguration);
                    const graphResult = createGraph(container.current, elements, links)
                    setGraphResult(graphResult);
                }

            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setGraphResult]);

    const loadComponentView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsComponentIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<ComponentView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {elements, links} = prepareGraphElements(response.data.elements, response.data.relationships, viewConfiguration);
                    const graphResult = createGraph(container.current, elements, links)
                    setGraphResult(graphResult);
                }

            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading,setGraphResult]);

    const loadFilteredView = useCallback((workspaceId: string, viewId: string, viewConfiguration?: ViewConfiguration | null) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsFilteredIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<FilteredView>) => {
                if (response.data.elements && response.data.relationships) {
                    const {elements, links} = prepareGraphElements(response.data.elements, response.data.relationships, viewConfiguration);
                    const graphResult = createGraph(container.current, elements, links)
                    setGraphResult(graphResult);
                }
            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading, setGraphResult]);


    useEffect(() => {
        if (selectedView && workspaceId && container.current) {

            d3.select(container.current).select("svg").remove();

            if (selectedView.viewType === "SystemLandscapeView") {
                loadSystemLandscapeView(workspaceId, selectedView.id, viewConfiguration);
            }

            if (selectedView.viewType === "SystemContextView") {
                loadSystemContextView(workspaceId, selectedView.id, viewConfiguration);
            }

            if (selectedView.viewType === "ContainerView") {
                loadContainerView(workspaceId, selectedView.id, viewConfiguration);
            }

            if (selectedView.viewType === "ComponentView") {
                loadComponentView(workspaceId, selectedView.id, viewConfiguration);
            }

            if (selectedView.viewType === "FilteredView") {
                loadFilteredView(workspaceId, selectedView.id, viewConfiguration);
            }
        }

    }, [container, selectedView, workspaceId, viewConfiguration,
        loadSystemLandscapeView, 
        loadSystemContextView,
        loadContainerView,
        loadComponentView,
        loadFilteredView
    ])

    useEffect(() => {
        if (!selectedView || selectedView?.id !== props.selectedView?.id) {
            setSelectedView(props.selectedView);
            setWorkspaceId(props.workspaceId);
            setViewConfiguration(props.viewConfiguration);
        }
    }, [props, selectedView])

    useEffect(() => {
        return () => {
            if (graphResult?.destroy) {
                graphResult?.destroy();
            }
        };
    }, [graphResult])

    return (
        <>
            <Box ref={container} padding={2} height={'calc(100vh - 168px)'} >

            </Box>
            {isLoading && <Loader />}
        </>
    );
};

export default GraphViewer;


