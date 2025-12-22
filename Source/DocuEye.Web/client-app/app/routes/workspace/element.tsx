import ElementView from "~/views/workspace/element";
import type { Route } from "./+types/element";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function WorkspaceElementRoute() {
  return <ElementView />;
}
