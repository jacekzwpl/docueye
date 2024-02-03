import { Box } from "@mui/material";
import { AxiosResponse } from "axios";
import { useCallback, useEffect, useRef, useState } from "react";
import DocuEyeApi from "../../api";
import { SystemContextView, SystemLandscapeView } from "../../api/docueye-api";
import { createGraph } from "./functions/createGraph";
import { IGraphViewerProps } from "./IGraphViewerProps";
import * as d3 from "d3";
import Loader from "../loader";

export const GraphViewer = (props: IGraphViewerProps) => {
    const container = useRef<any>();
    let destroyFn:() => void;
    const [selectedView, setSelectedView] = useState(props.selectedView);
    const [workspaceId, setWorkspaceId] = useState(props.workspaceId);

    const [isLoading, setIsLoading] = useState<boolean>(false);

    const loadSystemLandscapeView = useCallback((workspaceId: string, viewId: string) => {
        setIsLoading(true);
        return DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsSystemlandscapeIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<SystemLandscapeView>) => {
                const links: any[] = [];
                response.data.relationships?.forEach((relationship) => {
                    links.push({
                        source: relationship.sourceId,
                        target: relationship.destinationId
                    })
                })
                const { destroy } = createGraph(container.current, response.data.elements, links);
                destroyFn = destroy;
            }).finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading]);

    const loadSystemContextView = useCallback((workspaceId: string, viewId: string) => {
        setIsLoading(true);
        DocuEyeApi.ViewsApi
            .apiWorkspacesWorkspaceIdViewsSystemcontextIdGet(workspaceId, viewId)
            .then((response: AxiosResponse<SystemContextView>) => {
                if (response.data.elements && response.data.relationships) {
                    const links: any[] = [];
                    response.data.relationships?.forEach((relationship) => {
                        links.push({
                            source: relationship.sourceId,
                            target: relationship.destinationId
                        })
                    })
                    const { destroy } = createGraph(container.current, response.data.elements, links);
                    destroyFn = destroy;
                }
            }).finally(() => {
                setIsLoading(false);
            });
    }, [setIsLoading]);


    useEffect(() => {
        if (selectedView && workspaceId && container.current) {
            if(destroyFn) {
                destroyFn();
            }
            d3.select(container.current).select("svg").remove();
            if (selectedView.viewType === "SystemLandscapeView") {
                loadSystemLandscapeView(workspaceId, selectedView.id);
            }
            if (selectedView.viewType === "SystemContextView") {
                loadSystemContextView(workspaceId, selectedView.id);
            }
        }

    }, [container, selectedView, workspaceId, loadSystemLandscapeView, loadSystemContextView])

    useEffect(() => {
        if (!selectedView || selectedView?.id !== props.selectedView?.id) {
            setSelectedView(props.selectedView);
            setWorkspaceId(props.workspaceId);
        }
    }, [props, selectedView])

    useEffect(() => {
        return () => {
            if(destroyFn) {
                destroyFn();
            }
        };
    },[])

    return (
        <>
        <Box ref={container} padding={2} height={'calc(100vh - 168px)'} >

        </Box>
        {isLoading && <Loader />}
        </>
    );
};

export default GraphViewer;