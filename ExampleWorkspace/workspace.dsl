workspace "Example Online Shop" "DEBUGTEST Example DocuEye workspace" {
    
    !identifiers hierarchical

    !docs docs/main 
    !adrs adr/main 

    

    model {
        !identifiers hierarchical
        client = person "Client" "Online shop client"

        emailsystem = softwareSystem "Email System" {
            test = container "Test" "Test" "Test"
        }

        onlineshop = softwareSystem "Online Shop" "Online shop system" {
            web = container "Web Application" {
                !docs docs/onlineshop/web
                !adrs adr/onlineshop/web
                description "Online web application for clients"
                technology "ASP.NET MVC"
                url "https://docueye.com"
                properties {
                    "docueye.ownerteam" "Web App Team"
                    "docueye.sourcecodeurl" "https://github.com/jacekzwpl/docueye"
                }
            }

            catalog = container "Products Catalog Service" {
                !docs docs/onlineshop/catalog
                description "Responsible for serving data about products"
                technology "REST API"
                properties {
                    "docueye.ownerteam" "Product Team"
                    "docueye.sourcecodeurl" "https://github.com/jacekzwpl/docueye"
                }
            } 

            catalogdb = container "Products Database" "Products persistence" "MSSQL"

            basket = container "Basket Service" {
                !docs docs/onlineshop/basket
                description "Responsible for processing basket operations"
                technology "REST API"
                properties {
                    "docueye.ownerteam" "Orders Team"
                    "docueye.sourcecodeurl" "https://github.com/jacekzwpl/docueye"
                }
            }

            basketdb = container "Basket Persistence" "Basket persistence" "REDIS"

            orders = container "Orders Processor Service" {
                !docs docs/onlineshop/orders
                description "Responsible for porcessing orders"
                technology "REST API"
                properties {
                    "docueye.ownerteam" "Orders Team"
                    "docueye.sourcecodeurl" "https://github.com/jacekzwpl/docueye"
                }
            }

            ordersdb = container "Orders Database" "Orders persistence" "MSSQL"

            payment = container "Payment Service" {
                !docs docs/onlineshop/payment
                description "Responsible for processing payments"
                technology "REST API"
                properties {
                    "docueye.ownerteam" "Orders Team"
                    "docueye.sourcecodeurl" "https://github.com/jacekzwpl/docueye"
                }
            }


        }

        relation1 = client -> onlineshop.web "Do shopping" "HTTPS"
        relation2 = onlineshop.web -> onlineshop.catalog "Reads data about products" "HTTPS"
        relation3 = onlineshop.web -> onlineshop.basket "Adds/Removes products from basket" "HTTPS"
        relation4 = onlineshop.web -> onlineshop.orders "Creates orders" "HTTPS"
        relation5 = onlineshop.web -> onlineshop.payment "Creates payments request" "HTTPS"

        relation6 = onlineshop.orders -> emailsystem "Sends mails using" "HTTPS"

        relation7 = onlineshop.catalog -> onlineshop.catalogdb "Reads/Writes data" "TCP 1433"
        relation8 = onlineshop.orders -> onlineshop.ordersdb "Reads/Writes data" "TCP 1433"
        relation9 = onlineshop.basket -> onlineshop.basketdb "Reads/Writes data" "TCP 6379"

        production = deploymentEnvironment "Production" {
            exchangeServer = deploymentNode "Exchange Server" {
                technology "Mail Server"
                emailsystemInstance = softwareSystemInstance emailsystem
            }

            webserver = deploymentNode "Web Server" {
                technology "IIS"
                webinstance = containerInstance onlineshop.web
            }

            kubernetes = deploymentNode "Kubernetes" {
                technology "Kubernetes Cluster"
                properties {
                    "docueye.ownerteam" "Operations Team"
                }
                catalogInstance = containerInstance onlineshop.catalog
                basketInstance = containerInstance onlineshop.basket
                ordersInstance = containerInstance onlineshop.orders
                paymentInstance = containerInstance onlineshop.payment
            }

            sqlserver = deploymentNode "SQL Server" {
                technology "SQL Server"
                catalogdbInstance = containerInstance onlineshop.catalogdb
                ordersdbInstance = containerInstance onlineshop.ordersdb
            }

            redisserver = deploymentNode "Redis Server" {
                technology "Redis Server"
                basketdbInstance = containerInstance onlineshop.basketdb
            }
        }
    }

    views {

        systemContext onlineshop "onlineshopContext" {
            title "Online Shop"
            include * 
            autoLayout tb
        }

        container onlineshop "onlineshopContainers" { 
            title "Online shop containers"
            include * 
            autoLayout tb
        }

        container emailsystem "emailsystemContainers" { 
            title "EmailSystem containers"
            include * 
            autoLayout tb
        }

        deployment onlineshop "Production" "onlineshopDeployment" { 
            title "Online shop deployment"
            include * 
            autoLayout tb
        }

        dynamic onlineshop "BuyProduct" "Summarises how the sign in feature works in the single-page application." {
            onlineshop.web -> onlineshop.catalog "Serarch product"
            onlineshop.catalog -> onlineshop.web "Return product"
            onlineshop.web -> onlineshop.basket "Add product to basket"
            onlineshop.web -> onlineshop.orders "Create order"
            onlineshop.web -> onlineshop.payment "Create payment"
            autoLayout
            description "Summarises how the sign in feature works in the single-page application."
        }

        themes default
    }
    
}