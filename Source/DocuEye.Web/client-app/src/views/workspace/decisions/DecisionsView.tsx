
import { useEffect } from "react";
import { useDispatch } from "react-redux";
import { useParams } from "react-router-dom";
import DocuEyeApi from "../../../api";
import DecisionsTimeline from "../../../components/decisionstimeline";
import store from "../../../store";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";


export const DecisionsView = () => {
    let { workspaceId } = useParams();
    const dispatch = useDispatch();
    useEffect(() => {
        if(!workspaceId) {
            return;
        }

        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({data: null});

        getWorkspace.then((response:any) => {
            if(response.data) {
                dispatch(setWorkspaceData(response.data));
            }
        })
        
    },[dispatch, workspaceId])
    return (<DecisionsTimeline workspaceId={workspaceId} />)
};