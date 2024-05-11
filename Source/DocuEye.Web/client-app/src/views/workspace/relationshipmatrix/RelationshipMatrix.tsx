import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { DeploymentNodeRelationship } from "../../../api/docueye-api";
import store from "../../../store";
import DocuEyeApi from "../../../api";
import { useDispatch } from "react-redux";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";
import Loader from "../../../components/loader";

export const RelationshipMatrix = () => {
    let { workspaceId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [relationships, setRelationships] = useState<DeploymentNodeRelationship[]>([]);
    const dispatch = useDispatch();

    useEffect(() => {
        if (!workspaceId) {
            return;
        }

        setIsLoading(true);
        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({ data: null })
        const getModelChanges = DocuEyeApi.ElementsApi.apiWorkspacesWorkspaceIdElementsDeploymentnodesmatrixGet(workspaceId);
        Promise.all([getWorkspace, getModelChanges])
            .then((responses) => {
                const getWorkspaceResponse = responses[0];
                const getModelChangesResponse = responses[1];


                if (getWorkspaceResponse.data) {
                    dispatch(setWorkspaceData(getWorkspaceResponse.data))
                }

                setRelationships(getModelChangesResponse.data);
            }).finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading, setRelationships, dispatch, workspaceId]);


    return (<>{isLoading && <Loader />}</>)
}