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

## Getting started

The best way to get start is to use docker image.  
To run DoucuEye You can use docker compose. File used in this example can be found [here](docker-compose.yml). Description of configuration options can be found [here](Documentation/docs/0005-configuration.md)  
DocuEye use Mongo DB as persistence so You need two containers running at one time.  

```Powershell
docker compose up -d
```
Thats it :). If everything goes es expected You can now access DocuEye at http://localhost:8080  
If You manage to run application You can see empty workspaces list. So it's time to import one.  
At this time there is no additional cli to do this (there will be see [features roadmap](Documentation/docs/0003-features-roadmap.md) for details). Instead of this You can use powershell or curl. Example script to import workspace can be found [here](ExampleWorkspace/import.ps1)  
To run import using powershell:  
```Powershell
cd .\ExampleWorkspace

.\import.ps1 -workspaceDir $PWD -workspaceId "638d0822-12c7-4998-8647-9c7af7ad2989" -adminToken "docueyedmintoken" -docueyeAddress "http://localhost:8080"
# In this example $PWD is current directory path. 
# If You are using docker desktop with wsl on windows operating system this might not work. You have to change it to format //<diskname>/path/to/directory ex. //c/myrepos/docueye/ExampleWorkspace
```  
**What is going on here**  
DoucuEye is using workspace.json gereated by [structurizr cli export](https://docs.structurizr.com/cli/export).  So first thing we have to do is to export worskpace into json format.  
After that creating json file with our workspace data we build request and run import using DocuEye import endpoint. See the [example script](ExampleWorkspace/import.ps1) or [import endpoint description](Documentation/docs/0001-import-endpoint.md) for more details.

## Contributing
I welcome all contributors. But before making PR-s start with create issue that we can discuss the change.





