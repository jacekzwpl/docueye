## Configuration

Currently there are few configuration options that can be made by appsettings.json file or environment variables:  

| Config option | Environment Variable | Description | 
| --- | --- | ----- |
| `DocuEye:Database:ConnectionString` | `DocuEye__Database__ConnectionString` | Mongo db connection string ex. `mongodb://docueyeuser:docueyeuserpassword@databasehost:27017/` |
| `DocuEye:Database:Name` | `DocuEye__Database__Name` | The name of DocuEye database name |
| `DocuEye:AdminToken` | `DocuEye__AdminToken` | Admin token used for import | 
| `DocuEye:OIDC:Authority` | `DocuEye__OIDC__Authority` | Authority address ex: https://your-identity-server.com | 
| `DocuEye:OIDC:ClientId` | `DocuEye__OIDC__ClientId` | Application clinet_id| 
| `DocuEye:OIDC:ClientSecret` | `DocuEye__OIDC__ClientSecret` | Application client secret | 
| `DocuEye:OIDC:Scopes` | `DocuEye__OIDC__Scopes` | comma separated application scopes to be included in authorization request. Default value is: openid,profile,email | 
| `DocuEye:OIDC:ClaimType` | `DocuEye__OIDC__ClaimType` | Claim type witch will be used for check for user permissions. Default value is email | 
