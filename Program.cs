namespace MCSDKTest;
using SFMC.TLSProvisioning.MarketingCloud;
using SFMC.TLSProvisioning.MarketingCloud.SoapApi;
class Program
{
    static string user_name = Environment.GetEnvironmentVariable("api_user_name") ?? "";
    static string password = Environment.GetEnvironmentVariable("api_user_password") ?? "";
    static string soap_url = Environment.GetEnvironmentVariable("soap_endpoint") ?? "";

    const string deName = "TEST_SOAP_LIB";

    static IDataExtensionServiceOptions options = new DataExtensionServiceOptions(user_name, password, soap_url);
    static DataExtensionService service = new DataExtensionService(options);

    static async Task Main(string[] args)
    {
        await TestReadAll();
        await TestAnUpsert();
        await TestReadAll();
        await TestBatchDelete();
        await TestReadAll();


        Console.WriteLine("**********\nDemo Filtered\n********");

        await TestReadFiltered();
    }

    static async Task TestAnUpsert(){
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

    static async Task TestReadAll(){
        var results = await service.ReadAsync(deName, new string[]{"SubscriberKey", "ExpirationDate", "EID"});

        Console.WriteLine("Read All:");
        PrintResults(results);
    }

    static async Task TestReadFiltered(){
        var filter = new SimpleFilterPart(){
            Property = "EID",
            SimpleOperator = SimpleOperators.equals,
            Value = new string[]{ "1237" }
        };

        var results = await service.ReadAsync(deName, new string[]{"SubscriberKey", "ExpirationDate", "EID"}, filter);
        Console.WriteLine("Read Filtered: ");
        PrintResults(results);
    }

    static void PrintResults(List<DataExtensionObject> results) {
         foreach (var r in results) {
            Console.WriteLine("\tobj:");
            foreach (var p in r.Properties) {
                Console.WriteLine($"\t\t{p.Name}: {p.Value}");
            }
        }
    }
}
