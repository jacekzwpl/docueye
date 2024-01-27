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
import { StructurizrDocumentation } from './structurizr-documentation';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrRelationship } from './structurizr-relationship';

/**
 * 
 * @export
 * @interface StructurizrComponent
 */
export interface StructurizrComponent {
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'id'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'name'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'description'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'technology'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'tags'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'url'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'group'?: string | null;
    /**
     * 
     * @type {{ [key: string]: string; }}
     * @memberof StructurizrComponent
     */
    'properties'?: { [key: string]: string; } | null;
    /**
     * 
     * @type {Array<StructurizrRelationship>}
     * @memberof StructurizrComponent
     */
    'relationships'?: Array<StructurizrRelationship> | null;
    /**
     * 
     * @type {StructurizrDocumentation}
     * @memberof StructurizrComponent
     */
    'documentation'?: StructurizrDocumentation;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'dslId'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'ownerTeam'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrComponent
     */
    'sourceCodeUrl'?: string | null;
}

