import { useEffect, useState } from "react"
import SwaggerUI from "swagger-ui-react"
import "swagger-ui-react/swagger-ui.css"
import DocuEyeApi from "../../../../api"
import Loader from "../../../../components/loader"
import type { IElementOpenApiProps } from "./IElementOpenApiProps"

export const ElementOpenApi = (props: IElementOpenApiProps) => {
    const [spec, setSpec] = useState<string>("");
    const [isLoading, setIsLoading] = useState<boolean>(false);
    useEffect(() => {

        if(props.elementId && props.workspaceId) {
            setIsLoading(true);
            DocuEyeApi.DocumentationFilesApi
            .apiWorkspacesWorkspaceIdDocumentationfilesOpenapiElementIdGet(
                props.workspaceId, props.elementId, {
                validateStatus: (status) => {
                    return (status >= 200 && status < 300) || status === 404; 
                }
            })
            .then((resp:any) => {
                if(resp.status !== 404) { 
                    setSpec(resp.data);
                }
            }).finally(() => {
                setIsLoading(false);
            })

        }
        
    },[props, setSpec, setIsLoading])

    return (
       <>
        {spec !== "" && <SwaggerUI spec={spec} /> }
        {!isLoading && spec === "" && "No Open Api documentation found"}
        {isLoading && <Loader />}
       </>
    )
}