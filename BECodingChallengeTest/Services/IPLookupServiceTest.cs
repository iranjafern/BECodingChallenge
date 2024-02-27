using BEBusinessService.implementations;
using BEBusinessService.interfaces;
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
        public async Task GetIPLookUpTest()
        {
            var cityLocation = await SetupTest().GetIPLookUp("175.38.82.173");

            Assert.NotNull(cityLocation);
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
        private IIPLookupService SetupTest()
        {
            //handlerMock = new Mock<HttpMessageHandler>();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{""ip"": ""175.38.82.173"",""hostname"": """",""city"": ""Ballarat"",""region"": ""Victoria"",""country"": """",""loc"": """",""org"": """",""postal"": """",""timezone"": """"}]")
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