export interface IPaginatorProps {
    elementsCount: number;
    pageSize: number;
    currentPage: number;
    onPreviousClick: (page: number) => void;
    onNextClick: (page: number) => void;
}