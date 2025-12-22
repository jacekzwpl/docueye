import DecisionsView from "~/views/workspace/decisions";
import type { Route } from "./+types/decisions";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceDecisionsRoute() {
  return <DecisionsView />;
}
