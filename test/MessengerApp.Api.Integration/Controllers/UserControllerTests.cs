using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MessengerApp.Api.Integration.Controllers
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public UserControllerTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact(DisplayName = "Should log in user")]
        public async Task ShouldLogInUser()
        {
            var client = factory.CreateClient();
            var uri = "api/user/testusername/login";

            HttpResponseMessage response;
            string content;

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri))
            {
                response = await client.SendAsync(httpRequest);

                content = await response.Content.ReadAsStringAsync();
            }

            response.Should().NotBeNull()
                .And.Match<HttpResponseMessage>(message => message.StatusCode == HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Should retrieve user")]
        public async Task ShouldRetrieveUser()
        {
            var client = factory.CreateClient();
            var uri = "api/user/testusername";

            HttpResponseMessage response;
            string content;

            using (var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri))
            {
                var test = httpRequest.RequestUri;
                response = await client.SendAsync(httpRequest);

                content = await response.Content.ReadAsStringAsync();
            }

            response.Should().NotBeNull()
                .And.Match<HttpResponseMessage>(message => message.StatusCode == HttpStatusCode.OK);
        }
    }
}
