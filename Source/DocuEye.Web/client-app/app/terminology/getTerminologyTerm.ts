import type { Terminology } from "../api/docueye-api";

export const defaultTerminology:Terminology  = {
    person: "Person",
    softwareSystem: "Software System",
    container: "Container",
    component: "Component",
    deploymentNode: "Deployment Node",
    infrastructureNode: "Infrastructure Node"
}

export const getTerminologyTerm = (term: string | null | undefined, overrideTerms: Terminology | undefined) => {
    switch(term) {
        case "Person":
            return overrideTerms?.person ? 
                overrideTerms.person : defaultTerminology.person;
        case "SoftwareSystem":
            return overrideTerms?.softwareSystem ? 
                overrideTerms.softwareSystem : defaultTerminology.softwareSystem;
        case "Container":
            return overrideTerms?.container ? 
                overrideTerms.container : defaultTerminology.container;
        case "Component":
            return overrideTerms?.component ? 
                overrideTerms.component : defaultTerminology.component;
        case "DeploymentNode":
            return overrideTerms?.deploymentNode ? 
                 overrideTerms.deploymentNode : defaultTerminology.deploymentNode;
        case "ContainerInstance":
            return overrideTerms?.container ? 
                overrideTerms.container + " Instance" : defaultTerminology.container + " Instance";
        case "SoftwareSystemInstance":
            return overrideTerms?.softwareSystem ? 
                overrideTerms.softwareSystem + " Instance" : defaultTerminology.softwareSystem + " Instance";
        case "InfrastructureNode": 
        return overrideTerms?.infrastructureNode ? 
            overrideTerms.infrastructureNode : defaultTerminology.infrastructureNode;
        default: 
            return "";
    }
}


