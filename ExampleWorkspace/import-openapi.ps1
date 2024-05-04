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
    HelpMessage="Path to openapi specification file")]
    [string]$openApiFile,
    [Parameter(Mandatory,
    HelpMessage="element Structurizr DSL ID")]
    [string]$elementDslId,
    [Parameter(
    HelpMessage="Indicates that script will use dotnet tool insead of docueeye-cli docker image")]
    [switch]$useDotNetTool
)
#####
# .\import-openapi.ps1 -docueyeAddress http://localhost:8080 -adminToken docueyedmintoken -workspaceId 638d0822-12c7-4998-8647-9c7af7ad2989 -openApiFile "/app/import/openapi/onlineshop/basket/swagger.yml" -elementDslId onlineshop.basket
#####

# import workspace to DocuEye
if(!$useDotNetTool) {
docker run -it --rm --network="host" -v "$($PWD):/app/import" jacekzwpl/docueye-cli  `
--import=openapi  `
--docueyeAddress="$docueyeAddress"  `
--adminToken="$adminToken"  `
--workspaceId="$workspaceId"  `
--openApiFile="$openApiFile"  `
--elementDslId="$elementDslId"  
}else {
docueye --import=openapi  `
--docueyeAddress="$docueyeAddress"  `
--adminToken="$adminToken"  `
--workspaceId="$workspaceId"  `
--openApiFile="$openApiFile"  `
--elementDslId="$elementDslId"  
}


