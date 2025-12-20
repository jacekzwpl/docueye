import type { ViewConfiguration } from "../../api/docueye-api";

export interface IViewDiagramProps {
    selectedView: any;
    workspaceId: string | null | undefined;
    viewConfiguration?: ViewConfiguration | null;
}