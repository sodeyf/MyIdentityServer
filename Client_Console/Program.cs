// See https://aka.ms/new-console-template for more information

using IdentityModel.Client;
using System.Text.Json;

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7001");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "client1",
    ClientSecret = "secret1",
    Scope = "api2 api1"
});

//var tokenResponse2 = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
//{
//    Address = disco.TokenEndpoint,
//    ClientId = "client1",
//    ClientSecret = "secret3",
//    Scope = "api1"
//});

//var tokenResponse3 = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
//{
//    Address = disco.TokenEndpoint,
//    ClientId = "client2",
//    ClientSecret = "secret2",
//    Scope = "api1 api3 api4"
//});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    Console.WriteLine(tokenResponse.ErrorDescription);
    return;
}

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken!); // AccessToken is always non-null when IsError is false

var response = await apiClient.GetAsync("https://localhost:7002/identity");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
    Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
}

Console.WriteLine(tokenResponse.AccessToken);

Console.WriteLine("Hello, World!");
