import ModelChanges from "~/views/workspace/modelchanges";
import type { Route } from "./+types/modelchanges";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function WorkspaceModelChangesRoute() {
  return <ModelChanges />;
}
