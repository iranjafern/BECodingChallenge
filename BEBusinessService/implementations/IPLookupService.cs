using BEBusinessService.interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using System.Text.Json;

namespace BEBusinessService.implementations
{
    public class IPLookupService(HttpClient httpClient, IConfiguration configuration) : IIPLookupService
    {
        async Task<CityLocation> IIPLookupService.GetIPLookUp(string ipAddress)
        {
            string url = string.Concat(configuration.GetValue<string>("IPLookup:BaseURL"), ipAddress, "?token=", configuration.GetValue<string>("IPLookup:Token"));
            var cityLocation = new CityLocation();
            var httpResponseMessage = await httpClient.GetAsync(url);
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var ipLookUp = JsonSerializer.Deserialize<IPLookUp>(responseContent);

            if (ipLookUp != null)
            {
                if(string.IsNullOrEmpty(ipLookUp.Bogon))
                    cityLocation = new CityLocation { City = ipLookUp.City, Loc = ipLookUp.Loc, IsValidIP = true };
                else
                    cityLocation = new CityLocation { City = string.Empty, Loc = "", IsValidIP = false };
            }

            return await Task.FromResult(cityLocation);
        }
    }
}
