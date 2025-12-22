
import { Drawer, styled } from "@mui/material";
import type { IMainDrawerProps } from "./IMainDrawerProps";

export const MainDrawer = styled(Drawer, {
    shouldForwardProp: (prop) => { return prop !== 'open' && prop !== 'drawerWidth' }
})<IMainDrawerProps>(
    ({ theme, open, drawerWidth }) => ({
        '& .MuiDrawer-paper': {
            position: 'relative',
            whiteSpace: 'nowrap',
            width: drawerWidth === undefined ? 240 : drawerWidth,
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen,
            }),
            boxSizing: 'border-box',
            ...(!open && {
                overflowX: 'hidden',
                transition: theme.transitions.create('width', {
                    easing: theme.transitions.easing.sharp,
                    duration: theme.transitions.duration.leavingScreen,
                }),
                width: theme.spacing(7),
                [theme.breakpoints.up('sm')]: {
                    width: theme.spacing(9),
                },
            }),
        },
    }),
);

