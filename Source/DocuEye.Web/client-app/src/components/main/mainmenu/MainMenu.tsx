import { List, ListItemButton, ListItemIcon, ListItemText } from "@mui/material";
import { useNavigate } from "react-router-dom";
import DashboardIcon from '@mui/icons-material/Dashboard';
import HistoryIcon from '@mui/icons-material/History';
import AppsIcon from '@mui/icons-material/Apps';
import BatchPredictionIcon from '@mui/icons-material/BatchPrediction';
import AssignmentIcon from '@mui/icons-material/Assignment';
import AccountTreeIcon from '@mui/icons-material/AccountTree';
import BubbleChartIcon from '@mui/icons-material/BubbleChart';
import PivotTableChartIcon from '@mui/icons-material/PivotTableChart';
import { IWorkspaceState } from "../../../store/slices/workspace/IWorkspaceState";
import { useSelector } from "react-redux";

export const MainMenu = () => {

  const currentWorkspace: IWorkspaceState =
        useSelector((state: any) => state.workspace);

  const navigate = useNavigate();

  const goToPage = (page: string) => {
    navigate(page);
  };

  return (
    <List>
      <ListItemButton onClick={() => goToPage('/')}>
        <ListItemIcon>
          <DashboardIcon />
        </ListItemIcon>
        <ListItemText primary="Workspaces" />
      </ListItemButton>
      {currentWorkspace.value &&
      <ListItemButton onClick={() => goToPage('/workspace/' + currentWorkspace.value?.id + '/view')}>
        <ListItemIcon>
          <AccountTreeIcon />
        </ListItemIcon>
        <ListItemText primary="Diagrams" />
      </ListItemButton> }
      {currentWorkspace.value &&
      <ListItemButton onClick={() => goToPage('/workspace/' + currentWorkspace.value?.id + '/graph')}>
        <ListItemIcon>
          <BubbleChartIcon />
        </ListItemIcon>
        <ListItemText primary="Graphs" />
      </ListItemButton> }
      {currentWorkspace.value &&
      <ListItemButton onClick={() => goToPage('/workspace/' + currentWorkspace.value?.id + '/documentation')}>
        <ListItemIcon>
          <AssignmentIcon />
        </ListItemIcon>
        <ListItemText primary="Documentation" />
      </ListItemButton> }
      {currentWorkspace.value &&
      <ListItemButton onClick={() => goToPage('/workspace/' + currentWorkspace.value?.id + '/decisions')}>
        <ListItemIcon>
          <BatchPredictionIcon />
        </ListItemIcon>
        <ListItemText primary="Decisions" />
      </ListItemButton> }
      {currentWorkspace.value &&
      <ListItemButton onClick={() => goToPage('/workspace/' + currentWorkspace.value?.id + '/elements')}>
        <ListItemIcon>
          <AppsIcon />
        </ListItemIcon>
        <ListItemText primary="Elements" />
      </ListItemButton> }
      {currentWorkspace.value &&
      <ListItemButton onClick={() => goToPage('/workspace/' + currentWorkspace.value?.id + '/deploymentnodesmatrix')}>
        <ListItemIcon>
          <PivotTableChartIcon />
        </ListItemIcon>
        <ListItemText primary="Nodes Matrix" />
      </ListItemButton> }
      {currentWorkspace.value &&
      <ListItemButton onClick={() => goToPage('/workspace/' + currentWorkspace.value?.id + '/modelchanges')}>
        <ListItemIcon>
          <HistoryIcon />
        </ListItemIcon>
        <ListItemText primary="Model Changes" />
      </ListItemButton> }
    </List>
  );
};

