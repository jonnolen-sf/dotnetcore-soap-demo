using Newtonsoft.Json;
using System.Text;
using System.ServiceModel.Channels;

namespace MCSDKTest;


class Program
{
    static string user_name = Environment.GetEnvironmentVariable("api_user_name");
    static string password = Environment.GetEnvironmentVariable("api_user_password");
    static string soap_url = Environment.GetEnvironmentVariable("soap_endpoint");


    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        await MakeSoapRequest();
    }

    static async Task MakeSoapRequest(){

        System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
        binding.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.TransportWithMessageCredential;
        binding.Security.Message.ClientCredentialType = System.ServiceModel.BasicHttpMessageCredentialType.UserName;
        binding.ReceiveTimeout = new TimeSpan(0,5,0);
        binding.OpenTimeout = new TimeSpan(0,5,0);
        binding.CloseTimeout = new TimeSpan(0,5,0);
        binding.SendTimeout = new TimeSpan(0,5,0);

        var address = new System.ServiceModel.EndpointAddress(soap_url);
        var sc = new ServiceReference.SoapClient(binding, address);

        sc.ClientCredentials.UserName.UserName = user_name;
        sc.ClientCredentials.UserName.Password = password;


        var deRequest = new ServiceReference.RetrieveRequest();

        deRequest.ObjectType = "DataExtensionObject[EIDEmailMapping_DE]";
        deRequest.Properties = new string[] { "Email", "EID", "ID" };

        var result = await sc.RetrieveAsync(new ServiceReference.RetrieveRequest1(deRequest));

        Console.WriteLine(result);
    }
}
