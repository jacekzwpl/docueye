import type { Route } from "./+types/decision";
import { DecisionView } from "~/views/workspace/decision/DecisionView";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceDecisionRoute() {
  return <DecisionView />;
}
