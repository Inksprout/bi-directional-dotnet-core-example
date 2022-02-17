using PactNet;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Consumer;
using System.Collections.Generic;
using System.Net.Http;
using PactNet.Matchers;

namespace tests
{
    public class ConsumerPactTests
    {
        private readonly IPactBuilderV3 pact;
        private readonly ProductClient apiClient;
        private readonly int port = 9000;
        private List<Product> products;

        public ConsumerPactTests(ITestOutputHelper output)
        {
            var config = new PactConfig
            {
                PactDir = "../../../pacts/",
                LogLevel = PactLogLevel.Debug,
                Outputters = (new[]
           {
                // NOTE: We default to using a ConsoleOutput, however xUnit 2 does not capture the console output,
                // so a custom outputter is required.
                new XUnitOutput(output)
            }),
                DefaultJsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };

            // you select which specification version you wish to use by calling either V2 or V3
            IPactV3 pact = Pact.V3("My Consumer", "My Provider", config);

            // the pact builder is created in the constructor so it's unique to each test
            this.pact = pact.UsingNativeBackend(port);
        }

        [Fact]
        public async void GetProducts_WhenCalled_ReturnsAllEvents()
        {
            //Arrange
           products = new List<Product>()
            {
                new Product { id = 9, type = "CREDIT_CARD", name = "GEM Visa"},
                new Product { id = 10, type = "CREDIT_CARD", name = "28 Degrees"}
            };

            pact
                .UponReceiving("a request to retrieve all products")
                .WithRequest(HttpMethod.Get, "/products")
                .WillRespond()
                .WithStatus(System.Net.HttpStatusCode.OK)
                .WithHeader("Content-Type", "appliction/json; charset=utf-8")
                .WithJsonBody(new
                {
                    id = Match.Type("27"),
                    name = "burger",
                    type = "food"
                });


            //Act
            await pact.VerifyAsync(async ctx =>
            {
                var client = new ProductClient();
               var products = await client.GetProducts(ctx.MockServerUri.AbsoluteUri, null);

                //Assert
                
                //Assert.Equal(System.Net.÷HttpStatusCode.OK, products.StatusCode);
            });


            //the mock server is no longer running once VerifyAsync returns

        }

    }
}
