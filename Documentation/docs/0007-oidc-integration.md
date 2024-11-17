## OIDC Integration

DocuEye can be integrated with identity providers that are compatible with OIDC such as Keycloack.  
If OIDC configuration is present then access to the application will be restricted only to authenticated users and access to workspaces can be restricted to specific users.  
All configuration options can be found [here](0004-configuration.md).  

The default configuration for each workspace is public. This means every authenticated user has full access. To restrict access to workspace some additional configuration in dsl file have to be done. For example:  
```
configuration {
    visibility private
    users {
        user_name read
    }
}
```
This configuration makes workspace private and gives access only to one user. You can read more about this configuration in structurizr dsl documentation [here](https://docs.structurizr.com/dsl/language#configuration).  

> Note that this DocuEye use memory cache for keeping this information. Interval for refreshing data is 5 minutes, so any changes in this won't take effect immediately after importing new configuration.

During authorization flow DocuEye compares user_name value from workspace configuration with specified claim which type can be configured. This can be done using `DocuEye__OIDC__ClaimType` environment variable. Default value is `email`. 

DocuEye uses [authorization code flow](https://auth0.com/docs/get-started/authentication-and-authorization-flow/authorization-code-flow) for default. Also [authorization code flow with pkce](https://auth0.com/docs/get-started/authentication-and-authorization-flow/authorization-code-flow-with-pkce) can be used. To use PKCE set `DocuEye__OIDC__UsePKCE` environment variable to `1`.

Additional redirect endpoints for oidc provider are:  
| Endpoint | Description |
| -- | ----|
| `/signin-oidc` | Where to redirect after login |
| `/signout-callback-oidc` | Where to redirect after logout |








