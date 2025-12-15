
//import { AccountCircle, Logout, Settings } from '@mui/icons-material';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import PowerSettingsNewIcon from '@mui/icons-material/PowerSettingsNew';
import { Box,  Divider, IconButton, Toolbar, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import './App.css';
import MainAppBar from './components/main/mainappbar';
import MainDrawer from './components/main/maindrawer';
import MainMenu from './components/main/mainmenu';
import Router from './router';
import { initResponseInterceptors } from './api/interceptors';
import { IWorkspaceState } from './store/slices/workspace/IWorkspaceState';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import globalRouter from './router/globalRouter';





const App = () => {

  const currentWorkspace: IWorkspaceState =
    useSelector((state: any) => state.workspace);
  const [menuOpened, setMenuOpened] = useState(true);
  //const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const toogleOpen = (): void => {
    setMenuOpened(!menuOpened);
  };
  const navigate = useNavigate();
  globalRouter.navigate = navigate;
  /*
  const handleMenu = (event: React.MouseEvent<HTMLElement>): void => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = (): void => {
    setAnchorEl(null);
  };
*/
  const drawerWidth: number = 240;

  const logout = (): void => {
    
    if (process.env.REACT_APP_SERVER_URL) {
      window.location.href = process.env.REACT_APP_SERVER_URL + "/auth/logout";
    } else {
      window.location.href = "/auth/logout";
    }
  }

  useEffect(() => {
    initResponseInterceptors();
  }, [])

  return (
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
            DocuEYE {currentWorkspace.value && ` (${currentWorkspace.value.name})`}
          </Typography>
          <div>
            <IconButton onClick={() => logout()} aria-label="sign out"><PowerSettingsNewIcon/></IconButton>
          </div>
          {/*<div>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={(evt) => handleMenu(evt)}
              color="inherit"
            >
              <AccountCircle />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorEl}
              anchorOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              open={Boolean(anchorEl)}
              onClose={() => handleClose()}
            >
              <MenuItem></MenuItem>
              <Divider />
              <MenuItem ><ListItemIcon>
                <Settings fontSize="small" />
              </ListItemIcon>Mój profil</MenuItem>
              <MenuItem >
                <ListItemIcon>
                  <Logout fontSize="small" />
                </ListItemIcon>Wyloguj</MenuItem>
            </Menu>
          </div>*/}
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

      ><Toolbar /><Router /></Box>
      {/*isLoading && <Loader />*/}
    </Box>
  );



}

export default App;
