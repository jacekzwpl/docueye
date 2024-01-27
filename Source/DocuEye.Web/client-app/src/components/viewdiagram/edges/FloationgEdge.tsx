import { useCallback } from 'react';
import { useStore, getBezierPath, EdgeLabelRenderer, Position, ReactFlowState } from 'reactflow';
import { getEdgeParams } from './utils';


const FloatingEdge = ({ id, source, target, markerEnd, style, label, labelWidth }: any) => {


  const sourceNode = useStore(useCallback((store) => store.nodeInternals.get(source), [source]));
  const targetNode = useStore(useCallback((store) => store.nodeInternals.get(target), [target]));
  const identicalIndex = useStore((s: ReactFlowState) => {
    const edgeExists = s.edges.filter(
      (e) => {
        return (e.source === source && e.target === target)
      }
    );

    return edgeExists.findIndex((e) => e.id === id);
  });

  const indenticalOffset = 20;

  if (!sourceNode || !targetNode) {
    return null;
  }
  const { sx, sy, tx, ty, sourcePos, targetPos } = getEdgeParams(sourceNode, targetNode);

  let nsx, nsy, ntx, nty, nsourcePos, ntargetPos;

  //if there is another connector beetween same nodes
  if (identicalIndex > 0) {
    //apply offset to conector
    nsx = sourcePos === Position.Top || sourcePos === Position.Bottom ? sx + indenticalOffset * identicalIndex : sx;
    nsy = sourcePos === Position.Left || sourcePos === Position.Right ? sy + indenticalOffset * identicalIndex : sy;
    ntx = targetPos === Position.Top || targetPos === Position.Bottom ? tx + indenticalOffset * identicalIndex : tx;
    if (targetPos === Position.Left) {
      nty = ty + indenticalOffset * identicalIndex;
    } else if (targetPos === Position.Right) {
      nty = ty + indenticalOffset * identicalIndex * (-1);
    } else {
      nty = ty;
    }

    nsourcePos = sourcePos;
    ntargetPos = targetPos;

    // After applying offset ther can be sytuation where connector source positoon or 
    // target position points out of node edge. 

    //Handle those situations


    if (nsx > (sourceNode.positionAbsolute?.x || 0) + (sourceNode.width ?? 0) && sourcePos === Position.Top) {
      nsourcePos = Position.Right;
      nsx = sx;
      nsy = sy + indenticalOffset * identicalIndex;
    }

    if (nty > (targetNode.positionAbsolute?.y || 0) + (targetNode.height ?? 0) && targetPos === Position.Left) {
      //console.log(id);
      ntargetPos = Position.Bottom;
      nty = (targetNode.positionAbsolute?.y || 0) + (targetNode.height ?? 0);//ty; //targetNode.position.y + (targetNode.height ?? 0)
      ntx = tx + indenticalOffset * identicalIndex
    }

    if (ntx > (targetNode.positionAbsolute?.x || 0) + (targetNode.width ?? 0) && targetPos === Position.Bottom) {
      ntargetPos = Position.Right;
      ntx = (targetNode.positionAbsolute?.x || 0) + (targetNode.width ?? 0)
      nty = ty + (indenticalOffset * identicalIndex * (-1))
    }

    if (nsy > (sourceNode.positionAbsolute?.y || 0) + (sourceNode.height ?? 0) && sourcePos === Position.Left) {
      //console.log(id);
      ntargetPos = Position.Bottom;
      nsy = (sourceNode.positionAbsolute?.y || 0) + (sourceNode.height ?? 0);//ty; //targetNode.position.y + (targetNode.height ?? 0)
      nsx = sx + indenticalOffset * identicalIndex
    }

    if (ntx > (targetNode.positionAbsolute?.x || 0) + (targetNode.width ?? 0) && targetPos === Position.Top) {
      ntargetPos = Position.Right;
      ntx = (targetNode.positionAbsolute?.x || 0) + (targetNode.width ?? 0)
      nty = ty + (indenticalOffset * identicalIndex);
    }

    if (nsx > (sourceNode.positionAbsolute?.x || 0) + (sourceNode.width ?? 0) && sourcePos === Position.Bottom) {
      nsourcePos = Position.Right;
      nsx = sx;
      nsy = sy + indenticalOffset * identicalIndex * (-1);
    }

    if (nsy > (sourceNode.positionAbsolute?.y || 0) + (sourceNode.height ?? 0) && sourcePos === Position.Right) {
      //console.log(id);
      ntargetPos = Position.Bottom;
      nsy = (sourceNode.positionAbsolute?.y || 0) + (sourceNode.height ?? 0);
      nsx = sx + indenticalOffset * identicalIndex * (-1);
    }

    if (nty < (targetNode.positionAbsolute?.y || 0) && targetPos === Position.Right) {
      ntargetPos = Position.Top;
      nty = targetNode.positionAbsolute?.y;
      ntx = tx + indenticalOffset * identicalIndex * (-1)
    }
  } else {
    nsx = sx;
    nsy = sy;
    ntx = tx;
    nty = ty;
    nsourcePos = sourcePos;
    ntargetPos = targetPos;
  }



  const [edgePath, labelX, labelY] = getBezierPath({
    sourceX: nsx,
    sourceY: nsy,
    sourcePosition: nsourcePos,
    targetPosition: ntargetPos,
    targetX: ntx,
    targetY: nty,
  });

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
            //background: '#ffcc00',
            //padding: 10,
            //borderRadius: 5,
            maxWidth: labelWidth ?? 100,
            textAlign: 'center',
            fontSize: style.fontSize ?? 10,
            fontWeight: 300,
          }}
        //className="nodrag nopan"
        >
          {label}
        </div>
      </EdgeLabelRenderer>}
      {/*
    <foreignObject
        width={40}
        height={40}
        x={labelX - 40 / 2}
        y={labelY - 40 / 2}
        className="edgebutton-foreignobject"
        requiredExtensions="http://www.w3.org/1999/xhtml"
      >
        <body>
          <button
            className="edgebutton"
            //onClick={(event) => onEdgeClick(event, id)}
          >
            R {label}
          </button>
        </body>
      </foreignObject>
        */}
    </>

  );
}

export default FloatingEdge;
