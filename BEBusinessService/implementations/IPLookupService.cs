using BEBusinessService.interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BEBusinessService.implementations
{
    public class IPLookupService : IIPLookupService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IPLookupService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        async Task<CityLocation> IIPLookupService.GetIPLookUp(string ipAddress)
        {
            CityLocation cityLocation = new CityLocation();
            string url = "https://ipinfo.io/" + ipAddress + "?token=54b075ffd917a7";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var ipLookUp = await JsonSerializer.DeserializeAsync<IPLookUp>(contentStream);
                
                if (ipLookUp != null)
                  cityLocation = new CityLocation { City = ipLookUp.City, Loc = ipLookUp.Loc };
            }

            //IPLookUp ipLookUp = new IPLookUp { City = "Test", HostName= "", Country = "", IP = "", Org = "", Loc = "", Postal = "", Region = "" };
            return await Task.FromResult(cityLocation);
        }
    }
}
