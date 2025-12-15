import { Box, Divider, IconButton, Toolbar, Typography } from "@mui/material";
import { SnackbarProvider } from "notistack";
import { Provider } from "react-redux";
import { Outlet, useNavigate } from "react-router";
import store from "~/store";
import MainAppBar from "../mainappbar";
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import PowerSettingsNewIcon from '@mui/icons-material/PowerSettingsNew';
import { useState } from "react";
import MainDrawer from "../maindrawer";
import MainMenu from "../mainmenu";

export const Main = () => {

  const drawerWidth: number = 240;
  const logout = (): void => {

    if (process.env.REACT_APP_SERVER_URL) {
      window.location.href = process.env.REACT_APP_SERVER_URL + "/auth/logout";
    } else {
      window.location.href = "/auth/logout";
    }
  };

  const [menuOpened, setMenuOpened] = useState(true);
  //const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const toogleOpen = (): void => {
    setMenuOpened(!menuOpened);
  };
  const navigate = useNavigate();

  return (
    <Provider store={store}>
      <SnackbarProvider
        autoHideDuration={3000}
        anchorOrigin={{ horizontal: 'right', vertical: 'top' }}
        maxSnack={3}>
        <Box sx={{ display: 'flex' }}>
          <MainAppBar position="absolute" sx={{ boxShadow: 0, borderBottom: 1, borderColor: "#e7e7e7" }} color="default" open={menuOpened} drawerWidth={drawerWidth}>
            <Toolbar>
              <IconButton onClick={() => toogleOpen()}
                size="large"
                edge="start"
                color="inherit"
                aria-label="menu"
                sx={{ mr: 2 }}
              >
                <MenuIcon />
              </IconButton>
              <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                DocuEYE {/*currentWorkspace.value && ` (${currentWorkspace.value.name})` */}
              </Typography>
              <div>
                <IconButton onClick={() => logout()} aria-label="sign out"><PowerSettingsNewIcon /></IconButton>
              </div>
            </Toolbar>
          </MainAppBar>
          <MainDrawer variant="permanent" open={menuOpened} drawerWidth={drawerWidth}>
            <Toolbar
              sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'flex-end',
                px: [1],
              }}
            >
              <IconButton onClick={() => toogleOpen()} >
                <ChevronLeftIcon />
              </IconButton>
            </Toolbar>
            <Divider />
            <MainMenu />
          </MainDrawer>
          <Box
            component="main"
            sx={{
              backgroundColor: (theme) =>
                theme.palette.mode === 'light'
                  ? theme.palette.grey[100]
                  : theme.palette.grey[900],
              flexGrow: 1,
              height: '100vh',
              overflow: 'auto',
              background: '#FFFFFF'
            }}

          ><Toolbar /><Outlet /></Box>
          {/*isLoading && <Loader />*/}
        </Box>
      </SnackbarProvider>
    </Provider>
  );

};