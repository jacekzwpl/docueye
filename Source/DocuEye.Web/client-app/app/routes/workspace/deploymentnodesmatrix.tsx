import DeploymentNodesMatrix from "~/views/workspace/deploymentnodesmatrix";
import type { Route } from "./+types/deploymentnodesmatrix";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceDeploymentNodesMatrixRoute() {
  return <DeploymentNodesMatrix />;
}
