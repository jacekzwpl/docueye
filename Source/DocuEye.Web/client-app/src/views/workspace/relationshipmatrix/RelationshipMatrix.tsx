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

export const RelationshipMatrix = () => {
    let { workspaceId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [relationships, setRelationships] = useState<DeploymentNodeRelationship[]>([]);
    const [nodes, setNodes] = useState<any[]>([]);
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

    useEffect(() => {
        const nodes: any[] = [];
        relationships.forEach(relationship => {
            const exisintgSourceNode = nodes.find(node => node.id === relationship.sourceNodeId);
            const exisintgDestinationNode = nodes.find(node => node.id === relationship.destinationNodeId);
            if (!exisintgSourceNode) {
                nodes.push({ id: relationship.sourceNodeId, name: relationship.sourceNodeName });
            }
            if (!exisintgDestinationNode) {
                nodes.push({ id: relationship.destinationNodeId, name: relationship.destinationNodeName });
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

    return (
        <Box padding={2} >
            <Card variant="outlined" >
                <CardContent>
                    <TableContainer sx={{height: 'calc(100vh - 168px)'}}>
                        <Table stickyHeader sx={{ minWidth: 650}} aria-label="elements table">
                            <TableHead>
                                <TableRow>
                                    <TableCell sx={{
                                        position: "sticky",
                                        left: 0,
                                        background: "white",
                                        boxShadow: "5px 2px 5px grey",
                                        borderRight: "2px solid black"
                                    }}>Source/Destination</TableCell>
                                    {nodes.map((node) => (
                                        <TableCell sx={{minWidth: 100}} key={node.id}><strong>{node.name}</strong></TableCell>
                                    ))}
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                    <TableCell sx={{minWidth: 100}} ><strong>aaaa1</strong></TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {nodes.map((sourceNode) => (
                                    <TableRow key={sourceNode.id}>
                                        <TableCell sx={{
                                            position: "sticky",
                                            left: 0,
                                            background: "white",
                                            boxShadow: "5px 2px 5px grey",
                                            borderRight: "2px solid black"
                                        }}><strong>{sourceNode.name}</strong></TableCell>
                                        {nodes.map((destNode) => {
                                            const dataR = getNodesRelation(sourceNode.id, destNode.id);
                                            return (<TableCell key={destNode.id}>
                                                {dataR.fromSource && !dataR.fromDestination && <IconButton><ArrowUpwardIcon /></IconButton>}
                                                {!dataR.fromSource && dataR.fromDestination && <IconButton><ArrowBackIcon /></IconButton>}
                                                {dataR.fromSource && dataR.fromDestination && <IconButton><SyncAltIcon /></IconButton>}
                                            </TableCell>)
                                        })}
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                        <TableCell ><strong>aaaa1</strong></TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </CardContent>
            </Card>
            {isLoading && <Loader />}
        </Box>)
}