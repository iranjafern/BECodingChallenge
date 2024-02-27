using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEBusinessService.interfaces
{
    public interface ICandidateService
    {
        public Task<Candidate> GetCandidate(); 
    }
}
