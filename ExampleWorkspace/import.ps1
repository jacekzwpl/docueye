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
    [switch]$useDotNetTool,
    [Parameter(
    HelpMessage="Indicates that workspace will be imported from json file instead of dsl file. The json file should be generated with structurizr-cli export command with --format json option")]
    [switch]$importFromJson

)

####
# .\import.ps1 -docueyeAddress http://localhost:8080 -adminToken docueyedmintoken -workspaceId 638d0822-12c7-4998-8647-9c7af7ad2989 
####

if($importFromJson) {
    docker run -it --rm -v "$($PWD):/usr/local/structurizr" structurizr/cli export --workspace workspace.dsl -format json
    $importFile = "workspace.json"
    $mode = "json"
}else {
    $importFile = "workspace.dsl"
    $mode = "dsl"
}

if(!$useDotNetTool) {
docker run -it --rm --network="host" -v "$($PWD):/app/import" local-docueye-cli:1.4.0-rc2 `
workspace import  `
--docueye-address="$docueyeAddress"  `
--admin-token="$adminToken"  `
--key="$importKey"  `
--id="$workspaceId"  `
--file="./import/$importFile"  `
--mode="$mode"
}else {
docueye workspace import  `
--docueye-address="$docueyeAddress"  `
--admin-token="$adminToken"  `
--key="$importKey"  `
--id="$workspaceId"  `
--file="$importFile"  `
--mode="$mode"
}



