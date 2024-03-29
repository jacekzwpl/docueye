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
    [string]$docueyeAddress
)

####
# .\import-workspace.ps1 -docueyeAddress http://localhost:8080 -adminToken docueyedmintoken -workspaceId 8ff549b9-af3b-4e76-9cdf-aaa3218398a4 
####


#export workspace to json format
docker run -it --rm -v "$($PWD):/usr/local/structurizr" structurizr/cli export --workspace workspace.dsl -format json
#import workspace to DocuEye
docker run -it --rm --network="host" -v "$($PWD):/app/import" jacekzwpl/docueye-cli  `
--import=workspace  `
--docueyeAddress="$docueyeAddress"  `
--adminToken="$adminToken"  `
--importKey="$importKey"  `
--workspaceId="$workspaceId"  `
--workspaceFile=./import/workspace.json