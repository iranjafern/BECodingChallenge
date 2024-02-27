using BEBusinessService.implementations;
using BEBusinessService.interfaces;


namespace BECodingChallengeTest.Services
{
    [TestFixture]
    public class BEAPIControllerTest
    {
        [Test]
        public async Task GetCandidateTest()
        {
            var candidate = await SetupTest().GetCandidate();

            Assert.That(candidate, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(candidate.Name, Is.EqualTo("test"));
                Assert.That(candidate.Phone, Is.EqualTo("test"));
            });
        }

        /// <summary>
        /// Mock CandidateService
        /// </summary>
        /// <returns>CandidateService</returns>
        private static ICandidateService SetupTest() => new CandidateService();
    }
}