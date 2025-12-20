import { memo } from "react";
import { Handle, Position } from "@xyflow/react";

const DeploymentNode = (nodeData: any) => {

  const diplayNodeName = (name:string, technology?:string, metadata?: boolean) : string => {
    if(technology && metadata) {
      return `${name} [${technology}]`;
    }
    return name;
  }


  return (
    <>
      <div style={{ 
        fontSize: nodeData.data.style.fontSize ?? 11,
        position: 'absolute',
        bottom: 0,
        left: 0 }}>
          {nodeData.data.style?.icon && <img alt={nodeData.data.name} style={{verticalAlign:'middle'}} width={16} src={nodeData.data.style?.icon} />}
          <span>{diplayNodeName(nodeData.data.name, nodeData.data.technology, nodeData.data.style.metadata)}</span>
        </div>
      <Handle id={nodeData.id + "handle1"} type="target" position={Position.Top} style={{opacity: 0}} className="w-16 !bg-teal-500" />
      <Handle id={nodeData.id + "handle2"} type="source" position={Position.Bottom} style={{opacity: 0}} className="w-16 !bg-teal-500" />
    </>
  )
};

export default memo(DeploymentNode);