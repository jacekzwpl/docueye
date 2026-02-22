Param(
    [Parameter(Mandatory=$true)]
    [string]$version
)

####
#   .\Scripts\build-cli-localy.ps1 -version 1.3.0-a1
####

docker build -t local-docueye-cli:$version -f ./Source/DocuEye.CLI/Dockerfile .