import Forbidden from "~/views/errors/forbidden";
import type { Route } from "./+types/forbidden";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "DocuEye" },
    { name: "description", content: "DocuEye!" },
  ];
}

export default function ErrorsForbiddenRoute() {
  return <Forbidden />;
}