import type { Route } from "./+types/documentation";
import DocumentationView from "~/views/workspace/documentation";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceDocumentationRoute() {
  return <DocumentationView />;
}
