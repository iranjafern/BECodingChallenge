using Models;

namespace BEBusinessService.interfaces
{
    public interface ICandidateService
    {
        public Task<Candidate> GetCandidate(); 
    }
}
