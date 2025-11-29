systemcontext internetBankingSystem "SystemContext" {
    include *
    animation {
        internetBankingSystem
        customer
        mainframe
        email
    }
    autoLayout
    description "The system context diagram for the Internet Banking System."
    properties {
        structurizr.groups false
    }
}