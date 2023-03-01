## POC using dotnet-svcutil and .Net core for SOAP

*Internal TLSProvisioning lib now has the the generated code for the soap api*

Install dotnet-svcutil using:
```
dotnet tool install --global dotnet-svcutil
```

Generated ServiceReference/Reference.cs using:
```
dotnet-svcutil https://{TSE}.soap.marketingcloudapis.com/etframework.wsdl
```

Generate an API user following instructions here with DataExtension permissions.

https://developer.salesforce.com/docs/marketing/marketing-cloud/guide/authenticate-soap-api.html#authenticate-with-usernametoken

Set the following environment variables:

| env var | description |
| --- | --- |
| api_user_name | *api user name* |
| api_user_password | *api user password* |
| soap_endpoint | *full tse soap endpoint including trailing /Service.asmx* |
