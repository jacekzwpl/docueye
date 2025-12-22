Param(
    [Parameter(Mandatory=$true)]
    [string]$version
)
####
#   .\Scripts\build-localy.ps1 -version 1.1.0-rc7
####

docker build -t local-docueye:$version -f ./Source/DocuEye.Web/Dockerfile .