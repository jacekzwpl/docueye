import { ElementsListView } from "~/views/workspace/elements/ElementsListView";
import type { Route } from "./+types/elements";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function WorkspaceElementsRoute() {
  return <ElementsListView />;
}
