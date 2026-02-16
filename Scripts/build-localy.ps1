Param(
    [Parameter(Mandatory=$true)]
    [string]$version
)
####
#   .\Scripts\build-localy.ps1 -version 1.2.0-rc3
####

docker build -t local-docueye:$version -f ./Source/DocuEye.Web/Dockerfile .