Param(
    [Parameter(Mandatory,
    HelpMessage="The Admin token from DocuEye configuration")]
    [string]$adminToken,
    [Parameter(Mandatory,
    HelpMessage="The ID of the Workspace. ")]
    [string]$workspaceId,
    [Parameter(Mandatory,
    HelpMessage="DocuEye address ex. http://localhost:8080")]
    [string]$docueyeAddress,
    [Parameter(Mandatory,
    HelpMessage="Path to asyncapi specification file")]
    [string]$documentationFile,
    [Parameter(Mandatory,
    HelpMessage="element Structurizr DSL ID")]
    [string]$elementDslId,
    [Parameter(
    HelpMessage="Indicates that script will use dotnet tool insead of docueeye-cli docker image")]
    [switch]$useDotNetTool
)
#####
# .\import-asyncapi.ps1 -docueyeAddress http://localhost:8080 -adminToken docueyedmintoken -workspaceId 638d0822-12c7-4998-8647-9c7af7ad2989 -documentationFile "/app/import/asyncapi/onlineshop/basket/asyncapi.yaml" -elementDslId onlineshop.basket
#####


# import workspace to DocuEye
if(!$useDotNetTool) {
docker run -it --rm --network="host" -v "$($PWD):/app/import" jacekzwpl/docueye-cli  `
docfile import  `
--docueye-address="$docueyeAddress"  `
--admin-token="$adminToken"  `
--id="$workspaceId"  `
--file="$documentationFile"  `
--element-dsl-id="$elementDslId"  `
--type=asyncapi
}else {
docueye docfile import  `
--docueye-address="$docueyeAddress"  `
--admin-token="$adminToken"  `
--id="$workspaceId"  `
--file="$documentationFile"  `
--element-dsl-id="$elementDslId"  `
--type=asyncapi
}


