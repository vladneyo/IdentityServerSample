using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServerSample.Shared.Constants;
using Newtonsoft.Json.Linq;

namespace IdentityServerSample.WebAPIApp.ResourceOwner
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
            Console.ReadKey();
        }

        static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync(EndpointsConstants.ISHost);
            if (disco.IsError)
            {
                Console.WriteLine("DiscoveryClient error");
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, ISClients.WebAPIClientId, "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(ISApiNames.Api1);

            if (tokenResponse.IsError)
            {
                Console.WriteLine("tokenResponse error");
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync($"{EndpointsConstants.WebAPIApp}/api/values/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
