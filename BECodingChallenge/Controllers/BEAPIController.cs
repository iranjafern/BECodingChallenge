using BEBusinessService.implementations;
using BEBusinessService.interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Text.RegularExpressions;

namespace BECodingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BEAPIController(ICandidateService candidateService, IIPLookupService ipLookupService, IQuotationService quotationService) : ControllerBase
    {
        readonly ICandidateService _candidateService = candidateService;
        readonly IIPLookupService _ipLookupService = ipLookupService;
        readonly IQuotationService _quotationService = quotationService;
        private const string _ipFormatRegex = @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b";

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
            var match = Regex.Match(ipaddress, _ipFormatRegex, RegexOptions.IgnoreCase);
            if(!match.Success)
                return BadRequest();

            return await _ipLookupService.GetIPLookUp(ipaddress);
        }

        [HttpGet]
        [Route("listings/{numberofpassangers}")]
        public async Task<ActionResult<TotalPassangers>> GetPassengersWithTotal(int numberofpassangers)
        {
            if (numberofpassangers <= 0 || numberofpassangers > 1000)                
            {
                return BadRequest();
            }

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
