# DocuEye
DocuEye is a tool that lets You visualize views and documentation created using [Structurizr DSL](https://structurizr.com/). 

## Features
- Import workspace via REST API
- Diagram viewer - You can view all diagrams in nice user friendly way (in my opinion :))
    - Automatic layout
    - Manual move elements
    - Diagram navigation
    - Export to PNG
- Elements catalog - You can search, navigate through your model elements 
    - View element properties. [See extra DocueEye properties](Documentation/docs/0004-element-extra-properties.md)
    - View element dependence's
    - View element consumers
    - View element children
    - View element documentation
    - View element decisions
- Decisions viewer - You can view all decisions defined at workspace or element level
- Documentation viewer - You can view documentation defined at workspace or element level
- Change Tracker - DocuEye tracks changes in the model on every import. You can view history of changes in Your model. [See what to do to change tracker works as expected](Documentation/docs/0002-change-tracker.md)  

See [features roadmap](Documentation/docs/0003-features-roadmap.md) if You want to know what features will be implemented in nearest feature.

## Getting started

The best way to get start is to use docker image.  
To run DocuEye You can use docker compose. File used in this example can be found [here](docker-compose.yml). Description of configuration options can be found [here](Documentation/docs/0005-configuration.md)  
DocuEye use Mongo DB as persistence so You need two containers running at one time.  

```Powershell
docker compose up -d
```
Thats it :). If everything goes es expected You can now access DocuEye at http://localhost:8080  
If You manage to run application You can see empty workspaces list.  
  
So it's time to import one.  
    
First step is exporting our workspace to json format using [structurizr cli export](https://docs.structurizr.com/cli/export).  

```Powershell
cd .\ExampleWorkspace

docker run -it --rm -v "$($PWD):/usr/local/structurizr" structurizr/cli export --workspace workspace.dsl -format json
# In this example $PWD is current directory path. 
```
Second step is to import workspace to DocuEye  

```Powershell
docker run -it --rm --network="host" -v "$($PWD):/app/import" jacekzwpl/docueye-cli --import=workspace --docueyeAddress=http://localhost:8080 --adminToken=docueyedmintoken --importKey="$((New-Guid).Guid)" --workspaceId=638d0822-12c7-4998-8647-9c7af7ad2989 --workspaceFile=./import/workspace.json
# In this example $PWD is current directory path. 
```

DocueEye CLI is also distributed as dotnet tool via [nuget package](https://www.nuget.org/packages/DocuEye.CLI/)  
Installing DocuEye CLI 
```Powershell
dotnet tool install --global DocuEye.CLI
```
Import workspace using command line 
```Powershell
docueye --import=workspace --docueyeAddress=http://localhost:8080 --adminToken=docueyedmintoken --importKey="$((New-Guid).Guid)" --workspaceId=638d0822-12c7-4998-8647-9c7af7ad2989 --workspaceFile=workspace.json
```

You can read about all options for DocuEye CLI [here](Documentation/docs/0006-docueye-cli.md).  
You can also use --help switch to see available options.  
For docker image  
```
docker run -it --rm jacekzwpl/docueye-cli --help
```
For command line 
```
docueye --help
```

Example script to import workspace can be found [here](ExampleWorkspace/import.ps1)  


## Contributing
I welcome all contributors. But before making PR-s start with create issue that we can discuss the change.





