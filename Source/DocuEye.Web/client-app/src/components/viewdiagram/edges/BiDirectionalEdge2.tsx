import { useCallback } from 'react';
import { useStore, EdgeLabelRenderer } from 'reactflow';
import { getEdgeParams } from './utils';


export const getSpecialPath = (
  { sourceX, sourceY, targetX, targetY }: any,
  offset: number, labelOffset: number
) => {
  const centerX = (sourceX + targetX) / 2;
  const centerY = (sourceY + targetY) / 2;

  let xOffset;
  let yOffset;
  let xLabelOffset;
  let yLabelOffset;
  if (Math.abs(sourceX - targetX) < 40) {
    xOffset = sourceY < targetY ? offset : (-1 * offset);
    yOffset = 0;
    xLabelOffset = sourceY < targetY ? labelOffset : (-1 * labelOffset);
    yLabelOffset = sourceY < targetY ? 10 : 0;
  } else {
    xOffset = 0;
    yOffset = sourceX < targetX ? offset : (-1 * offset);
    yLabelOffset = sourceX < targetX ? labelOffset : (-1 * labelOffset);
    xLabelOffset = 0;
  }


  return [`M ${sourceX} ${sourceY} Q ${centerX + xOffset} ${centerY + yOffset} ${targetX} ${targetY}`, centerX + xLabelOffset, centerY + yLabelOffset] as const;
};

const BiDirectionalEdge2 = ({ id, source, target, markerEnd, style, label, labelWidth }: any) => {
  const sourceNode = useStore(useCallback((store) => store.nodeInternals.get(source), [source]));
  const targetNode = useStore(useCallback((store) => store.nodeInternals.get(target), [target]));



  if (!sourceNode || !targetNode) {
    return null;
  }

  const { sx, sy, tx, ty } = getEdgeParams(sourceNode, targetNode);
  const [edgePath, labelX, labelY] = getSpecialPath({ sourceX: sx, sourceY: sy, targetX: tx, targetY: ty }, 100, 50);


  return (
    <>
      <path
        id={id}
        className="react-flow__edge-path"
        d={edgePath}
        markerEnd={markerEnd}
        style={style}
      />
      {label && <EdgeLabelRenderer>
        <div
          style={{
            position: 'absolute',
            transform: `translate(-50%, -50%) translate(${labelX}px,${labelY}px)`,
            maxWidth: labelWidth ?? 100,
            fontSize: style.fontSize ?? 10,
            fontWeight: 300,
          }}
        >
          {label}
        </div>
      </EdgeLabelRenderer>}
    </>

  );
}

export default BiDirectionalEdge2;
