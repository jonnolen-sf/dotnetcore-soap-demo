## POC using dotnet-svcutil and .Net core for SOAP


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
