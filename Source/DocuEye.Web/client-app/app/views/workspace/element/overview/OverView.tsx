import { Button, Card, CardContent, Chip, Grid, IconButton, Link, Stack, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material"
import type { IOverViewProps } from "./IOverViewProps";
import GitHubIcon from '@mui/icons-material/GitHub';
import InsertLinkIcon from '@mui/icons-material/InsertLink';
import type { IViewConfigurationState } from "../../../../store/slices/viewConfiguration/IViewConfigurationState";
import { useSelector } from "react-redux";
import { getTerminologyTerm } from "../../../../terminology/getTerminologyTerm";
import { useNavigate } from "react-router";
import { useEffect, useState } from "react";
import type { ChildElement, Element } from "../../../../api/docueye-api";
import DocuEyeApi from "../../../../api";
import TroubleshootIcon from '@mui/icons-material/Troubleshoot';
import ElementDiagramsDialog from "../../../../components/elementdiagramsdialog";
import ContentCopyIcon from '@mui/icons-material/ContentCopy';

export const OverView = (props: IOverViewProps) => {
    const navigate = useNavigate();
    const viewConfiguration: IViewConfigurationState =
        useSelector((state: any) => state.viewConfiguration);

    const [children, setChildren] = useState<ChildElement[]>([]);
    const [parentElement, setParentElement] = useState<Element | null>(null)
    const goToElement = (id: string | null | undefined) => {
        if (!id) {
            return;
        }
        navigate('/workspace/' + props.element.workspaceId + '/element/' + id);
    }
    const [isDiagramsDialogOpen, setIsDiagramsDialogOpen] = useState<boolean>(false);

    const openUrl = (url: string | null | undefined) => {
        if (!url) {
            return;
        }
        window.open(url, '_blank', 'noopener,noreferrer');
    }

    const onDiagramDialogClose = (diagrmaId?: string | null) => {
        setIsDiagramsDialogOpen(false);
        if (diagrmaId) {
            navigate(`/workspace/${props.element.workspaceId}/view/${diagrmaId}`);
        }
    }

    useEffect(() => {
        if (!props.element.workspaceId || !props.element.id) {
            return;
        }
        const getChildren = DocuEyeApi.ElementsApi
            .apiWorkspacesWorkspaceIdElementsIdChildrenGet(props.element.workspaceId, props.element.id)
        const getParent = props.element.parentId ?
            DocuEyeApi.ElementsApi.apiWorkspacesWorkspaceIdElementsIdGet(props.element.workspaceId, props.element.parentId)
            : Promise.resolve({ data: null });
        Promise.all([getChildren, getParent])
            .then((responses) => {
                const getChildrenResponse = responses[0];
                const getParentResponse = responses[1];
                setChildren(getChildrenResponse.data);
                setParentElement(getParentResponse.data);
            }).finally(() => {
                //setIsLoading(false);
                //setSelectedTab('0');
            });
    }, [props, setChildren])

    const findInDiagrams = () => {
        if (!props.element.workspaceId || !props.element.id) {
            return;
        }

        setIsDiagramsDialogOpen(true);
    }

    const copyToClipBoard = (text: string) => {
        navigator.clipboard.writeText(text);
    }

    return (
        <>
            <Grid container spacing={2}>
                <Grid size={6} >
                    <Card variant="outlined">
                        <CardContent>
                            <Typography sx={{ fontSize: 20 }} color="text.primary" gutterBottom>
                                {props.element.name} [{getTerminologyTerm(props.element.type, viewConfiguration.value?.terminology)}]
                            </Typography>
                            <Grid container>
                                {props.element.description && props.element.description !== "" &&
                                    <Grid size={12}>
                                        <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                            Description
                                        </Typography>
                                        <Typography sx={{ fontSize: 12 }} color="text.secondary" gutterBottom>
                                            {props.element.description}
                                        </Typography>
                                    </Grid>}

                                <Grid size={6}>
                                    {props.element.technology && props.element.technology !== "" &&
                                        <><Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                            Technology
                                        </Typography>
                                            <Typography sx={{ fontSize: 12 }} color="text.secondary" gutterBottom>
                                                {props.element.technology}
                                            </Typography></>}
                                </Grid>
                                <Grid size={6}>
                                    {props.element.ownerTeam && props.element.ownerTeam !== "" && <>
                                        <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                            Owner Team
                                        </Typography>
                                        <Typography sx={{ fontSize: 12 }} color="text.secondary" gutterBottom>
                                            {props.element.ownerTeam}
                                        </Typography></>}
                                </Grid>
                                <Grid size={6}>
                                    <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                        DSL Identifier
                                    </Typography>
                                    <Typography sx={{ fontSize: 12 }} color="text.secondary" gutterBottom>
                                        {props.element.dslId} <IconButton aria-label="copy" size="small" onClick={() => copyToClipBoard(props.element.dslId ?? "")}>
                                            <ContentCopyIcon fontSize="small" />
                                        </IconButton>
                                    </Typography>
                                </Grid>
                                <Grid size={6}>
                                    {parentElement && <>
                                        <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                            Parent
                                        </Typography>
                                        <Typography sx={{ fontSize: 12 }} color="text.secondary" gutterBottom>
                                            <Link component="button"
                                                variant="body2"
                                                onClick={() => goToElement(parentElement?.id)}
                                            >
                                                {parentElement?.name}
                                            </Link>
                                        </Typography></>}
                                </Grid>
                                <Grid size={12}>
                                    <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                        Tags
                                    </Typography>
                                    {props.element.tags?.map((tag, index) => (
                                        <Chip key={index} label={tag} variant="outlined" />
                                    ))}

                                </Grid>
                            </Grid>
                        </CardContent>
                    </Card>
                </Grid>
                <Grid size={6}>
                    <Card variant="outlined">
                        <CardContent>
                            <Stack direction="row" spacing={2}>
                                <Button disabled={!props.element.sourceCodeUrl} onClick={() => openUrl(props.element.sourceCodeUrl)} variant="outlined" startIcon={<GitHubIcon />}>
                                    Source Code
                                </Button>
                                <Button disabled={!props.element.url} onClick={() => openUrl(props.element.url)} variant="outlined" startIcon={<InsertLinkIcon />}>
                                    Read more
                                </Button>
                                <Button variant="outlined" onClick={() => { findInDiagrams() }} startIcon={<TroubleshootIcon />}>
                                    Find in diagrams
                                </Button>
                            </Stack>
                        </CardContent>
                    </Card>
                </Grid>
                <Grid size={12}>
                    <Card variant="outlined">
                        <CardContent>
                            <Typography sx={{ fontSize: 20 }} color="text.primary" gutterBottom>
                                Children
                            </Typography>
                            {children.length === 0 &&
                                <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                    Element has no children
                                </Typography>
                            }
                            {children.length > 0 &&
                                <Table sx={{ minWidth: 650 }} aria-label="elements table">
                                    <TableHead>
                                        <TableRow>
                                            <TableCell sx={{ fontWeight: 'bold' }} align="right">Name</TableCell>
                                            <TableCell sx={{ fontWeight: 'bold' }} align="right">Type</TableCell>
                                            <TableCell sx={{ fontWeight: 'bold' }} align="right">Description</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {children.length === 0 &&
                                            <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                                                Element has no children
                                            </Typography>
                                        }
                                        {children.map((child) => (
                                            <TableRow
                                                key={child.id}
                                            //sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                            >
                                                <TableCell align="right">
                                                    <Link component="button"
                                                        variant="body2"
                                                        onClick={() => goToElement(child.instanceOfId ? child.instanceOfId : child.id)}
                                                    >
                                                        {child.name}
                                                    </Link>
                                                </TableCell>
                                                <TableCell align="right">{getTerminologyTerm(child.type, viewConfiguration.value?.terminology)}</TableCell>
                                                <TableCell align="right">{child.description}</TableCell>
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            }
                        </CardContent>
                    </Card>
                </Grid>
            </Grid>
            <ElementDiagramsDialog
                open={isDiagramsDialogOpen}
                elementId={props.element.id}
                workspaceId={props.element.workspaceId}
                onClose={onDiagramDialogClose} />
        </>
    )
}
