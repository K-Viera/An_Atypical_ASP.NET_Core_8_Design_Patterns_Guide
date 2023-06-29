using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.IntegrationTest.Controllers
{
    public class ValuesControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ValuesControllerTest(WebApplicationFactory<Program> webApplicationFactory)
        {
            _httpClient = webApplicationFactory.CreateClient();
        }

        public class Get : ValuesControllerTest
        {
            public Get(WebApplicationFactory<Program> webApplicationFactory) : base(webApplicationFactory) { }

            [Fact]
            public async Task Should_respond_a_status_200_OK()
            {
                // Act
                var result = await _httpClient.GetAsync("/api/Values");

                // Assert
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            }

            [Fact]
            public async Task Should_respond_the_expected_strings()
            {
                // Act
                var result = await _httpClient.GetFromJsonAsync<string[]>("/api/values");

                // Assert
                Assert.Collection(result,
                    x => Assert.Equal("value1", x),
                    x => Assert.Equal("value2", x)
                );
            }
        }
    }
}
