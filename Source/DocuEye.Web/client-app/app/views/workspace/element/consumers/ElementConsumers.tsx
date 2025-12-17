import { Card, CardContent, FormControlLabel, Link, Switch, Table, TableBody, TableCell, TableHead, TableRow, Toolbar, Tooltip, Typography } from "@mui/material";
import type { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router";
import DocuEyeApi from "../../../../api";
import type { ElementConsumer } from "../../../../api/docueye-api";
import Loader from "../../../../components/loader";
import type { IViewConfigurationState } from "../../../../store/slices/viewConfiguration/IViewConfigurationState";
import { getTerminologyTerm } from "../../../../terminology/getTerminologyTerm";
import type { IElementConsumers } from "./IElementConsumers";
import InfoIcon from '@mui/icons-material/Info';

export const ElementConsumers = (props: IElementConsumers) => {
    const navigate = useNavigate();
    const viewConfiguration: IViewConfigurationState =
        useSelector((state: any) => state.viewConfiguration);

    const [consumers, setConsumers] = useState<ElementConsumer[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);

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
            .apiWorkspacesWorkspaceIdElementsIdConsumersGet(props.element.workspaceId, props.element.id, showImplied)
            .then((response: AxiosResponse<ElementConsumer[]>) => {
                setConsumers(response.data);
            }).finally(() => {
                setIsLoading(false);
            })
    }, [props, setConsumers, setIsLoading, showImplied])

    return (<Card variant="outlined">
        <CardContent>
            <Toolbar>
                <FormControlLabel control={<Switch checked={showImplied} onChange={handleShowImpliedChange} />} label="Show implied consumers" />
                <Tooltip title="Implied consumers are consumers that are not explicitly defined but result from other explicitly defined relationships.">
                    <InfoIcon />
                </Tooltip>
            </Toolbar>
            {!isLoading && consumers.length === 0 &&
                <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                    {showImplied && "Element has no consumers"}
                    {!showImplied && "Element has no explicitly defined consumers"}
                </Typography>
            }
            {!isLoading && consumers.length > 0 &&
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
                        {consumers.map((consumer) => (
                            <TableRow
                                key={consumer.id}>
                                <TableCell align="right">
                                    <Link align="right" component="button"
                                        variant="body2" onClick={() => goToElement(consumer.id)}>
                                        {consumer.name}
                                    </Link>
                                </TableCell>
                                <TableCell align="right">{getTerminologyTerm(consumer.type, viewConfiguration.value?.terminology)}</TableCell>
                                <TableCell align="right">{consumer.relationDescription}</TableCell>
                                <TableCell align="right">{consumer.relationTechnology}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            }
        </CardContent>
        {isLoading && <Loader />}
    </Card>)
}