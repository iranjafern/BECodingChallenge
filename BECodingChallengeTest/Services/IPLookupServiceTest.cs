using BEBusinessService.implementations;
using BEBusinessService.interfaces;
using Microsoft.Extensions.Configuration;
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

            var configurationBaseUrlSectionMock = new Mock<IConfigurationSection>();
            var configurationTokenSectionMock = new Mock<IConfigurationSection>();
            var configurationMock = new Mock<IConfiguration>();

            configurationBaseUrlSectionMock
               .Setup(x => x.Value)
               .Returns("https://ipinfo.io/");

            configurationMock
               .Setup(x => x.GetSection("IPLookup:BaseURL"))
               .Returns(configurationBaseUrlSectionMock.Object);

            configurationTokenSectionMock
               .Setup(x => x.Value)
               .Returns("54b075ffd917a7");

            configurationMock
               .Setup(x => x.GetSection("IPLookup:Token"))
               .Returns(configurationTokenSectionMock.Object);

            return new IPLookupService(httpClient, configurationMock.Object);            
        }
    }
}