import { useEffect, useState } from "react"
import SwaggerUI from "swagger-ui-react"
import "swagger-ui-react/swagger-ui.css"
import { IElementOpenApiProps } from "./IElementOpenApiProps"

export const ElementOpenApi = (props: IElementOpenApiProps) => {
    const [url, setUrl] = useState<string>("")
    useEffect(() => {
        if(props.elementId && props.workspaceId) {
            setUrl(`/api/workspaces/${props.workspaceId}/documentationfiles/openapi/${props.elementId}`);
        }
        
    },[props])

    return (
       <>
        {url !== "" && <SwaggerUI url={url}  /> }
       </>
    )
}