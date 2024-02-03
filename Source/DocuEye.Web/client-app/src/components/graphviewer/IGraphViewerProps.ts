import { ViewConfiguration } from "../../api/docueye-api";

export interface IGraphViewerProps {
    selectedView: any;
    workspaceId: string | null | undefined;
    viewConfiguration?: ViewConfiguration | null;
}