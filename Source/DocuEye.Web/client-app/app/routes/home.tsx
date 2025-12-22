import WorkspacesList from "~/views/workspaces";
import type { Route } from "./+types/home";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function Home() {
  return <WorkspacesList />;
}
