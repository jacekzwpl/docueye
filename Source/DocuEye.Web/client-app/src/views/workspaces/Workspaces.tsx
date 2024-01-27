import { Card, CardContent, Link, Table, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import DocuEyeApi from "../../api";
import { FoundedWorkspace } from "../../api/docueye-api";

export const Workspaces = () => {

    const [workspaces, setWorkspaces] = useState<FoundedWorkspace[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        DocuEyeApi.WorkspacesApi.apiWorkspacesGet()
            .then((response: AxiosResponse<FoundedWorkspace[]>) => {
                setWorkspaces(response.data);
            })

    }, [setWorkspaces]);

    const goToWorkspace = (id: string | null | undefined, name: string | null | undefined) => {
        if (!id || !name) {
            return;
        }
        navigate('/workspace/' + id + '/elements');
    }

    return (
        <Card variant="outlined">
            <CardContent>
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
            </CardContent>
        </Card>
    );
};