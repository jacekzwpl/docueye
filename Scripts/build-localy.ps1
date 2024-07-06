Param(
    [Parameter(Mandatory=$true)]
    [string]$version
)
####
#   .\Scripts\build-localy.ps1 -version 0.10.1-rc1
####

docker build -t local-docueye:$version -f ./Source/DocuEye.Web/Dockerfile .