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
    [Parameter(Mandatory,
    HelpMessage="Indicates that script will use dotnet tool insead of docueeye-cli docker image")]
    [switch]$useDotNetTool
)

####
# .\import.ps1 -docueyeAddress http://localhost:8080 -adminToken docueyedmintoken -workspaceId 638d0822-12c7-4998-8647-9c7af7ad2989 
####


#export workspace to json format
docker run -it --rm -v "$($PWD):/usr/local/structurizr" structurizr/cli export --workspace workspace.dsl -format json
#import workspace to DocuEye
if(!$useDotNetTool) {
docker run -it --rm --network="host" -v "$($PWD):/app/import" jacekzwpl/docueye-cli  `
--import=workspace  `
--docueyeAddress="$docueyeAddress"  `
--adminToken="$adminToken"  `
--importKey="$importKey"  `
--workspaceId="$workspaceId"  `
--workspaceFile=./import/workspace.json
}else {
docueye --import=workspace  `
--docueyeAddress="$docueyeAddress"  `
--adminToken="$adminToken"  `
--importKey="$importKey"  `
--workspaceId="$workspaceId"  `
--workspaceFile=workspace.json
}

