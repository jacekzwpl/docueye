
import { IssuesView } from "~/views/workspace/issues/IssuesView";
import type { Route } from "./+types/issues";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function IssuesRoute() {
  return <IssuesView />;
}
