import { Box } from "@mui/material";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useParams } from "react-router-dom";
import DocuEyeApi from "../../../api";
import Loader from "../../../components/loader";
import store from "../../../store";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";

export const DecisionView = () => {
    let { workspaceId, decisionId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [docHtml, setDocHtml] = useState<string>("");
    //const [workspaceName, setWorkspaceName] = useState<string>("");
    const dispatch = useDispatch();
    useEffect(() => {
        if (!workspaceId || !decisionId) {
            return;
        }
        setIsLoading(true);
        let currentHost:string = "";
        if(window.location.port) {
            currentHost = `${window.location.protocol}//${window.location.hostname}:${window.location.port}`;
        }else {
            currentHost = `${window.location.protocol}//${window.location.hostname}`;
        }

        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({data: null});

        //const getWorkspace = DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId);
        const getDecision = DocuEyeApi.DecisionsApi.apiWorkspacesWorkspaceIdDecisionsIdGet(workspaceId, decisionId, `${currentHost}/api/workspaces/${workspaceId}/documentations/{documentationId}/images/`);
        Promise.all([getDecision, getWorkspace])
        .then((responses) => {
            const getDecisionResponse = responses[0];
            const getWorkspaceResponse = responses[1];
            
            if(getWorkspaceResponse.data) {
                dispatch(setWorkspaceData(getWorkspaceResponse.data));
                //setWorkspaceName(getWorkspaceResponse.data.name);
            }
            if (getDecisionResponse.data.htmlContent) { 
                setDocHtml(getDecisionResponse.data.htmlContent);
            }

        }).catch((reason) => {
            if(reason.response.status === 404) {
                setDocHtml("<strong>No decisions found</strong>");
            }
        }).finally(() => {
            setIsLoading(false);
        });
       

    }, [setIsLoading, workspaceId,decisionId, setDocHtml, dispatch]);

    return (
        <Box padding={2} >

                <Box paddingBottom={2} >
                    
                </Box>
                <Box paddingBottom={2} height={'calc(100vh - 168px)'} >
                <div dangerouslySetInnerHTML={{__html: docHtml}} />
                </Box>

            {isLoading && <Loader />}
        </Box>

    );
};

