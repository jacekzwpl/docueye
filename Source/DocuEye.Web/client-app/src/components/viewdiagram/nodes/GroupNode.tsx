import { memo } from "react";

const GroupNode = (data: any) => {


  return (
    <>
      <div style={{ fontSize: 11 }}>{data.data.label}</div>
    </>
  )
};

export default memo(GroupNode);