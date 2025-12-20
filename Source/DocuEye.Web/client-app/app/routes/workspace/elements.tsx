import { ElementsListView } from "~/views/workspace/elements/ElementsListView";
import type { Route } from "./+types/elements";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function WorkspaceElementsRoute() {
  return <ElementsListView />;
}
