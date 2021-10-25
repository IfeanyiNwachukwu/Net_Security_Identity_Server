using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNetSecurity.Consoles
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        
        private static async Task MainAsync()
        {
            var discoRo = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discoRo.IsError)
            {
                Console.WriteLine(discoRo.Error);
                return;
            }

            // Grab a Bearer token using ResourceOwnerPassword Grant Type
            var tokenClietRo = new TokenClient(discoRo.TokenEndpoint, "ro.client", "secret");
            var tokenResponseRo = await tokenClietRo.RequestResourceOwnerPasswordAsync("Manish","password","AspNetSecurityApi");

            if (tokenResponseRo.IsError)
            {
                Console.WriteLine(tokenResponseRo.Error);
                return;
            }
            Console.WriteLine(tokenResponseRo.Json);

            Console.WriteLine("\n\n");

            // Discover all the end points using metadata of Identity server
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // Grab a Bearer token
            var tokenCliet = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenCliet.RequestClientCredentialsAsync("AspNetSecurityApi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);

            Console.WriteLine("\n\n");

            // Consume Customer API

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);


            var customerInfo = new StringContent(
                JsonConvert.SerializeObject
                (
                    new {Id = 10, FirstName = "Manish", LastName = "Narayan"}
                 ), Encoding.UTF8, "application/json");

            var createCustomerResponse = await client.PostAsync("http://localhost:5001/api/Customers",customerInfo);

            if (!createCustomerResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(createCustomerResponse.StatusCode);
            }

            // Get customers

            var getCustomerRespones = await client.GetAsync("http://localhost:5001/api/Customers");
            if (!getCustomerRespones.IsSuccessStatusCode)
            {
                Console.WriteLine(getCustomerRespones.StatusCode);
            }
            else
            {
                var content = await getCustomerRespones.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.Read();

        }
    
    }
}
