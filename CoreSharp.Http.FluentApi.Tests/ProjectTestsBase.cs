using AutoFixture;
using CoreSharp.Http.FluentApi.Steps.Interfaces;
using System.Net;
using Tests.Common;
using Tests.Common.Mocks;

namespace CoreSharp.Http.FluentApi.Tests;

public abstract class ProjectTestsBase : TestsBase
{
    protected override void ConfigureFixture(IFixture fixture)
    {
        fixture.Register(() => new MockHttpMessageHandler()
        {
            HttpResponseMessageFactory = () => new HttpResponseMessage(HttpStatusCode.OK)
        });

        fixture.Register(() =>
        {
            var mockHttpMessageHandler = fixture.Create<MockHttpMessageHandler>();
            return new HttpClient(mockHttpMessageHandler);
        });

        fixture.Register(() =>
        {
            var request = Substitute.For<IRequest>();

            var httpClient = fixture.Create<HttpClient>();
            request.HttpClient.Returns(httpClient);
            request.HttpCompletionOption.Returns(HttpCompletionOption.ResponseContentRead);
            request.Headers.Returns(new Dictionary<string, string>());
            request.ThrowOnError.Returns(false);
            request.Timeout.Returns((TimeSpan?)null);

            return request;
        });

        fixture.Register(() =>
        {
            var endpoint = Substitute.For<IEndpoint>();

            var request = fixture.Create<IRequest>();
            endpoint.Request.Returns(request);
            endpoint.Endpoint.Returns("http://www.example.com/");
            endpoint.QueryParameters.Returns(new Dictionary<string, string>());

            return endpoint;
        });

        fixture.Register(() => Substitute.For<MemoryStream>());
    }
}
