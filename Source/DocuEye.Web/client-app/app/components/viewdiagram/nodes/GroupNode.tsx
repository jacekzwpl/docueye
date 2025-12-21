import { NodeResizer } from "@xyflow/react";
import { memo } from "react";

const GroupNode = ({data, selected}: any) => {

  return (
    <>
      <NodeResizer
        color="#ff0071"
        isVisible={selected}
      />
      <div style={{ fontSize: 11 }}>{data.label}</div>
    </>
  )
};

export default memo(GroupNode);