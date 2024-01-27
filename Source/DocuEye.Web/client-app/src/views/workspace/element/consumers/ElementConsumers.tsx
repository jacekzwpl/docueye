import { Card, CardContent, Link, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import DocuEyeApi from "../../../../api";
import { ElementConsumer } from "../../../../api/docueye-api";
import Loader from "../../../../components/loader";
import { IViewConfigurationState } from "../../../../store/slices/viewConfiguration/IViewConfigurationState";
import { getTerminologyTerm } from "../../../../terminology/getTerminologyTerm";
import { IElementConsumers } from "./IElementConsumers";

export const ElementConsumers = (props: IElementConsumers) => {
    const navigate = useNavigate();
    const viewConfiguration: IViewConfigurationState =
        useSelector((state: any) => state.viewConfiguration);

    const [consumers, setConsumers] = useState<ElementConsumer[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
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
            .apiWorkspacesWorkspaceIdElementsIdConsumersGet(props.element.workspaceId, props.element.id)
            .then((response: AxiosResponse<ElementConsumer[]>) => {
                setConsumers(response.data);
            }).finally(() => {
                setIsLoading(false);
            })
    }, [props, setConsumers, setIsLoading])

    return (<Card variant="outlined">
        <CardContent>
            {!isLoading && consumers.length === 0 &&
                <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                    Element has no children
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