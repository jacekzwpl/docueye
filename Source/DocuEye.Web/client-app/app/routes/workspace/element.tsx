import ElementView from "~/views/workspace/element";
import type { Route } from "./+types/element";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceElementRoute() {
  return <ElementView />;
}
