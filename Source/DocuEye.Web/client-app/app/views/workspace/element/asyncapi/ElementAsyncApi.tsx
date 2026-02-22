import { useEffect, useState } from "react"
import "swagger-ui-react/swagger-ui.css"
import DocuEyeApi from "../../../../api"
import Loader from "../../../../components/loader"
// @ts-ignore - Module lacks type declarations
import AsyncApiComponent from "@asyncapi/react-component/browser/without-parser";
import type { IElementAsyncApiProps } from "./IElementAsyncApiProps"
import "@asyncapi/react-component/styles/default.css";
// @ts-ignore - Module lacks type declarations
import Parser  from "@asyncapi/parser/browser";
const config = {
  schemaID: 'custom-spec',
};

export const ElementAsyncApi = (props: IElementAsyncApiProps) => {
    const [spec, setSpec] = useState<any>(null);
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
            .then(async (resp:any) => {
                if(resp.status !== 404) { 
                    const parser = new Parser();
                    const { document } = await parser.parse(resp.data);
                    setSpec(document);
                }
            }).finally(() => {
                setIsLoading(false);
            })

        }
        
    },[props, setSpec, setIsLoading])

    return (
       <>
        {spec !== null && <AsyncApiComponent schema={spec} config={config} /> }
        {!isLoading && spec === null && "No Async API documentation found"}
        {isLoading && <Loader />}
       </>
    )
}