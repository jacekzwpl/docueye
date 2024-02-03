import { Autocomplete, Box, TextField, Toolbar } from "@mui/material";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import DocuEyeApi from "../../../api";
import GraphViewer from "../../../components/graphviewer";
import Loader from "../../../components/loader";
import store from "../../../store";
import { setViewConfiguration } from "../../../store/slices/viewConfiguration/viewConfigurationSlice";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";

export const GraphView = () => {

    let { workspaceId, viewId } = useParams();

    const [availableViews, setAvailableViews] = useState<any[]>([]);
    const [selectedView, setSelectedView] = useState<any | null>(null);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const dispatch = useDispatch();
    const navigate = useNavigate();

    useEffect(() => {
        if (!workspaceId) {
            return;
        }
        setIsLoading(true);
        const viewConfigurationState = store.getState().viewConfiguration;
        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({ data: workspaceState.value });
        const getViewConfiguration = !viewConfigurationState.value || (viewConfigurationState.value && viewConfigurationState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdViewconfigurationGet(workspaceId)
            : Promise.resolve({ data: viewConfigurationState.value });

        Promise.all([getWorkspace, getViewConfiguration])
            .then((responses) => {

                const getWorkspaceResponse = responses[0];
                const getViewConfigurationResponse = responses[1];

                const avViews: any[] = [];
                getWorkspaceResponse.data.views?.forEach(view => {
                    if(view.viewType !== "ImageView") {
                        avViews.push({ id: view.id, label: view.name, viewType: view.viewType });
                    }
                });

                dispatch(setWorkspaceData(getWorkspaceResponse.data));

                setAvailableViews(avViews);
                if (getViewConfigurationResponse.data) {
                    dispatch(setViewConfiguration(getViewConfigurationResponse.data));
                }
                if (viewId) {
                    const view = avViews.find((obj) => { return obj.id === viewId });
                    if (view) {
                        setSelectedView(view);
                    } else {
                        setSelectedView(avViews[0]);
                    }
                } else {

                    navigate('/workspace/' + workspaceId + '/graph/' + avViews[0].id);
                }

            }).finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading, workspaceId,viewId, dispatch, navigate])

    const onSelectedViewChange = (event: any, newValue: any | null) => {
        navigate('/workspace/' + workspaceId + '/graph/' + newValue.id);
    }



    return (
        <Box padding={2} >
            <Box paddingBottom={2} >
                <Toolbar>
                    <Autocomplete
                        value={selectedView}
                        onChange={onSelectedViewChange}
                        disablePortal
                        id="combo-box-demo"
                        options={availableViews}
                        sx={{ width: 300 }}
                        size="small"
                        disableClearable={true}
                        renderInput={(params) => <TextField {...params} label="Diagram" />}
                    />
                </Toolbar>
            </Box>
            <GraphViewer selectedView={selectedView}
                        workspaceId={workspaceId} />
            {isLoading && <Loader />}
        </Box>
    );
}