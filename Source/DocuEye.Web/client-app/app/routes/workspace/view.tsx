
import { WorkspaceView } from "~/views/workspace/view/WorkspaceView";
import type { Route } from "./+types/view";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceViewRoute() {
  return <WorkspaceView />;
}
