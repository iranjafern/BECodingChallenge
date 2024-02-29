using BEBusinessService.interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace BEBusinessService.implementations
{
    public class IPLookupService(HttpClient httpClient) : IIPLookupService
    {
        private readonly HttpClient httpClient = httpClient;
        private const string _baseUrl = "https://ipinfo.io/";
        private const string _token = "54b075ffd917a7";

        async Task<CityLocation> IIPLookupService.GetIPLookUp(string ipAddress)
        {
            string url = string.Concat(_baseUrl, ipAddress, "?token=", _token);
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
