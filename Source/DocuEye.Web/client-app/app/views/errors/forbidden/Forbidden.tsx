import { Card, CardContent } from "@mui/material";

export const Forbidden = () => {

   
    return (
        <Card variant="outlined">
            <CardContent>
                <h1>Forbidden</h1>
                <p>You don't have permission to access this page.</p>
            </CardContent>
        </Card>
    );
};