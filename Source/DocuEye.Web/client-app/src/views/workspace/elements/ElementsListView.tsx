import { Autocomplete, Box, Card, CardContent, IconButton, Link, Table, TableBody, TableCell, TableHead, TableRow, TextField, Toolbar } from "@mui/material";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate, useParams } from "react-router-dom";
import DocuEyeApi from "../../../api";
import { WorkspaceCatalogElement } from "../../../api/docueye-api";
import Loader from "../../../components/loader";
import { Paginator } from "../../../components/paginator/Paginator";
import store from "../../../store";
import { IViewConfigurationState } from "../../../store/slices/viewConfiguration/IViewConfigurationState";
import { setViewConfiguration } from "../../../store/slices/viewConfiguration/viewConfigurationSlice";
import { getTerminologyTerm } from "../../../terminology/getTerminologyTerm";
import SearchIcon from '@mui/icons-material/Search';
import FilterAltOffIcon from '@mui/icons-material/FilterAltOff';
import { setWorkspaceData } from "../../../store/slices/workspace/workspaceSlice";

const componentTypes: string[] = [
    "Person",
    "SoftwareSystem",
    "Container",
    "Component",
    "DeploymentNode",
    "InfrastructureNode"
];

export const ElementsListView = () => {
    let { workspaceId } = useParams();
    const [elements, setElements] = useState<WorkspaceCatalogElement[]>([]);
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const pageSize = 50;
    const [nameFilter, setNameFilter] = useState<string>("");
    const [availableTypes, setAvaliableTypes] = useState<any[]>([]);
    const [typeFilter, setTypeFilter] = useState<any>(null);

    const viewConfiguration: IViewConfigurationState =
        useSelector((state: any) => state.viewConfiguration);
    const dispatch = useDispatch();

    useEffect(() => {
        if (!workspaceId) {
            return;
        }

        setIsLoading(true);
        const viewConfigurationState = store.getState().viewConfiguration;
        const workspaceState = store.getState().workspace;
        const getWorkspace = !workspaceState.value || (workspaceState.value && workspaceState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdGet(workspaceId)
            : Promise.resolve({ data: null })
        const getViewConfiguration = !viewConfigurationState.value || (viewConfigurationState.value && viewConfigurationState.value.id !== workspaceId) ?
            DocuEyeApi.WorkspacesApi.apiWorkspacesIdViewconfigurationGet(workspaceId)
            : Promise.resolve({ data: viewConfigurationState.value });
        const getElements = DocuEyeApi.ElementsApi.apiWorkspacesWorkspaceIdElementsGet(workspaceId, undefined, undefined, 50, 0);
        Promise.all([getWorkspace, getViewConfiguration, getElements])
            .then((responses) => {
                const getWorkspaceResponse = responses[0];
                const getViewConfigurationResponse = responses[1];
                const getElementsResponse = responses[2];

                if (getWorkspaceResponse.data) {
                    dispatch(setWorkspaceData(getWorkspaceResponse.data))
                }

                if (getViewConfigurationResponse.data) {
                    dispatch(setViewConfiguration(getViewConfigurationResponse.data));
                    const avaliableTypesOptions: any[] = []
                    componentTypes.forEach(type => {
                        avaliableTypesOptions.push(
                            {
                                id: type,
                                label: getTerminologyTerm(type, getViewConfigurationResponse.data.terminology)
                            }
                        );
                    });
                    setAvaliableTypes(avaliableTypesOptions);
                }
                setElements(getElementsResponse.data);
            }).finally(() => {
                setIsLoading(false);
            });

    }, [setIsLoading, setElements, dispatch, workspaceId]);

    const goToElement = (id: string | null | undefined) => {
        if (!id) {
            return;
        }
        navigate('/workspace/' + workspaceId + '/element/' + id);
    }

    const onPageChange = (page: number) => {
        setCurrentPage(page);
        loadElements(page);
    }

    const loadElements = (page: number, clearFilter: boolean = false) => {
        if (!workspaceId) {
            return;
        }
        setIsLoading(true);
        DocuEyeApi.ElementsApi
            .apiWorkspacesWorkspaceIdElementsGet(workspaceId,
                nameFilter === "" || clearFilter ? undefined : nameFilter,
                typeFilter === null || clearFilter ? undefined : typeFilter.id,
                pageSize, (page - 1) * pageSize)
            .then((response: AxiosResponse<WorkspaceCatalogElement[]>) => {
                if (response.data) {
                    setElements(response.data);
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    }

    const onTypeFilterChanged = (event: any, newValue: any | null) => {
        setTypeFilter(newValue);
    }

    const searchElements = () => {
        setCurrentPage(1);
        loadElements(1);
    }

    const clearFilter = () => {
        setTypeFilter(null);
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
                        <Autocomplete
                            value={typeFilter}
                            onChange={onTypeFilterChanged}
                            disablePortal
                            id="type-filter"
                            options={availableTypes}
                            sx={{ width: 300 }}
                            size="small"
                            disableClearable={false}
                            renderInput={(params) => <TextField {...params} label="Type" />}
                        />
                        <IconButton onClick={() => searchElements()}>
                            <SearchIcon />
                        </IconButton>
                        <IconButton onClick={() => clearFilter()}>
                            <FilterAltOffIcon />
                        </IconButton>
                    </Toolbar>
                    <Table sx={{ minWidth: 650 }} aria-label="elements table">
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Name</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Type</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }} align="right">Description</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {elements.map((element) => (
                                <TableRow
                                    key={element.id}>
                                    <TableCell align="right">
                                        <Link align="right" component="button"
                                            variant="body2" onClick={() => goToElement(element.id)}>
                                            {element.name}
                                        </Link>
                                    </TableCell>
                                    <TableCell align="right">{getTerminologyTerm(element.type, viewConfiguration.value?.terminology)}</TableCell>
                                    <TableCell align="right">{element.description}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                    <Paginator
                        pageSize={pageSize}
                        elementsCount={elements.length}
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
}