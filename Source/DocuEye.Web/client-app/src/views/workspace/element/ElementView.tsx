
import { TabContext, TabPanel } from "@mui/lab";
import { Tab, Tabs } from "@mui/material"
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useParams } from "react-router-dom";
import DocuEyeApi from "../../../api";
import Loader from "../../../components/loader";
import store from "../../../store";
import { setViewConfiguration } from "../../../store/slices/viewConfiguration/viewConfigurationSlice";
import OverView from "./overview";
import { Element } from "../../../api/docueye-api";
import ElementDependences from "./dependences";
import DecisionsTimeline from "../../../components/decisionstimeline";
import DocumentationViewer from "../../../components/documentationviewer";
import ElementConsumers from "./consumers";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";
import { ElementOpenApi } from "./openapi/ElementOpenApi";

export const ElementView = () => {

    let { workspaceId, elementId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [element, setElment] = useState<Element | null>(null);

    const [selectedTab, setSelectedTab] = useState('0');
    const dispatch = useDispatch();


    useEffect(() => {
        if (!workspaceId || !elementId) {
            return;
        }

        setIsLoading(true);
        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({data: null});
        const viewConfigurationState = store.getState().viewConfiguration;
        const getViewConfiguration = !viewConfigurationState.value || (viewConfigurationState.value && viewConfigurationState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdViewconfigurationGet(workspaceId)
            : Promise.resolve({ data: viewConfigurationState.value });
        const getElement = DocuEyeApi.ElementsApi.apiWorkspacesWorkspaceIdElementsIdGet(workspaceId, elementId);
        Promise.all([getWorkspace, getViewConfiguration, getElement])
            .then((responses) => {
                const getWorkspaceResponse = responses[0];
                const getViewConfigurationResponse = responses[1];
                const getElementResponse = responses[2];

                if(getWorkspaceResponse.data) {
                    dispatch(setWorkspaceData(getWorkspaceResponse.data));
                }

                if (getViewConfigurationResponse.data) {
                    dispatch(setViewConfiguration(getViewConfigurationResponse.data));
                }
                setElment(getElementResponse.data);
            }).finally(() => {
                setIsLoading(false);
                setSelectedTab('0');
            });

    }, [setIsLoading, dispatch,setElment,elementId, workspaceId]);


    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        setSelectedTab(newValue.toString());
    };

    return (<>
        <TabContext value={selectedTab}>
            <Tabs
                value={selectedTab}
                onChange={handleChange}
                indicatorColor="secondary"
                textColor="inherit"
                variant="fullWidth"
                aria-label="full width tabs example"
            >
                <Tab label="Overview" value="0" />
                <Tab label="Dependences" value="1" />
                <Tab label="Consumers" value="2" />
                <Tab label="Documentation" value="3" />
                <Tab label="Decisions" value="4" />
                <Tab label="Open Api" value="5" />
            </Tabs>
            <TabPanel value="0"  >
                {element && <OverView element={element} /> }
            </TabPanel>
            <TabPanel value="1"  >
                {element && <ElementDependences element={element} />}
            </TabPanel>
            <TabPanel value="2"  >
                {element && <ElementConsumers element={element} />}
            </TabPanel>
            <TabPanel value="3"  >
                <DocumentationViewer workspaceId={workspaceId} elementId={element?.id} />
            </TabPanel>
            <TabPanel value="4"  >
                <DecisionsTimeline workspaceId={workspaceId} elementId={element?.id} />
            </TabPanel>
            <TabPanel value="5"  >
                <ElementOpenApi workspaceId={workspaceId} elementId={element?.id}  />
            </TabPanel>
        </TabContext>
        {isLoading && <Loader />}
    </>)
}