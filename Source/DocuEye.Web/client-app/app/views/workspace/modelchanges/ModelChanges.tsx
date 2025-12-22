import { Box, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Link } from "@mui/material";
import type { AxiosResponse } from "axios";
import moment from "moment";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useParams } from "react-router";
import DocuEyeApi from "../../../api";
import type { ModelChange } from "../../../api/docueye-api";
import Loader from "../../../components/loader";
import { Paginator } from "../../../components/paginator/Paginator";
import store from "../../../store";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";

export const ModelChanges = () => {
    let { workspaceId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [modelChanges, setModelChanges] = useState<ModelChange[]>([]);
    const dispatch = useDispatch();
    const [currentPage, setCurrentPage] = useState<number>(1);
    const pageSize = 50;

    useEffect(() => {
        if (!workspaceId) {
            return;
        }

        setIsLoading(true);
        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({ data: null })
        const getModelChanges = DocuEyeApi.ModelChangesApi.apiWorkspaceIdModelchangesGet(workspaceId, 50, 0);
        Promise.all([getWorkspace, getModelChanges])
            .then((responses) => {
                const getWorkspaceResponse = responses[0];
                const getModelChangesResponse = responses[1];


                if (getWorkspaceResponse.data) {
                    dispatch(setWorkspaceData(getWorkspaceResponse.data))
                }

                setModelChanges(getModelChangesResponse.data);
            }).finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading, setModelChanges, dispatch, workspaceId]);


    const onPageChange = (page: number) => {
        setCurrentPage(page);
        load(page);
    }

    const openUrl = (url: string | null | undefined) => {
        if (!url) {
            return;
        }
        window.open(url, '_blank', 'noopener,noreferrer');
    }

    const load = (page: number) => {
        if (!workspaceId) {
            return;
        }
        setIsLoading(true);
        DocuEyeApi.ModelChangesApi
            .apiWorkspaceIdModelchangesGet(workspaceId, pageSize, (page - 1) * pageSize)
            .then((response: AxiosResponse<ModelChange[]>) => {
                if (response.data) {
                    setModelChanges(response.data);
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    }


    return (
        <Box padding={2} >
            <Box paddingBottom={2} height={'calc(100vh - 168px)'} >
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 650 }} aria-label="elements table">
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Description</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Event Time</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Import</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {modelChanges.map((modelChange) => (
                                <TableRow
                                    key={modelChange.id}
                                //sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                >
                                    <TableCell align="right">
                                        {modelChange.description}
                                    </TableCell>
                                    <TableCell align="right">{moment.utc(modelChange.eventTime).local().format("YYYY-MM-DD HH:mm:ss")}</TableCell>
                                    <TableCell align="right">
                                        {modelChange.importLink && <Link align="right" component="button"
                                            variant="body2" onClick={() => openUrl(modelChange.importLink)}>
                                            {modelChange.importKey}
                                        </Link>}
                                        {!modelChange.importLink && `${modelChange.importKey}`}
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
                <Paginator
                    pageSize={pageSize}
                    elementsCount={modelChanges.length}
                    currentPage={currentPage}
                    onPreviousClick={onPageChange}
                    onNextClick={onPageChange}
                />
                <br />
            </Box>
            {isLoading && <Loader />}
        </Box>)
}