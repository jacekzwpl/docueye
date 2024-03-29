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
import { StructurizrContainerInstance } from './structurizr-container-instance';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrInfrastructureNode } from './structurizr-infrastructure-node';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrRelationship } from './structurizr-relationship';
// May contain unused imports in some cases
// @ts-ignore
import { StructurizrSoftwareSystemInstance } from './structurizr-software-system-instance';

/**
 * 
 * @export
 * @interface StructurizrDeploymentNode
 */
export interface StructurizrDeploymentNode {
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'id'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'name'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'description'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'technology'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'environment'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'instances'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'tags'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'url'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'group'?: string | null;
    /**
     * 
     * @type {Array<StructurizrDeploymentNode>}
     * @memberof StructurizrDeploymentNode
     */
    'children'?: Array<StructurizrDeploymentNode> | null;
    /**
     * 
     * @type {{ [key: string]: string; }}
     * @memberof StructurizrDeploymentNode
     */
    'properties'?: { [key: string]: string; } | null;
    /**
     * 
     * @type {Array<StructurizrContainerInstance>}
     * @memberof StructurizrDeploymentNode
     */
    'containerInstances'?: Array<StructurizrContainerInstance> | null;
    /**
     * 
     * @type {Array<StructurizrSoftwareSystemInstance>}
     * @memberof StructurizrDeploymentNode
     */
    'softwareSystemInstances'?: Array<StructurizrSoftwareSystemInstance> | null;
    /**
     * 
     * @type {Array<StructurizrRelationship>}
     * @memberof StructurizrDeploymentNode
     */
    'relationships'?: Array<StructurizrRelationship> | null;
    /**
     * 
     * @type {Array<StructurizrInfrastructureNode>}
     * @memberof StructurizrDeploymentNode
     */
    'infrastructureNodes'?: Array<StructurizrInfrastructureNode> | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'dslId'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'ownerTeam'?: string | null;
    /**
     * 
     * @type {string}
     * @memberof StructurizrDeploymentNode
     */
    'sourceCodeUrl'?: string | null;
}

