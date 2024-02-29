using BEBusinessService.implementations;
using BEBusinessService.interfaces;
using Models;
using Moq;
using Moq.Protected;
using System.Net;

namespace BECodingChallengeTest.Services
{
    [TestFixture]
    public class IPLookupServiceTest
    {
        private readonly Mock<HttpMessageHandler>  handlerMock = new Mock<HttpMessageHandler>();
        [Test]
        public async Task GetIPLookUpPassTest()
        {
            string ipLookUpJsonString = @"{""ip"": ""175.38.82.173"",""hostname"": """",""city"": ""Ballarat"",""region"": ""Victoria"",""country"": """",""loc"": """",""org"": """",""postal"": """",""timezone"": """"}";

            var cityLocation = await SetupTest(ipLookUpJsonString).GetIPLookUp("175.38.82.173");

            Assert.NotNull(cityLocation);
            Assert.That(cityLocation.City, Is.EqualTo("Ballarat"));

            handlerMock.Protected().Verify(
              "SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
              ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public async Task GetIPLookUpFailTest()
        {
            string ipLookUpJsonString = @"{""ip"": ""172.22.22.222"",""bogon"": ""true""}";

            var cityLocation = await SetupTest(ipLookUpJsonString).GetIPLookUp("172.22.22.222");

            Assert.NotNull(cityLocation);
            Assert.That(cityLocation.IsValidIP, Is.False);

            handlerMock.Protected().Verify(
              "SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
              ItExpr.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Mock IPLookupService
        /// </summary>
        /// <returns>IPLookupService</returns>
        private IIPLookupService SetupTest(string ipLookUpJsonString)
        {
            

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(ipLookUpJsonString)
            };

            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);
            return new IPLookupService(httpClient);            
        }
    }
}