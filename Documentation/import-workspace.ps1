Param(
    [Parameter(Mandatory,
    HelpMessage="The Admin token from DocuEye configuration")]
    [string]$adminToken,
    [Parameter(Mandatory,
    HelpMessage="The ID of the Workspace. If not provided the new workspace will be created. Also if workspace with given id does not exists new workspace will be created.")]
    [string]$workspaceId,
    [Parameter(HelpMessage="Unique import key. If not provided, one will be generated.")]
    [string]$importKey = (New-Guid).Guid,
    [Parameter(Mandatory,
    HelpMessage="DocuEye address ex. http://localhost:8080")]
    [string]$docueyeAddress,
    [Parameter(
    HelpMessage="Indicates that script will use dotnet tool instead of docueye-cli docker image")]
    [switch]$useDotNetTool
)

####
# .\import-workspace.ps1 -docueyeAddress http://localhost:8080 -adminToken docueyedmintoken -workspaceId 638d0822-12c7-4998-8647-9c7af7ad2989 
####

if(!$useDotNetTool) {
docker run -it --rm --network="host" -v "$($PWD):/app/import" local-docueye-cli:1.2.0-rc1  `
workspace import  `
--linter-config=./import/linter-config.json  `
--docueye-address="$docueyeAddress"  `
--admin-token="$adminToken"  `
--key="$importKey"  `
--id="$workspaceId"  `
--file=./import/workspace.dsl
}else {
docueye workspace import  `
--linter-config=./linter-config.json  `
--docueye-address="$docueyeAddress"  `
--admin-token="$adminToken"  `
--key="$importKey"  `
--id="$workspaceId"  `
--file=workspace.dsl
}