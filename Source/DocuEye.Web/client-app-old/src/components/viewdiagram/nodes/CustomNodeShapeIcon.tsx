
import { ICustomNodeShapeIconProps } from './ICustomNodeShapeIconProps';
import PersonIcon from '@mui/icons-material/Person';
import SmartToyIcon from '@mui/icons-material/SmartToy';
import FolderIcon from '@mui/icons-material/Folder';
import WebIcon from '@mui/icons-material/Web';
import StayCurrentPortraitIcon from '@mui/icons-material/StayCurrentPortrait';
import StayCurrentLandscapeIcon from '@mui/icons-material/StayCurrentLandscape';
import HexagonIcon from '@mui/icons-material/Hexagon';
import GridViewIcon from '@mui/icons-material/GridView';
const CustomNodeShapeIcon = (props: ICustomNodeShapeIconProps) => {

    
//Box|RoundedBox|Circle|Ellipse||Cylinder|Pipe|||||||

  return (
    <div style={{ 
        fontSize: 12, 
        fontWeight: 'bold', 
        color: props.color ?? '#FFFFFF', 
        display: 'block', 
        margin: '0 auto', 
        padding: 1 }}>
        {props.shape === "Person" && <PersonIcon fontSize='small' />}
        {props.shape === "Robot" && <SmartToyIcon fontSize='small' />}
        {props.shape === "Folder" && <FolderIcon fontSize='small' />}
        {props.shape === "WebBrowser" && <WebIcon fontSize='small' />}
        {props.shape === "MobileDevicePortrait" && <StayCurrentPortraitIcon fontSize='small' />}
        {props.shape === "MobileDeviceLandscape" && <StayCurrentLandscapeIcon fontSize='small' />}
        {props.shape === "Hexagon" && <HexagonIcon fontSize='small' />}
        {props.shape === "Component" && <GridViewIcon fontSize='small' />}
    </div>
  )
};

export default CustomNodeShapeIcon;