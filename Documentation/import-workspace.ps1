Param(
    [string]$workspaceDir = "//c/nCode/ArchPortal/Documentation",
    [string]$adminToken = "docueyedmintoken",
    [string]$workspaceId = "6b4e03fa-1ada-4ec8-8e9f-550350334e91",
    [string]$importKey = (New-Guid).Guid,
    [string]$sourceLink = $null,
    [string]$docueyeAddress = "http://localhost:8080"
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