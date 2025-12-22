import ModelChanges from "~/views/workspace/modelchanges";
import type { Route } from "./+types/modelchanges";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceModelChangesRoute() {
  return <ModelChanges />;
}
