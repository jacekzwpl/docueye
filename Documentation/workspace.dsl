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
                issueTracker = component "IssueTracker" "Implements storing and reading issues data"
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
                    workspaceimports = component "WorkspaceImports" "Stores workspaces imports data" "Mongo DB Collection"
                }

                group "IssueTracker" {
                    issues = component "Issues" "Stores issues data" "Mongo DB Collection"
                }
            }
        } 
        
        teammember -> docueye.app "Views documentation, diagrams, software catalog" "HTTPS"

        cicd -> docueye.app "Imports workspaces chnages to" "HTTPS"

        docueye.app -> docueye.database "Writes and reads data" "TCP/27017"

        docueye.app.spa -> docueye.app.changeTracker "Reads data about model changes" "HTTPS"
        docueye.app.spa -> docueye.app.modelkeeper "Reads data about model" "HTTPS"
        docueye.app.spa -> docueye.app.workspacesKeeper "Reads data about workspace" "HTTPS"
        docueye.app.spa -> docueye.app.viewsKeeper "Reads data about views" "HTTPS"
        docueye.app.spa -> docueye.app.docsKeeper "Reads data about documentaion" "HTTPS"

        docueye.app.importer -> docueye.app.changeTracker "Sends events to" "HTTPS"
        docueye.app.importer -> docueye.app.modelkeeper "Sends write data commands and reads data" "HTTPS"
        docueye.app.importer -> docueye.app.workspacesKeeper "Sends write data commands and reads data" "HTTPS"
        docueye.app.importer -> docueye.app.viewsKeeper "Sends write data commands and reads data" "HTTPS"
        docueye.app.importer -> docueye.app.docsKeeper "Sends write data commands and reads data" "HTTPS"
        docueye.app.importer -> docueye.app.issueTracker "Sends write data commands and reads data" "HTTPS"
        docueye.app.importer -> docueye.database.workspaceimports "Writes and Reads data" "TCP/27017"

        docueye.app.changeTracker -> docueye.database.elementchanges "Writes and Reads data" "TCP/27017"
        docueye.app.modelkeeper -> docueye.database.relationships "Writes and Reads data" "TCP/27017"
        docueye.app.modelkeeper -> docueye.database.elements "Writes and Reads data" "TCP/27017"
        docueye.app.workspacesKeeper -> docueye.database.workspaces "Writes and Reads data" "TCP/27017"
        docueye.app.workspacesKeeper -> docueye.database.viewconfig "Writes and Reads data" "TCP/27017"
        docueye.app.docsKeeper -> docueye.database.decisions "Writes and Reads data" "TCP/27017"
        docueye.app.docsKeeper -> docueye.database.documentations "Writes and Reads data" "TCP/27017"
        docueye.app.docsKeeper -> docueye.database.images "Writes and Reads data" "TCP/27017"
        docueye.app.viewsKeeper -> docueye.database.viewsc "Writes and Reads data" "TCP/27017"
        docueye.app.issueTracker -> docueye.database.issues "Writes and Reads data" "TCP/27017"
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