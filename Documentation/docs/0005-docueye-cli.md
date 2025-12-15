## DocueEye CLI

DocueEye CLI is distributed by:
- [docker image](https://hub.docker.com/r/jacekzwpl/docueye-cli) 
- dotnet tool via [nuget package](https://www.nuget.org/packages/DocuEye.CLI/) - current version requires .net 10 to be preinstalled.

**Example usage with docker image**
```Powershell
docker run -it --rm jacekzwpl/docueye-cli --help # shows help for DocuEye CLI
docker run -it --rm jacekzwpl/docueye-cli workspace import --help # shows help for workspace import
```

**Example usage with dotnet tool**
```Powershell
docueye --help # shows help for DocuEye CLI
docueye workspace import --help # shows help for workspace import
```


### Workspace commands

Commands for managing workspaces

[import](0006-docueye-cli-workspace-commands.md#workspace-import-command) - Imports workspace to DocuEye.  

[delete](0006-docueye-cli-workspace-commands.md#workspace-delete-command) - Deletes a workspace from DocuEye.  

[export](0006-docueye-cli-workspace-commands.md#workspace-export-command) - Exports a workspace to given format.  

[validate](0006-docueye-cli-workspace-commands.md#workspace-export-command) - Validates dsl workspace.  



### OpenApi commands
Commands for working with OpenAPI specifications.

 [import](0007-docueye-cli-openapi-commands.md#openapi-import-command) - Imports or updates OpenAPI specification for given element.  

 [delete](0007-docueye-cli-openapi-commands.md#openapi-delete-command) - Deletes OpenAPI file for element.  
 





