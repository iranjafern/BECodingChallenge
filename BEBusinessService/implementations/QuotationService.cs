using BEBusinessService.interfaces;
using Models;
using System.Text.Json;

namespace BEBusinessService.implementations
{
    public class QuotationService : IQuotationService
    {
        private readonly HttpClient httpClient;
        private const string _baseUrl = "https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";
        public QuotationService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        async Task<TotalPassangers> IQuotationService.GetPassengersWithTotal(int nuberOfPassagers)
        {
            var totalPassangers = new TotalPassangers();
            var httpResponseMessage = await httpClient.GetAsync(_baseUrl);
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var quote = JsonSerializer.Deserialize<Quote>(responseContent);

            if (quote != null)
            {
                totalPassangers.Passengers = quote.Passengers.Where(x => x.VehicleType.MaxPassengers == nuberOfPassagers).ToList();
                totalPassangers.Total = totalPassangers.Passengers.Sum(x => x.PricePerPassenger);
            }

            return await Task.FromResult(totalPassangers);
        }        
    }
}
