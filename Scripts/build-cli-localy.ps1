Param(
    [Parameter(Mandatory=$true)]
    [string]$version
)

####
#   .\Scripts\build-cli-localy.ps1 -version 0.10.1-rc1
####

docker build -t local-docueye-cli:$version -f ./Source/DocuEye.CLI/Dockerfile .