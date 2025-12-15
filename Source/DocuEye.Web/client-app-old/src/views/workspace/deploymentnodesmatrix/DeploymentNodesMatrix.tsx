import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { DeploymentNodeRelationship } from "../../../api/docueye-api";
import store from "../../../store";
import DocuEyeApi from "../../../api";
import { useDispatch } from "react-redux";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";
import Loader from "../../../components/loader";
import { Box, Card, CardContent, IconButton, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";

import ArrowUpwardIcon from '@mui/icons-material/ArrowUpward';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import SyncAltIcon from '@mui/icons-material/SyncAlt';
import { RelationshipDescriptionDialog } from "./RelationshipDescriptionDialog";

export const DeploymentNodesMatrix = () => {
    let { workspaceId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [relationships, setRelationships] = useState<DeploymentNodeRelationship[]>([]);
    const [nodes, setNodes] = useState<any[]>([]);
    const dispatch = useDispatch();
    const [descriptionOpened, setDescriptionOpened] = useState<boolean>(false);
    const [descriptionData, setDescriptionData] = useState<{ fromSource?: DeploymentNodeRelationship, fromDestination?: DeploymentNodeRelationship }>({});


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

    useEffect(() => {
        const nodes: any[] = [];
        relationships.forEach(relationship => {
            const exisintgSourceNode = nodes.find(node => node.id === relationship.sourceNodeId);
            const exisintgDestinationNode = nodes.find(node => node.id === relationship.destinationNodeId);
            if (!exisintgSourceNode) {
                nodes.push({ id: relationship.sourceNodeId, name: relationship.sourceNodeName, technology: relationship.sourceNodeTechnology });
            }
            if (!exisintgDestinationNode) {
                nodes.push({ id: relationship.destinationNodeId, name: relationship.destinationNodeName, technology: relationship.destinationTechnology });
            }
        });
        nodes.sort((a, b) => {
            if (a.name < b.name) {
                return -1;
            }
            if (a.name > b.name) {
                return 1;
            }
            return 0;
        });
        setNodes(nodes);
    }, [relationships, setNodes])

    const getNodesRelation = (sourceNodeId: string, destinationNodeId: string) => {
        const fromSource = relationships.find(relationship => relationship.sourceNodeId === sourceNodeId && relationship.destinationNodeId === destinationNodeId);
        const fromDestination = relationships.find(relationship => relationship.sourceNodeId === destinationNodeId && relationship.destinationNodeId === sourceNodeId);
        return {
            fromSource,
            fromDestination
        }
    }

    const openDescriptionDialog = (fromSource?: DeploymentNodeRelationship, fromDestination?: DeploymentNodeRelationship) => {
        setDescriptionData({ fromSource, fromDestination });
        setDescriptionOpened(true);
    }

    return (
        <Box padding={2} >
            <Card variant="outlined" >
                <CardContent>
                    <TableContainer sx={{ height: 'calc(100vh - 158px)' }}>
                        <Table stickyHeader sx={{ minWidth: 650 }} aria-label="elements table">
                            <TableHead>
                                <TableRow>
                                    <TableCell sx={{
                                        position: "sticky",
                                        left: 0,
                                        zIndex: 3,
                                        background: "white",
                                    }}>Deployment Nodes</TableCell>
                                    {nodes.map((node) => (
                                        <TableCell sx={{ minWidth: 100 }} key={node.id}><strong>{node.name}</strong><br />[{node.technology}]</TableCell>
                                    ))}

                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {nodes.map((sourceNode) => (
                                    <TableRow key={sourceNode.id}>
                                        <TableCell sx={{
                                            position: "sticky",
                                            left: 0,
                                            zIndex: 2,
                                            background: "white"
                                        }}><strong>{sourceNode.name}</strong><br />[{sourceNode.technology}]</TableCell>
                                        {nodes.map((destNode) => {
                                            const dataR = getNodesRelation(sourceNode.id, destNode.id);
                                            return (<TableCell key={destNode.id}>
                                                {dataR.fromSource && !dataR.fromDestination && <IconButton onClick={() => openDescriptionDialog(dataR.fromSource)}><ArrowUpwardIcon /></IconButton>}
                                                {!dataR.fromSource && dataR.fromDestination && <IconButton onClick={() => openDescriptionDialog(undefined, dataR.fromDestination)}><ArrowBackIcon /></IconButton>}
                                                {dataR.fromSource && dataR.fromDestination && <IconButton onClick={() => openDescriptionDialog(dataR.fromSource, dataR.fromDestination)}><SyncAltIcon /></IconButton>}
                                            </TableCell>)
                                        })}
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </CardContent>
            </Card>
            {isLoading && <Loader />}
            <RelationshipDescriptionDialog 
                data={descriptionData}
                onClose={() => setDescriptionOpened(false)} 
                open={descriptionOpened} />
        </Box>)
}