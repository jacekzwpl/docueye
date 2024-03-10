import { NavigateFunction } from "react-router-dom";

const globalRouter = { navigate: null } as {
  navigate: null | NavigateFunction;
};

export default globalRouter;