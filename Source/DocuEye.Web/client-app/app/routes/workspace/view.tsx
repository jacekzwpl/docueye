
import { WorkspaceView } from "~/views/workspace/view/WorkspaceView";
import type { Route } from "./+types/view";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function ViewRoute() {
  return <WorkspaceView />;
}
