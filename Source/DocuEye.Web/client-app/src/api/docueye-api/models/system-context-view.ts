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
import { AutomaticLayout } from './automatic-layout';
// May contain unused imports in some cases
// @ts-ignore
import { ElementView } from './element-view';
// May contain unused imports in some cases
// @ts-ignore
import { RelationshipView } from './relationship-view';

/**
 * 
 * @export
 * @interface SystemContextView
 */
export interface SystemContextView {
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'id'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'title'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'description'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'key'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'workspaceId'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'viewType'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'paperSize'?: string | null;
    /**
     * 
     * @type {Array<ElementView>}
     * @memberof SystemContextView
     */
    'elements'?: Array<ElementView> | null;
    /**
     * 
     * @type {Array<RelationshipView>}
     * @memberof SystemContextView
     */
    'relationships'?: Array<RelationshipView> | null;
    /**
     * 
     * @type {AutomaticLayout}
     * @memberof SystemContextView
     */
    'automaticLayout'?: AutomaticLayout;
    /**
     * 
     * @type {string}
     * @memberof SystemContextView
     */
    'softwareSystemId'?: string | null;
    /**
     * 
     * @type {boolean}
     * @memberof SystemContextView
     */
    'enterpriseBoundaryVisible'?: boolean | null;
}

