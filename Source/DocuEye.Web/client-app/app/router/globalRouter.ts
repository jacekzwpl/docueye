import type { NavigateFunction } from "react-router";

const globalRouter = { navigate: null } as {
  navigate: null | NavigateFunction;
};

export default globalRouter;