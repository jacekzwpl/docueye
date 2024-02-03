import { Box, Toolbar } from "@mui/material";
import { useEffect, useRef } from "react";
import { createGraph } from "../../components/graphviewer/functions/createGraph";

const nodesData = [
    {
        "id": 1,
        "name": "Andy",
        "gender": "male"
    },
    {
        "id": 2,
        "name": "Betty",
        "gender": "female"
    },
    {
        "id": 3,
        "name": "Cate",
        "gender": "female"
    },
    {
        "id": 4,
        "name": "Dave",
        "gender": "male"
    },
    {
        "id": 5,
        "name": "Ellen",
        "gender": "female"
    },
    {
        "id": 6,
        "name": "Fiona",
        "gender": "female"
    },
    {
        "id": 7,
        "name": "Garry",
        "gender": "male"
    },
    {
        "id": 8,
        "name": "Holly",
        "gender": "female"
    },
    {
        "id": 9,
        "name": "Iris",
        "gender": "female"
    },
    {
        "id": 10,
        "name": "Jane",
        "gender": "female"
    }
]
const linksData = [
    {
        "source": 1,
        "target": 2
    },
    {
        "source": 1,
        "target": 5
    },
    {
        "source": 1,
        "target": 6
    },
    {
        "source": 2,
        "target": 3
    },
    {
        "source": 2,
        "target": 7
    },
    {
        "source": 3,
        "target": 4
    },
    {
        "source": 8,
        "target": 3
    },
    {
        "source": 4,
        "target": 5
    },
    {
        "source": 4,
        "target": 9
    },
    {
        "source": 5,
        "target": 10
    },{
        "source": 10,
        "target": 5
    }
]

export const Graph = () => {

    const container = useRef<any>();

    useEffect(() => {

        let destroyFn;

        if (container.current) {
            console.log("aa")
            const { destroy } = createGraph(container.current, nodesData, linksData);
            destroyFn = destroy;
        }
        return destroyFn;

    }, [container])

    return (
        <Box padding={2} >
            <Box paddingBottom={2} >
                <Toolbar>
                </Toolbar>
            </Box>
            <Box ref={container} padding={2} height={'calc(100vh - 168px)'} >

            </Box>
        </Box>
    );
};