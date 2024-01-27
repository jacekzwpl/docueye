import { useEffect, useMemo, useRef } from "react";
import { ImageViewerProps } from "./ImageViewerProps";

const ImageViewer = (props: ImageViewerProps) => {
    const canvasRef = useRef<any>(null);
    const background = useMemo(() => new Image(), []);
    useEffect(() => {
        background.src = props.image;

        if (canvasRef.current) {
            background.onload = () => {
                // Get the image dimensions
                const { width, height } = background;
                var rect = canvasRef.current.parentNode.getBoundingClientRect();
                const scale = rect.width / width;
                canvasRef.current.width = rect.width;
                canvasRef.current.height = height * scale;//rect.height
                // Set image as background
                canvasRef.current.getContext("2d").drawImage(background, 0, 0, width * scale, height * scale);
            };
        }


    }, [background, props.image]);

    return (
        <>
            <canvas ref={canvasRef} />
        </>
    );
};

export default ImageViewer;