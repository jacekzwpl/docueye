import { Autocomplete, Box, Card, CardContent, IconButton, Link, Table, TableBody, TableCell, TableHead, TableRow, TextField, Toolbar, Typography } from "@mui/material";
import type { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router";
import DocuEyeApi from "../../../api";
import type { Issue, WorkspaceCatalogElement } from "../../../api/docueye-api";
import Loader from "../../../components/loader";
import { Paginator } from "../../../components/paginator/Paginator";
import store from "../../../store";
import type { IViewConfigurationState } from "../../../store/slices/viewConfiguration/IViewConfigurationState";
import { setViewConfiguration } from "../../../store/slices/viewConfiguration/viewConfigurationSlice";
import { getTerminologyTerm } from "../../../terminology/getTerminologyTerm";
import SearchIcon from '@mui/icons-material/Search';
import FilterAltOffIcon from '@mui/icons-material/FilterAltOff';
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';


export const IssuesView = () => {
    let { workspaceId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const [issues, setIssues] = useState<Issue[]>([]);

    useEffect(() => {
        if (!workspaceId) {
            return;
        }

        setIsLoading(true);
        DocuEyeApi.IssuesApi.apiWorkspacesWorkspaceIdIssuesGet(workspaceId)
            .then((response: AxiosResponse<Issue[]>) => {
                setIssues(response.data);
            })
            .finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading, workspaceId]);




    return (
        <Box padding={2} >
            <Card variant="outlined">
                <CardContent>

                    <Table sx={{ minWidth: 650 }} aria-label="elements table">
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Rule ID</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Name</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Severity</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Description</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {issues.map((issue) => (
                                <TableRow
                                    key={issue.id}>
                                    <TableCell align="right">
                                        <Link align="right" component="button"
                                            variant="body2">
                                            {issue.rule?.id}
                                        </Link>
                                    </TableCell>
                                    <TableCell align="right">
                                        {issue.rule?.name}
                                    </TableCell>
                                    <TableCell align="right">
                                        {issue.severity === 1 && <>
                                            <Typography color="error">
                                                <ErrorOutlineIcon color="error" /> Error
                                            </Typography>
                                        </>}
                                        {issue.severity === 2 && <>
                                            <Typography color="warning">
                                                <ErrorOutlineIcon color="warning" /> Warning
                                            </Typography>
                                        </>}
                                        {issue.severity === 3 && <>
                                            <Typography color="info">
                                                <ErrorOutlineIcon color="info" /> Info
                                            </Typography>
                                        </>}
                                    </TableCell>
                                    <TableCell align="right">{issue.rule?.description}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                    <br />
                </CardContent>
            </Card>
            {isLoading && <Loader />}
        </Box>
    );
}