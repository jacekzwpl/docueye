import { lazy } from 'react'
import { IRoute } from "./IRoute";

export const titleTemplate = '%s - Clear Feedback';

export const setTitle = (title: string) => {
  document.title = titleTemplate.replace('%s', title)
}

export const defaultRoute = '/';

export const routes:IRoute[] = [
  {
    path: '/',
    component: lazy(() => import('../views/workspaces'))
  },
  {
    path: '/workspace/:workspaceId/view/:viewId',
    component: lazy(() => import('../views/workspace/view'))
  },
  {
    path: '/workspace/:workspaceId/view',
    component: lazy(() => import('../views/workspace/view'))
  },
  {
    path: '/workspace/:workspaceId/documentation',
    component: lazy(() => import('../views/workspace/documentation'))
  },
  {
    path: '/workspace/:workspaceId/decisions',
    component: lazy(() => import('../views/workspace/decisions'))
  },
  {
    path: '/workspace/:workspaceId/decision/:decisionId',
    component: lazy(() => import('../views/workspace/decision'))
  },
  {
    path: '/workspace/:workspaceId/elements',
    component: lazy(() => import('../views/workspace/elements'))
  },
  {
    path: '/workspace/:workspaceId/element/:elementId',
    component: lazy(() => import('../views/workspace/element'))
  },
  {
    path: '/workspace/:workspaceId/modelchanges',
    component: lazy(() => import('../views/workspace/modelchanges'))
  },
  {
    path: '/graph',
    component: lazy(() => import('../views/graph'))
  },
  {
    path: '/workspace/:workspaceId/graph',
    component: lazy(() => import('../views/workspace/graph'))
  },
  {
    path: '/workspace/:workspaceId/graph/:viewId',
    component: lazy(() => import('../views/workspace/graph'))
  }
]