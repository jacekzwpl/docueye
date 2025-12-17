export interface IElementDiagramsDialogProps {
    open: boolean;
    workspaceId?: string | null;
    elementId?: string | null;
    onClose: (diagrmaId?: string | null) => void;
} 