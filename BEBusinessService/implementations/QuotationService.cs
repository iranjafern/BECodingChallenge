using BEBusinessService.interfaces;
using Microsoft.Extensions.Configuration;
using Models;
using System.Text.Json;

namespace BEBusinessService.implementations
{
    public class QuotationService(HttpClient httpClient, IConfiguration configuration) : IQuotationService
    {
        async Task<TotalPassangers> IQuotationService.GetPassengersWithTotal(int nuberOfPassagers)
        {
            var totalPassangers = new TotalPassangers();
            var httpResponseMessage = await httpClient.GetAsync(configuration.GetValue<string>("IPLookup:BaseURL"));
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
