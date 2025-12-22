import axios from "axios";
import { snackbarUtils } from "../snackbar/snackbarUtils";
import globalRouter from "../router/globalRouter";

export const initRequestInterceptors = (csrfToken: string) => {
    axios.interceptors.request.use(function (config) {
        // Do something before request is sent
        config.headers["X-CSRF-TOKEN-ADMIN"] = csrfToken;

        return config;
    }, function (error) {
        // Do something with request error
        return Promise.reject(error);
    });
}

export const initResponseInterceptors = () => {

    axios.interceptors.response.use(function (response) {
        // Any status code that lie within the range of 2xx cause this function to trigger
        // Do something with response data
        return response;
    }, function (error) {
        // Any status codes that falls outside the range of 2xx cause this function to trigger
        // Do something with response error
        if(error.response.status === 400) {
            if(error.response.data.title) {
                snackbarUtils.error(error.response.data.title);
            }else {
                snackbarUtils.error("Bad request");
            }
            if(error.response.data.detail) {
                snackbarUtils.info(error.response.data.detail);
            }
        }

        if (error.response.status === 401) {
            if (import.meta.env.VITE_APP_SERVER_URL) {
                window.location.href = import.meta.env.VITE_APP_SERVER_URL + "/auth/login";
            } else {
                window.location.href = "/auth/login";
            }
        }

        if(error.response.status === 403) {
            if(error.response.data.title) {
                snackbarUtils.error(error.response.data.title);
            }else {
                snackbarUtils.error("Access denied");
            }
            if(error.response.data.detail) {
                snackbarUtils.info(error.response.data.detail);
            }
            if(globalRouter.navigate) {
                globalRouter.navigate("/errors/forbidden");
            }
        }

        if(error.response.status === 404) {
            
            if(error.response.data.title) {
                snackbarUtils.error(error.response.data.title);
            }else {
                snackbarUtils.error("Requested resource was not found.");
            }
            if(error.response.data.detail) {
                snackbarUtils.info(error.response.data.detail);
            }
        }

        if(error.response.status === 500) {
            snackbarUtils.error("Unexpected error occurred");
        }
        return Promise.reject(error);
    });
}

