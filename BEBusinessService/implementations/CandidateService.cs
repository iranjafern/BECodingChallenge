using BEBusinessService.interfaces;
using Models;
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
