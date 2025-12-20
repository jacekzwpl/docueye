import type { Route } from "./+types/documentation";
import DocumentationView from "~/views/workspace/documentation";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function WorkspaceDocumentationRoute() {
  return <DocumentationView />;
}
