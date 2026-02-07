import {  Box, Card, CardContent, Link, Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import type { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useParams } from "react-router";
import DocuEyeApi from "../../../api";
import type { Issue } from "../../../api/docueye-api";
import Loader from "../../../components/loader";
import store from "../../../store";
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';


export const IssuesView = () => {
    let { workspaceId } = useParams();
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const [issues, setIssues] = useState<Issue[]>([]);
    const dispatch = useDispatch();
    useEffect(() => {
        if (!workspaceId) {
            return;
        }

        setIsLoading(true);

        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({data: null});

        getWorkspace.then((response:any) => {
            if(response.data) {
                dispatch(setWorkspaceData(response.data));
            }
        })
        DocuEyeApi.IssuesApi.apiWorkspacesWorkspaceIdIssuesGet(workspaceId)
            .then((response: AxiosResponse<Issue[]>) => {
                setIssues(response.data);
            })
            .finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading, workspaceId, dispatch]);

    const issueMessage = (issue: Issue): string => {

        if (issue.element !== null) {
            return `Issue in element "${issue.element?.name}". `;
        } else if (issue.relationship !== null) {
            return `Issue in relationship "${issue.relationship?.source?.name} -> ${issue.relationship?.destination?.name}". `;
        }
        return issue.message ?? "";
    }

    return (
        <Box padding={2} >
            <Card variant="outlined">
                <CardContent>

                    <Table sx={{ minWidth: 650 }} aria-label="elements table">
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ minWidth: 100, fontWeight: 'bold' }} align="right">Rule ID</TableCell>
                                <TableCell sx={{ minWidth: 100, fontWeight: 'bold' }} align="right">Severity</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Message</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {issues.map((issue) => (
                                <TableRow
                                    key={issue.id}>
                                    <TableCell align="right">
                                        {issue.rule?.helpLink && (
                                            <Link href={issue.rule?.helpLink} target="_blank" rel="noopener" align="right" 
                                                variant="body2">
                                                {issue.rule?.id}
                                            </Link>)}
                                        {!issue.rule?.helpLink && (
                                            <>{issue.rule?.id}</>
                                        )}
                                    </TableCell>
                                    <TableCell align="right">
                                        {issue.severity === 1 && <Stack direction="row" alignItems="center" gap={1}>
                                            <ErrorOutlineIcon color="error" />
                                            <Typography color="error">
                                                Error
                                            </Typography>
                                        </Stack>}
                                        {issue.severity === 2 &&
                                            <Stack direction="row" alignItems="center" gap={1}>
                                                <ErrorOutlineIcon color="warning" />
                                                <Typography color="warning">
                                                    Warning
                                                </Typography>
                                            </Stack>
                                        }
                                        {issue.severity === 3 && <Stack direction="row" alignItems="center" gap={1}>
                                            <ErrorOutlineIcon color="info" />
                                            <Typography color="info">
                                                Info
                                            </Typography>
                                        </Stack>}
                                    </TableCell>
                                    <TableCell align="right">
                                        <b>{issue.rule?.name}</b> <br />
                                        {issueMessage(issue)}< br />
                                        {issue.rule?.description}
                                    </TableCell>
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