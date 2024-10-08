## DocueEye CLI options

| Parameter | Required | Description | Example |
| --- | --- | ------ | ---- |
| `--import` | Yes | Import mode. 'workspace' for workspace import. 'openapi' for import openapi specification for element. 'delete' for delete workspace data | `--import=workspace` or `--import=openapi` or `--import=delete` |
| `--docueyeAddress` | Yes | DocuEye address. | `--docueyeAddress=http://localhost:8080` |
| `--adminToken` | Yes | The Admin token from DocuEye configuration. | `--adminToken=docueyedmintoken` |
| `--workspaceFile ` | Yes for workspace import | Path to workspace.json file, generated by structuriz cli. | `--workspaceFile=workspace.json` |
| `--workspaceId` | Yes for openapi import and delete | The ID of the Workspace. <br /><br /> For workspace import: <br /> If not provided the new workspace will be created. Also if workspace with given id does not exists new workspace will be created. **It is recommended to allays pass workspace ID.** <br /><br /> For openapi import passing ID is required. | `--workspaceId=638d0822-12c7-4998-8647-9c7af7ad2989` |
| `--importKey` | No | Unique for workspace import key. If not provided, one will be generated. <br /> Import Key connects changes and prevents from running multiple imports for workspace at the same time. It can be for example commit hash that points to source version from witch workspace is imported. | `--importKey=09bb3efb-6de6-486d-9e90-b7aab8e36b7f` |
| `--sourceLink` | No | Link to source version from whitch workspace is imported ex. link to PR or commit on github. | `--sourceLink=https://localhost:8443` |
| `--elementId` | Yes for openapi import if `--elementDslId` is not set | Element ID (DocueEye ID) | `--elementId=638d0822-12c7-4998-8647-9c7af7ad2989` |
| `--elementDslId` | Yes for openapi import if `--elementId` is not set | `structurizr.dsl.identifier` generated in structurizr export operation. | `--elementDslId=onlineshop.basket` |
| `--openApiFile` | Yes for openapi import | Path to openapi specification file for element | `--openApiFile=swagger.yml` |
| `--help` | No | Display help screen. | `docueye --help` |
| `--version` | No | Display version information. | `docueye --version` |
