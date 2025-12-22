import DecisionsView from "~/views/workspace/decisions";
import type { Route } from "./+types/decisions";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function WorkspaceDecisionsRoute() {
  return <DecisionsView />;
}
