import { IconButton, Stack } from "@mui/material"
import NavigateNextIcon from '@mui/icons-material/NavigateNext';
import NavigateBeforeIcon from '@mui/icons-material/NavigateBefore';
import { IPaginatorProps } from "./IPaginatorProps";
export const Paginator = (props: IPaginatorProps) => {

    const onNextPage = (currentPage: number) => {
        if (props.onNextClick) {
            props.onNextClick(currentPage + 1);
        }
    }

    const onPreviousPage = (currentPage: number) => {
        if (currentPage === 1) {
            return;
        }
        if (props.onPreviousClick) {
            props.onPreviousClick(currentPage - 1);
        }
    }

    return (
        <Stack direction="row" textAlign={'center'} width="100%" spacing={1}>
            <IconButton onClick={() => onPreviousPage(props.currentPage)} disabled={props.currentPage === 1} aria-label="go previous">
                <NavigateBeforeIcon />
            </IconButton>
            <IconButton onClick={() => onNextPage(props.currentPage)} disabled={props.pageSize > props.elementsCount} aria-label="go next">
                <NavigateNextIcon />
            </IconButton>
        </Stack>)
}