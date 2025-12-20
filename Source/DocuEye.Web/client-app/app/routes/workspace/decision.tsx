import type { Route } from "./+types/decision";
import { DecisionView } from "~/views/workspace/decision/DecisionView";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function Decision() {
  return <DecisionView />;
}
