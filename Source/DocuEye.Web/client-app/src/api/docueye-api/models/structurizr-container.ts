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
import { StructurizrComponent } from './structurizr-component';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrDocumentation } from './structurizr-documentation';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrRelationship } from './structurizr-relationship';

/**
 * 
 * @export
 * @interface StructurizrContainer
 */
export interface StructurizrContainer {
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'id'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'name'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'description'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'technology'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'tags'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'url'?: string | null;
    /**
     * 
     * @type {Array<StructurizrComponent>}
     * @memberof StructurizrContainer
     */
    'components'?: Array<StructurizrComponent> | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'group'?: string | null;
    /**
     * 
     * @type {{ [key: string]: string; }}
     * @memberof StructurizrContainer
     */
    'properties'?: { [key: string]: string; } | null;
    /**
     * 
     * @type {Array<StructurizrRelationship>}
     * @memberof StructurizrContainer
     */
    'relationships'?: Array<StructurizrRelationship> | null;
    /**
     * 
     * @type {StructurizrDocumentation}
     * @memberof StructurizrContainer
     */
    'documentation'?: StructurizrDocumentation;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'dslId'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'ownerTeam'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrContainer
     */
    'sourceCodeUrl'?: string | null;
}

