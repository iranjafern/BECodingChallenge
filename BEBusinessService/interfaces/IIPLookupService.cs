using Models;

namespace BEBusinessService.interfaces
{
    public interface IIPLookupService
    {
        Task<CityLocation> GetIPLookUp(string ipAddress);
    }
}
