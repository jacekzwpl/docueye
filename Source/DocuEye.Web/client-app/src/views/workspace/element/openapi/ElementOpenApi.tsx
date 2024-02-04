import SwaggerUI from "swagger-ui-react"
import "swagger-ui-react/swagger-ui.css"

export const ElementOpenApi = () => {
    return (
       <>
        <SwaggerUI url="https://localhost:3000/swagger.yml" />
       </>
    )
}