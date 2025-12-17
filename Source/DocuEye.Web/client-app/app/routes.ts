import { type RouteConfig, index, route } from "@react-router/dev/routes";

export default [
    index("routes/home.tsx"),
    route(
        "workspace/:workspaceId/elements", 
        "./routes/workspace/elements.tsx"
    ),
    route(
        "workspace/:workspaceId/element/:elementId", 
        "./routes/workspace/element.tsx"
    ),
] satisfies RouteConfig;