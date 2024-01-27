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
    [Parameter(HelpMessage="Link to source version from whitch workspace is imported ex. link to PR or commit on github.")]
    [string]$sourceLink = $null,
    [Parameter(Mandatory,
    HelpMessage="DocuEye address ex. http://localhost:8080")]
    [string]$docueyeAddress
)

#export workspace to json format
docker run -it --rm -v "$($workspaceDir):/usr/local/structurizr" structurizr/cli export --workspace workspace.dsl -format json
#read json file content
$workspaceData = Get-Content -Path workspace.json -Raw | ConvertFrom-Json -Depth 100

# build request headers
$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Content-Type", "application/json")
$headers.Add("Authorization", "Basic $adminToken")

#build request body
$body = @{
    workspaceId = $workspaceId
    importKey = $importKey
    sourceLink = $sourceLink
    workspaceData = $workspaceData
}
$bodyStr = $body | ConvertTo-Json -Depth 100 

#run import
$response = Invoke-RestMethod "$docueyeAddress/api/workspaces/import" -Method 'PUT' -Headers $headers -Body $bodyStr
$response | ConvertTo-Json -Depth 100
