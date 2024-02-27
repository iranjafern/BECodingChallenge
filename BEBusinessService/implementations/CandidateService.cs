using BEBusinessService.interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEBusinessService.implementations
{
    public class CandidateService : ICandidateService
    {
        public CandidateService()
        {
            
        }               

        async Task<Candidate> ICandidateService.GetCandidate()
        {
            var candidate = new Candidate { Name = "test", Phone = "test" };
            return await Task.FromResult(candidate);
        }
    }
}
