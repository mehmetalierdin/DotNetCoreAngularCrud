using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using DotNetCoreAngularCrudDataService;
using DotNetCoreAngularCrudDataService.Framework.Models;

namespace DotNetCoreAngularCrudDataService.Test
{
    public class ApiTest
    {
        private TestServer _server;
        private HttpClient _client;
        public ApiTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public void ToGetPresentationsWithNoAuthentication_ShouldBeUnAuthorize_WhenHeaderIsNull()
        {
            var url = "api/data";
            var expected = HttpStatusCode.Unauthorized;
            var response = _client.GetAsync(url);
            Assert.Equal(expected, response.Result.StatusCode);
        }

        [Theory, InlineData(new object[] { "testuser", "test" })]
        public void ToAccessAllEndpoints_ShouldGetToken_WhenBodyHasCredentials(string username, string password)
        {
            var url = "api/login";
            var data = new { username = username, password = password };
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = _client.PostAsync(url, content).Result;
            var resultContent = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<User>(resultContent);
            Assert.IsType<User>(user);
            Assert.NotNull(user.Token);
        }
    }
}
