/* tslint:disable */
/* eslint-disable */
/**
 * DocuEye.Web
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import type { Configuration } from '../configuration';
import type { AxiosPromise, AxiosInstance, RawAxiosRequestConfig } from 'axios';
import globalAxios from 'axios';
// Some imports not used depending on template conditions
// @ts-ignore
import { DUMMY_BASE_URL, assertParamExists, setApiKeyToObject, setBasicAuthToObject, setBearerAuthToObject, setOAuthToObject, setSearchParams, serializeDataIfNeeded, toPathString, createRequestFunction } from '../common';
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError, operationServerMap } from '../base';
// @ts-ignore
import { FoundedWorkspace } from '../models';
// @ts-ignore
import { ViewConfiguration } from '../models';
// @ts-ignore
import { Workspace } from '../models';
/**
 * WorkspacesApi - axios parameter creator
 * @export
 */
export const WorkspacesApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {string} [name] 
         * @param {number} [limit] 
         * @param {number} [skip] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesGet: async (name?: string, limit?: number, skip?: number, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/workspaces`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (name !== undefined) {
                localVarQueryParameter['name'] = name;
            }

            if (limit !== undefined) {
                localVarQueryParameter['limit'] = limit;
            }

            if (skip !== undefined) {
                localVarQueryParameter['skip'] = skip;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesIdDelete: async (id: string, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiWorkspacesIdDelete', 'id', id)
            const localVarPath = `/api/workspaces/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'DELETE', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesIdGet: async (id: string, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiWorkspacesIdGet', 'id', id)
            const localVarPath = `/api/workspaces/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesIdViewconfigurationGet: async (id: string, options: RawAxiosRequestConfig = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiWorkspacesIdViewconfigurationGet', 'id', id)
            const localVarPath = `/api/workspaces/{id}/viewconfiguration`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            setSearchParams(localVarUrlObj, localVarQueryParameter);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * WorkspacesApi - functional programming interface
 * @export
 */
export const WorkspacesApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = WorkspacesApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {string} [name] 
         * @param {number} [limit] 
         * @param {number} [skip] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiWorkspacesGet(name?: string, limit?: number, skip?: number, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<Array<FoundedWorkspace>>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiWorkspacesGet(name, limit, skip, options);
            const index = configuration?.serverIndex ?? 0;
            const operationBasePath = operationServerMap['WorkspacesApi.apiWorkspacesGet']?.[index]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, operationBasePath || basePath);
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiWorkspacesIdDelete(id: string, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiWorkspacesIdDelete(id, options);
            const index = configuration?.serverIndex ?? 0;
            const operationBasePath = operationServerMap['WorkspacesApi.apiWorkspacesIdDelete']?.[index]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, operationBasePath || basePath);
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiWorkspacesIdGet(id: string, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<Workspace>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiWorkspacesIdGet(id, options);
            const index = configuration?.serverIndex ?? 0;
            const operationBasePath = operationServerMap['WorkspacesApi.apiWorkspacesIdGet']?.[index]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, operationBasePath || basePath);
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiWorkspacesIdViewconfigurationGet(id: string, options?: RawAxiosRequestConfig): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<ViewConfiguration>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiWorkspacesIdViewconfigurationGet(id, options);
            const index = configuration?.serverIndex ?? 0;
            const operationBasePath = operationServerMap['WorkspacesApi.apiWorkspacesIdViewconfigurationGet']?.[index]?.url;
            return (axios, basePath) => createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration)(axios, operationBasePath || basePath);
        },
    }
};

/**
 * WorkspacesApi - factory interface
 * @export
 */
export const WorkspacesApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = WorkspacesApiFp(configuration)
    return {
        /**
         * 
         * @param {string} [name] 
         * @param {number} [limit] 
         * @param {number} [skip] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesGet(name?: string, limit?: number, skip?: number, options?: any): AxiosPromise<Array<FoundedWorkspace>> {
            return localVarFp.apiWorkspacesGet(name, limit, skip, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesIdDelete(id: string, options?: any): AxiosPromise<void> {
            return localVarFp.apiWorkspacesIdDelete(id, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesIdGet(id: string, options?: any): AxiosPromise<Workspace> {
            return localVarFp.apiWorkspacesIdGet(id, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {string} id 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiWorkspacesIdViewconfigurationGet(id: string, options?: any): AxiosPromise<ViewConfiguration> {
            return localVarFp.apiWorkspacesIdViewconfigurationGet(id, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * WorkspacesApi - object-oriented interface
 * @export
 * @class WorkspacesApi
 * @extends {BaseAPI}
 */
export class WorkspacesApi extends BaseAPI {
    /**
     * 
     * @param {string} [name] 
     * @param {number} [limit] 
     * @param {number} [skip] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof WorkspacesApi
     */
    public apiWorkspacesGet(name?: string, limit?: number, skip?: number, options?: RawAxiosRequestConfig) {
        return WorkspacesApiFp(this.configuration).apiWorkspacesGet(name, limit, skip, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {string} id 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof WorkspacesApi
     */
    public apiWorkspacesIdDelete(id: string, options?: RawAxiosRequestConfig) {
        return WorkspacesApiFp(this.configuration).apiWorkspacesIdDelete(id, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {string} id 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof WorkspacesApi
     */
    public apiWorkspacesIdGet(id: string, options?: RawAxiosRequestConfig) {
        return WorkspacesApiFp(this.configuration).apiWorkspacesIdGet(id, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {string} id 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof WorkspacesApi
     */
    public apiWorkspacesIdViewconfigurationGet(id: string, options?: RawAxiosRequestConfig) {
        return WorkspacesApiFp(this.configuration).apiWorkspacesIdViewconfigurationGet(id, options).then((request) => request(this.axios, this.basePath));
    }
}

