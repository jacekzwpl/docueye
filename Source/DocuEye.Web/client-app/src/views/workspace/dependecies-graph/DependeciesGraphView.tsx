import { Box } from "@mui/material";
import { useEffect, useRef } from "react";
import * as d3 from "d3";

export const DependeciesGraphView = () => {
    const container = useRef<any>();


    useEffect(() => {
        if (!container.current) {
            return;
        }
        console.log(container.current);
        const containerRect = container.current.getBoundingClientRect();
        const height = containerRect.height;
        const width = containerRect.width;
        const treeLayout = d3.tree().size([height, width]);
    }, [])





    return (
        <Box padding={2} >
            <Box ref={container} padding={2} height={'calc(100vh - 168px)'} >

            </Box>
        </Box>
    );
}