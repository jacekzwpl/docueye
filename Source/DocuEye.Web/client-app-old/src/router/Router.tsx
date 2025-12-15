import { Suspense } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import { defaultRoute, routes } from "./routes";

export const Router = () => {
    return(
<Suspense >
        <Routes>
          {routes.map(route => (
            <Route
              key={route.path}
              path={route.path}
              element = {<route.component  />}
            />
          ))}
          {defaultRoute !== '/' && <Navigate to={defaultRoute} />}

          {/* NotFound page 
          <Route path="*" component={NotFound} /> */}
          
        </Routes></Suspense>
    );
}