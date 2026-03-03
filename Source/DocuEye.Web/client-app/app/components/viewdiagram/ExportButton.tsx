import { IconButton } from "@mui/material";
import { toPng } from "html-to-image";
import { getNodesBounds, getViewportForBounds, useReactFlow } from "@xyflow/react";
import ImageIcon from '@mui/icons-material/Image';
const ExportButton = (props: {elementId?: string}) => {

    const { getNodes } = useReactFlow();
    const imageWidth = 1024;
    const imageHeight = 768;

    const exportToPng = () => {
        if(props.elementId && props.elementId !== "") { 
            const element = document.getElementById(props.elementId);
            element?.clientHeight
            if (!element) return;
            toPng(element as HTMLElement, {
                backgroundColor: '#FFFFFF',
                width: element?.clientWidth,
                height: element?.clientHeight + 10,
                pixelRatio: 2,
                style: {
                    width: `${element?.clientWidth}`,
                    height: `${element?.clientHeight + 10}`
                },
            }).then(downloadImage);
        }else {
            const nodesBounds = getNodesBounds(getNodes());
            const transform = getViewportForBounds(nodesBounds, imageWidth, imageHeight, 0.5, 2, {});
            const element = document.getElementsByClassName('react-flow__viewport')[0];
            if (!element) return;
            toPng(element as HTMLElement, {
                backgroundColor: '#FFFFFF',
                width: imageWidth,
                height: imageHeight,
                pixelRatio: 2,
                style: {
                    width: `${imageWidth}`,
                    height: `${imageHeight}`,
                    transform: `translate(${transform.x}px, ${transform.y}px) scale(${transform.zoom})`,
                },
            }).then(downloadImage);
        }
        
        //console.log(element);
        
    }

    const downloadImage = (dataUrl: string) => {
        const a = document.createElement('a');
        a.setAttribute('download', 'docueye-diagram.png');
        a.setAttribute('href', dataUrl);
        a.click();
    }
    return (
        <IconButton aria-label="export to png" size="small" onClick={exportToPng}>
            <ImageIcon fontSize="small" />
        </IconButton>
    );
};

export default ExportButton;