import type { DeploymentNodeRelationship } from "../../../api/docueye-api";

export interface IRelationshipDescriptionDialogProps {
    open: boolean;
    onClose: () => void;
    data: {fromSource?: DeploymentNodeRelationship, fromDestination?: DeploymentNodeRelationship};
}