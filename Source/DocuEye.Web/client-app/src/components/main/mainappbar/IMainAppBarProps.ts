import { AppBarProps } from "@mui/material";

export interface IMainAppBarProps extends AppBarProps {
    open?: boolean;
    drawerWidth?: number;
}