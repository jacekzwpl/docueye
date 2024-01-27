import { 
    AppApi, 
    ModelChangesApi, 
    ElementsApi, 
    WorkspacesApi, 
    ViewsApi, 
    DocumentationsApi, 
    DecisionsApi } from "./docueye-api";


const DocuEyeApi = {
    AppApi: new AppApi(undefined, ""),
    ElementsApi: new ElementsApi(undefined, ""),
    DocumentationsApi: new DocumentationsApi(undefined, ""),
    DecisionsApi: new DecisionsApi(undefined, ""),
    ViewsApi: new ViewsApi(undefined, ""),
    WorkspacesApi: new WorkspacesApi(undefined, ""),
    ModelChangesApi: new ModelChangesApi(undefined, "")
};

//export * from "./interceptors";
export default DocuEyeApi;