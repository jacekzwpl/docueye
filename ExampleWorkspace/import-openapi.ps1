Param(
    [Parameter(Mandatory,
    HelpMessage="Local directory with workspace.tpl file.")]
    [string]$workspaceDir,
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

#export workspace to json format
docker run -it --rm -v "$($workspaceDir):/usr/local/structurizr" structurizr/cli export --workspace workspace.dsl -format json
#import workspace to DocuEye
docker run -it --rm --network="host" -v "$($workspaceDir):/app/import" jacekzwpl/docueye-cli  `
--import=workspace  `
--docueyeAddress="$docueyeAddress"  `
--adminToken="$adminToken"  `
--importKey="$importKey"  `
--workspaceId="$workspaceId"  `
--workspaceFile=./import/workspace.json
