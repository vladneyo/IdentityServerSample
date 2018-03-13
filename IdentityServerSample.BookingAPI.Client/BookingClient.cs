using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServerSample.Shared.Constants;
using IdentityServerSample.Shared.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace IdentityServerSample.BookingAPI.Client
{
    public class BookingClient<T> where T: class
    {
        public async Task<T> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(await FetchToken());

                var response = await client.GetAsync($"{EndpointsConstants.BookingAPI}/api/{url}");
                HandleError(response);
                var resContent = await response.Content.ReadAsStringAsync();
                return (T) PrepareResult(resContent);
            }
                
        }

        public async Task<T> PostAsync(string url, T data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(await FetchToken());

                var reqContent = PrepareContent(data);
                var response = await client.PostAsync($"{EndpointsConstants.BookingAPI}/api/{url}", reqContent);
                HandleError(response);
                var resContent = await response.Content.ReadAsStringAsync();
                return (T) PrepareResult(resContent);
            }

        }

        public async Task<T> PutAsync(string url, T data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(await FetchToken());

                var reqContent = PrepareContent(data);
                var response = await client.PutAsync($"{EndpointsConstants.BookingAPI}/api/{url}", reqContent);
                HandleError(response);
                var resContent = await response.Content.ReadAsStringAsync();
                return (T) PrepareResult(resContent);
            }

        }

        public async Task<T> DeleteAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(await FetchToken());

                var response = await client.DeleteAsync($"{EndpointsConstants.BookingAPI}/api/{url}");
                HandleError(response);
                var resContent = await response.Content.ReadAsStringAsync();
                return (T) PrepareResult(resContent);
            }

        }

        private async Task<string> FetchToken()
        {
            var disco = await DiscoveryClient.GetAsync(EndpointsConstants.ISHost);
            var tokenClient = new TokenClient(disco.TokenEndpoint, ISClients.BookingAPIClientId, Secrets.BookingAPI);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(ISApiNames.BookingAPI);
            return tokenResponse.AccessToken;
        }

        private object PrepareResult(string resContent)
        {
            return JsonHelper.IsValidJson(resContent) && !(typeof(T) is string) ? JObject.Parse(resContent).ToObject(typeof(T)) : resContent;
        }

        private StringContent PrepareContent(object data)
        {
            return new StringContent(JsonConvert.SerializeObject(data, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }), 
                Encoding.UTF8, "application/json");
        }

        private void HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(response.StatusCode.ToString());
            }
        }
    }
}
