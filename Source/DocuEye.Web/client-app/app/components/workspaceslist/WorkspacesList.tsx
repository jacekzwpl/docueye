import { Box, Card, CardContent, IconButton, Link, Table, TableBody, TableCell, TableHead, TableRow, TextField, Toolbar } from "@mui/material";
import type { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import DocuEyeApi from "../../api";
import type { FoundedWorkspace } from "../../api/docueye-api";
import SearchIcon from '@mui/icons-material/Search';
import FilterAltOffIcon from '@mui/icons-material/FilterAltOff';
import { Paginator } from "../../components/paginator/Paginator";
import Loader from "../../components/loader";

export const WorkspacesList = () => {

    const [workspaces, setWorkspaces] = useState<FoundedWorkspace[]>([]);
    const navigate = useNavigate();
    const [nameFilter, setNameFilter] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const pageSize = 50;

    useEffect(() => {
        console.log("WorkspacesList mounted");
        
    }, []);

    useEffect(() => {
        console.log("Loading workspaces...");
        setIsLoading(true);
        DocuEyeApi.WorkspacesApi.apiWorkspacesGet(undefined, pageSize, 0)
            .then((response: AxiosResponse<FoundedWorkspace[]>) => {
                setWorkspaces(response.data);
            }).finally(() => {
                setIsLoading(false);
            });

    }, [setWorkspaces, setIsLoading]);

    const goToWorkspace = (id: string | null | undefined, name: string | null | undefined) => {
        if (!id || !name) {
            return;
        }
        navigate('/workspace/' + id + '/elements');
    }

    const loadElements = (page: number, clearFilter: boolean = false) => {
        setIsLoading(true);
        DocuEyeApi.WorkspacesApi.apiWorkspacesGet(
            nameFilter === "" || clearFilter ? undefined : nameFilter,
            pageSize,
            (page - 1) * pageSize
        )
            .then((response: AxiosResponse<FoundedWorkspace[]>) => {
                setWorkspaces(response.data);
            }).finally(() => {
                setIsLoading(false);
            });


    }
    const onPageChange = (page: number) => {
        setCurrentPage(page);
        loadElements(page);
    }
    const searchElements = () => {
        setCurrentPage(1);
        loadElements(1);
    }

    const clearFilter = () => {
        setNameFilter("");
        loadElements(1, true);
    }

    return (
        <Box padding={2} >
            <Card variant="outlined">
                <CardContent>
                    <Toolbar>
                        <TextField
                            label="Name"
                            size="small"
                            id="name-filter"
                            value={nameFilter}
                            onChange={(event: React.ChangeEvent<HTMLInputElement>) => {
                                setNameFilter(event.target.value);
                            }}
                        />&nbsp;

                        <IconButton onClick={() => searchElements()}>
                            <SearchIcon />
                        </IconButton>
                        <IconButton onClick={() => clearFilter()}>
                            <FilterAltOffIcon />
                        </IconButton>
                    </Toolbar>
                    <Table sx={{ minWidth: 650 }}>
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Name</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Description</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {workspaces.map((workspace) => (
                                <TableRow key={workspace.id}>
                                    <TableCell align="right">
                                        <Link align="right" component="button"
                                            variant="body2" onClick={() => goToWorkspace(workspace.id, workspace.name)}>
                                            {workspace.name}
                                        </Link>
                                    </TableCell>
                                    <TableCell align="right">{workspace.description}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                    <Paginator
                        pageSize={pageSize}
                        elementsCount={workspaces.length}
                        currentPage={currentPage}
                        onPreviousClick={onPageChange}
                        onNextClick={onPageChange}
                    />
                    <br />
                </CardContent>
            </Card>
            {isLoading && <Loader />}
        </Box>
    );
};