using BEBusinessService.interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BEBusinessService.implementations
{
    public class QuotationService : IQuotationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public QuotationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        async Task<TotalPassangers> IQuotationService.GetPassengersWithTotal(int nuberOfPassagers)
        {
            TotalPassangers totalPassangers = new TotalPassangers();
            string url = "https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var quote = await JsonSerializer.DeserializeAsync<Quote>(contentStream);

                if (quote != null)
                {
                    totalPassangers.Passengers = quote.Passengers.Where(x => x.VehicleType.MaxPassengers == nuberOfPassagers).ToList();
                    totalPassangers.Total = totalPassangers.Passengers.Sum(x => x.PricePerPassenger);
                }                    
            }
            
            return await Task.FromResult(totalPassangers);
        }        
    }
}
