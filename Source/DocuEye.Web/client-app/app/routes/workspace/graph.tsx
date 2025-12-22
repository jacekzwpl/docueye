
import type { Route } from "./+types/graph";
import GraphView from "~/views/workspace/graph";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceGraphRoute() {
  return <GraphView />;
}
