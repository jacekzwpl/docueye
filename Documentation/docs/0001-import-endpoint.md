## Import endpoint

Endpoint for import is: `/api/workspaces/import`  
Http Method `PUT`  
Endpoint requires Basic Authorization header  
`Authorization: Basic admintoken`  
### Body parameters
| Parameter | Type | Required | Description |
| --- | -- | -- | ----- |
| workspaceId | string | no | This is workspace unique identifier. <br /> **If no ID is provided:** <br /> ID will be generated and returned in import result response. In this case new workspace will be created. <br /> **If ID is provided:** <br /> If workspace with provided ID exists it will be updated if no new workspace will be created. <br /> **It is recommended to allays pass workspace ID.**
| importKey | string | yes | This is unique identifier of single import. This is used for couple of reasons: <br /> Prevents from running more than one workspace imports at the same time. Joins model changes history. <br /> If something goes wrong during import you can run it again with same key to finish it. <br /> Import key should be unique wor workspace it can be for example commit hash or pipeline run id.
| sourceLink | string | no | This is the link to source code version from whitch workspace is imported. It can be for example link to commit in your github repository  |
| workspaceData | object | yes | This is workspace json exported from structurizr cli |

### Response Body
| Parameter | Type | Description |
| --- | -- | ----- |
| workspaceId | string | The ID of workspace being imported |
| isSuccess | boolean | Indicates if import finished with success. True or False |
| message | string or null | Additional message returned from the import. Ex. Problem details in case of import failure. |