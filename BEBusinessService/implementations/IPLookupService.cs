﻿using BEBusinessService.interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace BEBusinessService.implementations
{
    public class IPLookupService : IIPLookupService
    {
        private readonly HttpClient httpClient;
        private const string _baseUrl = "https://ipinfo.io/";
        private const string _token = "54b075ffd917a7";

        public IPLookupService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        
        async Task<CityLocation> IIPLookupService.GetIPLookUp(string ipAddress)
        {
            string url = string.Concat(_baseUrl, ipAddress, "?token=", _token);
            var cityLocation = new CityLocation();
            var httpResponseMessage = await httpClient.GetAsync(url);
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var ipLookUp = JsonSerializer.Deserialize<IPLookUp>(responseContent);

            if (ipLookUp != null)
                cityLocation = new CityLocation { City = ipLookUp.City, Loc = ipLookUp.Loc };

            return await Task.FromResult(cityLocation);
        }
    }
}
