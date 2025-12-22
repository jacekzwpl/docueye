
import type { Route } from "./+types/graph";
import GraphView from "~/views/workspace/graph";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function WorkspaceGraphRoute() {
  return <GraphView />;
}
