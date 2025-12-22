import Forbidden from "~/views/errors/forbidden";
import type { Route } from "./+types/forbidden";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "New React Router App" },
    { name: "description", content: "Welcome to React Router!" },
  ];
}

export default function ErrorsForbiddenRoute() {
  return <Forbidden />;
}