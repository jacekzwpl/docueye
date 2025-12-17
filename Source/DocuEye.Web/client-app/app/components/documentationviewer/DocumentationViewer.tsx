import { Box } from "@mui/material";
import type { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import DocuEyeApi from "../../api";
import type { DocumentationContent } from "../../api/docueye-api";
import Loader from "../loader";
import type { IDocumentationViewerProps } from "./IDocumentationViewerProps";

export const DocumentationViewer = (props: IDocumentationViewerProps) => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [docHtml, setDocHtml] = useState<string>("");

    useEffect(() => {
        if (!props.workspaceId) {
            return;
        }
        setIsLoading(true);
        let currentHost: string = "";
        if (window.location.port) {
            currentHost = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;
        } else {
            currentHost = `${window.location.protocol}//${window.location.hostname}`;
        }

        const getDocumentation = props.elementId ?
            DocuEyeApi.DocumentationsApi.apiWorkspacesWorkspaceIdDocumentationsElementElementIdGet(props.workspaceId, props.elementId, currentHost, {
                validateStatus: (status) => {
                    return (status >= 200 && status < 300) || status === 404;
                }
            })
            : DocuEyeApi.DocumentationsApi.apiWorkspacesWorkspaceIdDocumentationsWorskpaceGet(props.workspaceId, currentHost, {
                validateStatus: (status) => {
                    return (status >= 200 && status < 300) || status === 404; 
                }
            });

        getDocumentation.then((response: AxiosResponse<DocumentationContent>) => {
            if (response.data.htmlContent) {
                setDocHtml(response.data.htmlContent);
            }else {
                setDocHtml("No documentation found");
            }

        }).finally(() => {
            setIsLoading(false);
        })




    }, [setIsLoading, props, setDocHtml]);

    return (
        <Box padding={2} >
            <Box paddingBottom={2} height={'calc(100vh - 168px)'} >
                <div dangerouslySetInnerHTML={{ __html: docHtml }} />
            </Box>
            {isLoading && <Loader />}
        </Box>

    );
}