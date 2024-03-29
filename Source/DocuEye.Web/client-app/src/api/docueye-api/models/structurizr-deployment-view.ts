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
import { StructurizrAnimationStep } from './structurizr-animation-step';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrAutomaticLayout } from './structurizr-automatic-layout';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrDimensions } from './structurizr-dimensions';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrElementView } from './structurizr-element-view';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrRelationshipView } from './structurizr-relationship-view';

/**
 * 
 * @export
 * @interface StructurizrDeploymentView
 */
export interface StructurizrDeploymentView {
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentView
     */
    'title'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentView
     */
    'description'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentView
     */
    'key'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentView
     */
    'softwareSystemId'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentView
     */
    'paperSize'?: string | null;
    /**
     * 
     * @type {StructurizrDimensions}
     * @memberof StructurizrDeploymentView
     */
    'dimensions'?: StructurizrDimensions;
    /**
     * 
     * @type {StructurizrAutomaticLayout}
     * @memberof StructurizrDeploymentView
     */
    'automaticLayout'?: StructurizrAutomaticLayout;
    /**
     * 
     * @type {Array<StructurizrElementView>}
     * @memberof StructurizrDeploymentView
     */
    'elements'?: Array<StructurizrElementView> | null;
    /**
     * 
     * @type {Array<StructurizrRelationshipView>}
     * @memberof StructurizrDeploymentView
     */
    'relationships'?: Array<StructurizrRelationshipView> | null;
    /**
     * 
     * @type {Array<StructurizrAnimationStep>}
     * @memberof StructurizrDeploymentView
     */
    'animations'?: Array<StructurizrAnimationStep> | null;
}

