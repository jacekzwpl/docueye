import { AppBar, styled } from "@mui/material";
import { IMainAppBarProps } from "./IMainAppBarProps";

export const MainAppBar = styled(AppBar, {
  shouldForwardProp: (prop) => { return prop !== 'open' && prop !== 'drawerWidth' },
})<IMainAppBarProps>(({ theme, open, drawerWidth }) => ({
  zIndex: theme.zIndex.drawer + 1,
  transition: theme.transitions.create(['width', 'margin'], {
    easing: theme.transitions.easing.sharp,
    duration: theme.transitions.duration.leavingScreen,
  }),
  ...(open && {
    marginLeft: drawerWidth === undefined ? 240 : drawerWidth,
    width: `calc(100% - ${drawerWidth === undefined ? 240 : drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  }),
}));



