import { useEffect, useState } from "react"
import SwaggerUI from "swagger-ui-react"
import "swagger-ui-react/swagger-ui.css"
import DocuEyeApi from "../../../../api"
import Loader from "../../../../components/loader"
import AsyncApiComponent, { type ConfigInterface } from "@asyncapi/react-component";
import type { IElementAsyncApiProps } from "./IElementAsyncApiProps"
import "@asyncapi/react-component/styles/default.css";

const config: ConfigInterface = {
  schemaID: 'custom-spec',
};

export const ElementAsyncApi = (props: IElementAsyncApiProps) => {
    const [spec, setSpec] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    useEffect(() => {

        if(props.elementId && props.workspaceId) {
            setIsLoading(true);
            DocuEyeApi.DocumentationFilesApi
            .apiWorkspacesWorkspaceIdDocumentationfilesElementIdGet(
                props.workspaceId, props.elementId, "asyncapi", {
                validateStatus: (status) => {
                    return (status >= 200 && status < 300) || status === 404; 
                }
            })
            .then((resp:any) => {
                if(resp.status !== 404) { 
                    console.log(resp.data);
                    setSpec(resp.data);
                }
            }).finally(() => {
                setIsLoading(false);
            })

        }
        
    },[props, setSpec, setIsLoading])

    return (
       <>
        {spec !== "" && <AsyncApiComponent schema={spec} config={config} /> }
        {!isLoading && spec === "" && "No Async API documentation found"}
        {isLoading && <Loader />}
       </>
    )
}