import { memo } from "react";
import { Handle, Position } from "reactflow";
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import LinkIcon from '@mui/icons-material/Link';
import TopicIcon from '@mui/icons-material/Topic';
import { IconButton } from "@mui/material";
import CustomNodeShapeIcon from "./CustomNodeShapeIcon";
import { useNavigate } from "react-router-dom";
import store from "../../../store";


const CustomNode = (nodeData: any) => {
  
  const navigate = useNavigate();
  
  const onGoToElementClick = (elementId: string) => {
    const viewConfigurationState = store.getState().viewConfiguration;
    navigate(`/workspace/${viewConfigurationState.value?.id}/element/${elementId}`);
  }

  const onGoToDiagramClick = (diagramId: string) => {
    const viewConfigurationState = store.getState().viewConfiguration;
    navigate(`/workspace/${viewConfigurationState.value?.id}/view/${diagramId}`);
  }

  const openUrl = (url:string) => {
    window.open(url, '_blank', 'noopener,noreferrer');
  }

  const diplayNodeType = (type:string, technology?:string) : string => {
    if(technology) {
      return `[${type}: ${technology}]`;
    }
    return `[${type}]`;
  }

  const titleFontSize = (fontSize: number | null) => {
    return fontSize ? fontSize + 1 : 12;
  }

  const metadataFontSize = (fontSize: number | null) => {
    return fontSize ? fontSize - 2 : 9;
  }

  const calcStrokeWidth = (strokeWidth: number | null | undefined, border: string | null | undefined) => {
    if(strokeWidth) {
      return strokeWidth;
    }
    if(!strokeWidth && border && border !== "none") {
      return 1;
    }
    return 0;
  }
 
  

  return (
    <>
      <div style={{
        width: 170,
        height: 110,
        textAlign: 'center', 
        backgroundColor: nodeData.data.style?.background ?? '#999999',//'rgb(17, 104, 189)', //'rgb(17, 104, 189)',
        verticalAlign: 'middle',
        display: 'table-cell',
        border: calcStrokeWidth(nodeData.data.style?.strokeWidth, nodeData.data.style?.border),//nodeData.data.style?.strokeWidth ?? 0,
        borderColor: nodeData.data.style?.stroke ?? '#999999',
        borderStyle: nodeData.data.style?.border ? 'solid' : 'none',
        opacity: nodeData.data.style?.opacity ? nodeData.data.style?.opacity/100 : 1
      }} >
        {nodeData.data.style?.shape && <CustomNodeShapeIcon color={nodeData.data.style?.color} shape={nodeData.data.style.shape} />}
        {!nodeData.data.style?.shape && nodeData.data.style?.icon &&
        <div style={{ 
          fontSize: 12, 
          fontWeight: 'bold', 
          display: 'block', 
          margin: '0 auto', 
          padding: 1 }}>
            <img alt="" width={24} src={nodeData.data.style?.icon} />
        </div>
        }
        <div style={{ 
          fontSize: titleFontSize(nodeData.data.style?.fontSize), 
          fontWeight: 'bold', 
          color: nodeData.data.style?.color ?? '#FFFFFF', 
          display: 'block', 
          margin: '0 auto', 
          padding: 1 }}>{nodeData.data.name}</div>
        {nodeData.data.style?.metadata && <div style={{ 
          fontSize: metadataFontSize(nodeData.data.style?.fontSize), 
          color: nodeData.data.style?.color ?? '#FFFFFF', 
          display: 'block', 
          margin: '0 auto', 
          padding: 1 }}>{diplayNodeType(nodeData.data.typeName,nodeData.data.technology)}</div>}
        <div style={{ 
          fontSize: metadataFontSize(nodeData.data.style?.fontSize), 
          color: nodeData.data.style?.color ?? '#FFFFFF', 
          display: 'block', 
          margin: '0 auto', 
          padding: 1 }}>
          <IconButton aria-label="go to element" size="small" onClick={() => onGoToElementClick(nodeData.data.instanceOfId ? nodeData.data.instanceOfId : nodeData.id)}>
            <TopicIcon sx={{color: nodeData.data.style?.color ?? '#FFFFFF'}} fontSize="small" />
          </IconButton>
          { nodeData.data.diagramId && <IconButton aria-label="go to diagram" size="small" onClick={() => onGoToDiagramClick(nodeData.data.diagramId)}>
            <AddCircleOutlineIcon sx={{color: nodeData.data.style?.color ?? '#FFFFFF'}} fontSize="small" />
          </IconButton> }
          { nodeData.data.url && <IconButton aria-label="go to url" size="small" onClick={() => openUrl(nodeData.data.url)}>
            <LinkIcon sx={{color: nodeData.data.style?.color ?? '#FFFFFF'}} fontSize="small" />
          </IconButton> }
        </div>
        {nodeData.data.style?.description && <div style={{ 
          fontSize: nodeData.data.style?.fontSize ?? 11, 
          color: nodeData.data.style?.color ?? '#FFFFFF', 
          display: 'block', 
          margin: '0 auto', 
          padding: 1 }}>{nodeData.data.description ?? ""}</div>}

      </div>
      <Handle id={nodeData.id + "handle1"} type="target" position={Position.Top} style={{opacity: 0}} className="w-16 !bg-teal-500" />
      <Handle id={nodeData.id + "handle2"} type="source" position={Position.Bottom} style={{opacity: 0}} className="w-16 !bg-teal-500" />
    </>
  )
};

export default memo(CustomNode);