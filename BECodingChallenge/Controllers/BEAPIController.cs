using BEBusinessService.implementations;
using BEBusinessService.interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BECodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BEAPIController : ControllerBase
    {
        ICandidateService _candidateService;
        IIPLookupService _ipLookupService;
        IQuotationService _quotationService;
        public BEAPIController(ICandidateService candidateService, IIPLookupService ipLookupService, IQuotationService quotationService)
        {
            _candidateService = candidateService;
            _ipLookupService = ipLookupService;
            _quotationService = quotationService;
        }


        [HttpGet]
        [Route("candidate")]
        public async Task<ActionResult<Candidate>> GetCandidate()
        {
            return await _candidateService.GetCandidate();
        }

        [HttpGet]
        [Route("location/{ipaddress}")]
        public async Task<ActionResult<CityLocation>> GetIPLookUp(string ipaddress)
        {
            return await _ipLookupService.GetIPLookUp(ipaddress);
        }

        [HttpGet]
        [Route("listings/{numberofpassangers}")]
        public async Task<ActionResult<TotalPassangers>> GetPassengersWithTotal(int numberofpassangers)
        {
            return await _quotationService.GetPassengersWithTotal(numberofpassangers);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature =   HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(detail: exceptionHandlerFeature.Error.StackTrace, title: exceptionHandlerFeature.Error.Message);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();
    }
}
