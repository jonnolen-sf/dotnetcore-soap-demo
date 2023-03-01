namespace MCSDKTest;
using SFMC.TLSProvisioning.MarketingCloud;
using SFMC.TLSProvisioning.MarketingCloud.SoapApi;
class Program
{
    static string user_name = Environment.GetEnvironmentVariable("api_user_name");
    static string password = Environment.GetEnvironmentVariable("api_user_password");
    static string soap_url = Environment.GetEnvironmentVariable("soap_endpoint");


    static async Task Main(string[] args)
    {
        await TestAnUpsert();

        await TestBatchDelete();

    }

    static async Task TestAnUpsert(){
        var options = new DataExtensionServiceOptions(user_name, password, soap_url);
        var service = new DataExtensionService(options);

        var deName = "TEST_SOAP_LIB";

        var objs = new List<Dictionary<string, string>>{
            new Dictionary<string, string>{
                {"SubscriberKey", "jnolen+soap1@gmail.com"},
                {"ExpirationDate", DateTime.Now.ToUniversalTime().ToString("o") },
                {"EID", "10001"}
            },
            new Dictionary<string, string>{
                {"SubscriberKey", "jon.nolen+soap666@gmail.com"},
                {"ExpirationDate", DateTime.Now.ToUniversalTime().ToString("o")},
                {"EID", "10002"}
            },
            new Dictionary<string, string>{
                {"SubscriberKey", "jon.nolen+soap667@gmail.com"},
                {"ExpirationDate", DateTime.Now.ToUniversalTime().ToString("o")},
                {"EID", "10003"}
            }
        };

        var result = await service.UpsertAsync(deName, objs);

        Console.WriteLine($"Upsert Result: {result.OverallStatus}");
    }

    static async Task TestBatchDelete() {
        var options = new DataExtensionServiceOptions(user_name, password, soap_url);
        var service = new DataExtensionService(options);

        var deName = "TEST_SOAP_LIB";

        var objs = new List<Dictionary<string, string>>{
            new Dictionary<string, string>{
                {"SubscriberKey", "jon.nolen+soap666@gmail.com"}
            },
            new Dictionary<string, string>{
                {"SubscriberKey", "jon.nolen+soap667@gmail.com"}
            }
        };

        var result = await service.DeleteAsync(deName, objs);

        Console.WriteLine($"Batch Delete result: {result.OverallStatus}");
    }

}
