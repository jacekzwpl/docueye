import { Link, Card, CardContent, Table, TableBody, TableCell, TableHead, TableRow, Typography, Toolbar, FormControlLabel, Switch, Tooltip } from "@mui/material"
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import DocuEyeApi from "../../../../api";
import { ElementDependence } from "../../../../api/docueye-api";
import Loader from "../../../../components/loader";
import { IViewConfigurationState } from "../../../../store/slices/viewConfiguration/IViewConfigurationState";
import { getTerminologyTerm } from "../../../../terminology/getTerminologyTerm";
import { IElementDependencesProps } from "./IElementDependencesProps";
import InfoIcon from '@mui/icons-material/Info';

export const ElementDependences = (props: IElementDependencesProps) => {

    const navigate = useNavigate();
    const viewConfiguration: IViewConfigurationState =
        useSelector((state: any) => state.viewConfiguration);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [dependences, setDependences] = useState<ElementDependence[]>([]);

    const [showImplied, setShowImplied] = useState<boolean>(false);
    const handleShowImpliedChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setShowImplied(event.target.checked);
    };


    const goToElement = (id: string | null | undefined) => {
        if (!id) {
            return;
        }
        navigate('/workspace/' + props.element.workspaceId + '/element/' + id);
    }

    useEffect(() => {
        if (!props.element.workspaceId || !props.element.id) {
            return;
        }
        setIsLoading(true);
        DocuEyeApi.ElementsApi
            .apiWorkspacesWorkspaceIdElementsIdDependencesGet(props.element.workspaceId, props.element.id, showImplied)
            .then((response: AxiosResponse<ElementDependence[]>) => {
                setDependences(response.data);
            }).finally(() => {
                setIsLoading(false);
            })
    }, [props, setIsLoading, setDependences, showImplied])

    return (<Card variant="outlined">
        <CardContent>
            <Toolbar>
                <FormControlLabel control={<Switch checked={showImplied} onChange={handleShowImpliedChange} />} label="Show implied dependencies" />
                <Tooltip title="Implied dependencies are dependencies that are not explicitly defined but result from other explicitly defined relationships.">
                    <InfoIcon />
                </Tooltip>
            </Toolbar>
            {!isLoading && dependences.length === 0 &&
                <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                    {showImplied && "Element has no dependencies"}
                    {!showImplied && "Element has no explicitly defined dependencies"}
                </Typography>
            }
            {!isLoading && dependences.length > 0 &&
                <Table sx={{ minWidth: 650 }} aria-label="elements table">
                    <TableHead>
                        <TableRow>
                            <TableCell sx={{ fontWeight: 'bold' }} align="right">Name</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }} align="right">Type</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }} align="right">Relation Description</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }} align="right">Relation Technology</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {dependences.map((dependence) => (
                            <TableRow
                                key={dependence.id}>
                                <TableCell align="right">
                                    <Link align="right" component="button"
                                        variant="body2" onClick={() => goToElement(dependence.id)}>{dependence.name}
                                    </Link>
                                </TableCell>
                                <TableCell align="right">{getTerminologyTerm(dependence.type, viewConfiguration.value?.terminology)}</TableCell>
                                <TableCell align="right">{dependence.relationDescription}</TableCell>
                                <TableCell align="right">{dependence.relationTechnology}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            }
        </CardContent>
        {isLoading && <Loader />}
    </Card>)
}