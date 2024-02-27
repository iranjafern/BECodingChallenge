using Models;

namespace BEBusinessService.interfaces
{
    public interface IQuotationService
    {
        Task<TotalPassangers> GetPassengersWithTotal(int nuberOfPassagers);
    }
}
