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


// May contain unused imports in some cases
// @ts-ignore
import { StructurizrWorkspaceConfigurationUser } from './structurizr-workspace-configuration-user';

/**
 * 
 * @export
 * @interface StructurizrWorkspaceConfiguration
 */
export interface StructurizrWorkspaceConfiguration {
    /**
     * 
     * @type {string}
     * @memberof StructurizrWorkspaceConfiguration
     */
    'visibility'?: string | null;
    /**
     * 
     * @type {Array<StructurizrWorkspaceConfigurationUser>}
     * @memberof StructurizrWorkspaceConfiguration
     */
    'users'?: Array<StructurizrWorkspaceConfigurationUser> | null;
}

