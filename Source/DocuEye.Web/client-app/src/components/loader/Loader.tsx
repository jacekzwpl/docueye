import { Backdrop, CircularProgress } from "@mui/material";
import { ILoaderProps } from "./ILoaderProps";
export const Loader = (props: ILoaderProps) => {

    const position = props.position === undefined ? "absolute" : props.position
    return (
        <Backdrop
            sx={{ position: position, color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1000 }}
            open={true}
        >
            <CircularProgress color="inherit" />
        </Backdrop>
    );
};