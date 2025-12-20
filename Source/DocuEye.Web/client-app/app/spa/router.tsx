import React from "react";
import { createBrowserRouter } from "react-router";
import Workspaces from "~/views/workspaces";
import ElementsListView from "~/views/workspace/elements";
import ElementView from "~/views/workspace/element";
import DocumentationView from "~/views/workspace/documentation";
import DecisionsView from "~/views/workspace/decisions";
import DecisionView from "~/views/workspace/decision";
import { DeploymentNodesMatrix } from "~/views/workspace/deploymentnodesmatrix/DeploymentNodesMatrix";
import ModelChanges from "~/views/workspace/modelchanges";
import GraphView from "~/views/workspace/graph";

export const router = createBrowserRouter([
  { path: "/", element: <Workspaces /> },
  { path: "/workspace/:workspaceId/elements", element: <ElementsListView /> },
  { path: "/workspace/:workspaceId/element/:elementId", element: <ElementView /> },
  { path: "/workspace/:workspaceId/documentation", element: <DocumentationView /> },
  { path: "/workspace/:workspaceId/decisions", element: <DecisionsView /> },
  { path: "/workspace/:workspaceId/decision/:decisionId", element: <DecisionView /> },
  { path: "/workspace/:workspaceId/deploymentnodesmatrix", element: <DeploymentNodesMatrix /> },
  { path: "/workspace/:workspaceId/modelchanges", element: <ModelChanges /> },
  { path: "/workspace/:workspaceId/graph/:viewId?", element: <GraphView /> },
]);
