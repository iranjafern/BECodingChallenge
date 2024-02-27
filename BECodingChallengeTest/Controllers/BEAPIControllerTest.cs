using BEBusinessService.implementations;
using BEBusinessService.interfaces;
using BECodingChallenge.Controllers;
using Models;
using Moq;

namespace BECodingChallengeTest.Controllers
{
    [TestFixture]
    public class BEAPIControllerTest
    {
        [Test]
        public void GetCandidateTest()
        {
            var candidate = SetupTest().GetCandidate().Result;

            Assert.That(candidate, Is.Not.Null);
            Assert.That(candidate.Value, Is.Not.Null);
            Assert.That(candidate.Value.Name, Is.EqualTo("test"));
            Assert.That(candidate.Value.Phone, Is.EqualTo("test"));
        }

        [Test]
        public void GetIPLookUpTest()
        {
            var ipLookUp = SetupTest().GetIPLookUp("175.38.82.173").Result;

            Assert.That(ipLookUp, Is.Not.Null);
            Assert.That(ipLookUp.Value, Is.Not.Null);
            Assert.That(ipLookUp.Value.City, Is.EqualTo("Melbourne"));
            Assert.That(ipLookUp.Value.Loc, Is.EqualTo("-37.5662,143.8496"));
        }

        [Test]
        public void GetPassengersWithTotalTest()
        {
            var totalPassangers = SetupTest().GetPassengersWithTotal(3).Result;

            Assert.That(totalPassangers, Is.Not.Null);
            Assert.That(totalPassangers.Value, Is.Not.Null);
            Assert.That(totalPassangers.Value.Passengers, Is.Not.Null);
            Assert.That(totalPassangers.Value.Total, Is.EqualTo(24));
        }

        /// <summary>
        /// Mock the repositories
        /// </summary>
        /// <returns>MailsService</returns>
        private BEAPIController SetupTest()
        {
            var candidateService = new Mock<ICandidateService>();
            candidateService.Setup(x => x.GetCandidate().Result).Returns(new Candidate { Name = "test", Phone = "test" });

            var ipLookupService = new Mock<IIPLookupService>();
            ipLookupService.Setup(x => x.GetIPLookUp("175.38.82.173").Result).Returns(new CityLocation { City = "Melbourne", Loc = "-37.5662,143.8496" });

            var iquotationService = new Mock<IQuotationService>();
            iquotationService.Setup(x => x.GetPassengersWithTotal(3).Result).Returns(new TotalPassangers { Passengers = new List<Passenger>(), Total = 24 });

            var beAPIController = new BEAPIController(candidateService.Object, ipLookupService.Object, iquotationService.Object);

            return beAPIController;

        }
    }
}