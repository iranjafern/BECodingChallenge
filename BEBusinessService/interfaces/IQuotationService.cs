using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEBusinessService.interfaces
{
    public interface IQuotationService
    {
        Task<TotalPassangers> GetPassengersWithTotal(int nuberOfPassagers);
    }
}
