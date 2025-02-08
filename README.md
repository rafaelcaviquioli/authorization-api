# idp-discovery
This demo project has endpoints used by AD B2C custom policies on user authentication flows.

## IDP Discovery endpoint

[Check the article for more details.](https://medium.com/@rafaelcaviquioli)

The IDP discovery endpoint is used to redirect the user to the appropriate identity provider based on the domain of the email address entered by the user. The endpoint is implemented as an Azure Function and is deployed to Azure. The endpoint is called from the custom policy to determine the identity provider to redirect the user to.