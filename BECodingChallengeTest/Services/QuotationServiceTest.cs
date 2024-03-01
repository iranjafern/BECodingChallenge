using BEBusinessService.implementations;
using BEBusinessService.interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;

namespace BECodingChallengeTest.Services
{
    [TestFixture]
    public class QuotationServiceTest
    {
        private readonly Mock<HttpMessageHandler>  handlerMock = new Mock<HttpMessageHandler>();
        [Test]
        public async Task GetIPLookUpTest()
        {
            var cityLocation = await SetupTest().GetPassengersWithTotal(3);

            Assert.NotNull(cityLocation);
            handlerMock.Protected().Verify(
              "SendAsync",
              Times.Exactly(1),
              ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
              ItExpr.IsAny<CancellationToken>());
        }

        /// <summary>
        /// Mock QuotationService
        /// </summary>
        /// <returns>QuotationService</returns>
        private IQuotationService SetupTest()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"[{""from"":""Sydney Airport (SYD), T1 International Terminal"",""to"":""46 Church Street, Parramatta NSW, Australia"",""listings"":[{""name"":""Listing 1"",""pricePerPassenger"":47.82,""vehicleType"":{""name"":""Hatchback"",""maxPassengers"":2}}]}]")
            };

            handlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);
            var httpClient = new HttpClient(handlerMock.Object);

            var configurationQuatationUrlSectionMock = new Mock<IConfigurationSection>();            
            var configurationMock = new Mock<IConfiguration>();

            configurationQuatationUrlSectionMock
               .Setup(x => x.Value)
               .Returns("https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest");

            configurationMock
               .Setup(x => x.GetSection("QuotationService:URL"))
               .Returns(configurationQuatationUrlSectionMock.Object);            

            return new QuotationService(httpClient, configurationMock.Object);            
        }
    }
}