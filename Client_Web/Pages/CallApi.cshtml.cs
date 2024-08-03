using Common.MyConstants;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace MyApp.Namespace
{
    public class CallApiModel(IHttpClientFactory httpClientFactory) : PageModel
    {
        public string Json = string.Empty;
        public async Task OnGet()
        {
            //---------------------- method 1
            var tokenInfo = await HttpContext.GetUserAccessTokenAsync();
            var client1 = new HttpClient();
            client1.SetBearerToken(tokenInfo.AccessToken!);

            var content1 = await client1.GetStringAsync($"{MyUrls.ApiResource}/identity");

            var parsed1 = JsonDocument.Parse(content1);
            var formatted1 = JsonSerializer.Serialize(parsed1, new JsonSerializerOptions { WriteIndented = true });

            //--------------------- method 2
            var client2 = httpClientFactory.CreateClient(MyOtherConstants.ApiResourceClient);

            var content2 = await client2.GetStringAsync($"{MyUrls.ApiResource}/identity");

            var parsed2 = JsonDocument.Parse(content2);
            var formatted2 = JsonSerializer.Serialize(parsed2, new JsonSerializerOptions { WriteIndented = true });

            Json = formatted2;
        }
    }
}
