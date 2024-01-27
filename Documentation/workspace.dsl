workspace "DocuEye" "DocuEye Architecture" {
    
    !identifiers hierarchical

    !docs docs

    model {

        teammember = person "Development Team Member"

        cicd = softwareSystem "CI/CD Automation System"

        docueye = softwareSystem "DocuEye" {
            app = container "DocuEye.Web" "DocuEye web application" "ASP.NET" {
                spa = component "SPA Application" "Provides functionality to the docueye users" "React"
                changeTracker = component "ChangeTracker" "Tracks model changes"
                importer = component "WorkspaceImporter" "Imports workspace"
                modelkeeper = component "ModelKeeper" "Implements storing and reading model data"
                workspacesKeeper = component "WorkspacesKeeper" "Implements storing and reading worspace data"
                viewsKeeper = component "ViewsKeeper" "Implements storing and reading views data"
                docsKeeper = component "DocsKeeper" "Implements storing and reading documentation data"
            }

            database = container "DataBase" "Stores all data for workspaces" "Mongo DB" {
                group "ChangeTracker" {
                    elementchanges = component "ElementChanges" "Stores imformations about element changes" "Mongo DB Collection"
                }

                group "ModelKeeper" {
                    relationships = component "Relationships" "Stores relationships data" "Mongo DB Collection"
                    elements = component "Elements" "Stores elemnents data" "Mongo DB Collection"
                }

                group "WorkspaceKeeper" {
                    workspaces = component "Workspaces" "Stores informations about workspaces" "Mongo DB Collection"
                    viewconfig = component "ViewConfigurations" "Stores informations about worspaces view configuration" "Mongo DB Collection"
                }

                group "DocsKeeper" {
                    decisions = component "Decisions" "Stores ADRs data" "Mongo DB Collection"
                    documentations = component "Documentations" "Stores documentaion data" "Mongo DB Collection"
                    images = component "Images" "Stores images used in documentation" "Mongo DB Collection"
                }

                group "ViewsKeeper" {
                    viewsc = component "Views" "Stores views data" "Mongo DB Collection"
                }

                group "WorkspaceImporter" {
                    worspaceimports = component "WorkspaceImports" "Stores worspaces imports data" "Mongo DB Collection"
                }
            }
        } 
        
        teammember -> docueye.app "Views documentation, diagrams, software catalog"

        cicd -> docueye.app "Imports workspaces chnages to"

        docueye.app -> docueye.database "Writes and reads data"

        docueye.app.spa -> docueye.app.changeTracker "Reads data about model changes"
        docueye.app.spa -> docueye.app.modelkeeper "Reads data about model"
        docueye.app.spa -> docueye.app.workspacesKeeper "Reads data about workspace"
        docueye.app.spa -> docueye.app.viewsKeeper "Reads data about views"
        docueye.app.spa -> docueye.app.docsKeeper "Reads data about documentaion"

        docueye.app.importer -> docueye.app.changeTracker "Sends events to"
        docueye.app.importer -> docueye.app.modelkeeper "Sends write data commands and reads data"
        docueye.app.importer -> docueye.app.workspacesKeeper "Sends write data commands and reads data"
        docueye.app.importer -> docueye.app.viewsKeeper "Sends write data commands and reads data"
        docueye.app.importer -> docueye.app.docsKeeper "Sends write data commands and reads data"
        docueye.app.importer -> docueye.database.worspaceimports "Writes and Reads data"

        docueye.app.changeTracker -> docueye.database.elementchanges "Writes and Reads data"
        docueye.app.modelkeeper -> docueye.database.relationships "Writes and Reads data"
        docueye.app.modelkeeper -> docueye.database.elements "Writes and Reads data"
        docueye.app.workspacesKeeper -> docueye.database.workspaces "Writes and Reads data"
        docueye.app.workspacesKeeper -> docueye.database.viewconfig "Writes and Reads data"
        docueye.app.docsKeeper -> docueye.database.decisions "Writes and Reads data"
        docueye.app.docsKeeper -> docueye.database.documentations "Writes and Reads data"
        docueye.app.docsKeeper -> docueye.database.images "Writes and Reads data"
        docueye.app.viewsKeeper -> docueye.database.viewsc "Writes and Reads data"
        

    }

    views {
        systemlandscape "SystemLandscape" {
            include *
            autoLayout
        }
        systemContext docueye "DocuEyeContext" {
            include * 
            autoLayout
        }

        container docueye "DocuEyeContainers" { 
            include * 
            autoLayout
        }

        component docueye.app "DocuEyeAppComponents" { 
            include * 
            autoLayout
        }

        component docueye.database "DocuEyeDatabaseComponents" { 
            include * 
            autoLayout
        }

        themes default
    }
    
}