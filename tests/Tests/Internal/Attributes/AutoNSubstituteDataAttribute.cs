﻿using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.NUnit3;
using CoreSharp.Http.FluentApi.Services;
using CoreSharp.Http.FluentApi.Steps;
using CoreSharp.Http.FluentApi.Steps.Interfaces;
using CoreSharp.Http.FluentApi.Utilities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Tests.Internal.HttpmessageHandlers;

namespace Tests.Internal.Attributes;

public sealed class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
        : base(GetFixture)
    {
    }

    private static IFixture GetFixture()
    {
        var fixture = new Fixture();
        fixture.Customize(new AutoNSubstituteCustomization()
        {
            ConfigureMembers = true
        });

        fixture.Register<IFixture>(() => fixture);

        fixture.Register<IHttpResponseMessageDeserializer>(() => new HttpResponseMessageDeserializer());

        fixture.Register(() => new MockHttpMessageHandler()
        {
            ResponseStatus = System.Net.HttpStatusCode.OK,
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
            request.QueryParameters.Returns(new Dictionary<string, string>());
            request.ThrowOnError.Returns(false);
            request.Timeout.Returns((TimeSpan?)null);
            request.HttpResponseMessageDeserializer = fixture.Create<IHttpResponseMessageDeserializer>();

            return request;
        });

        fixture.Register<IEndpoint>(() =>
        {
            var request = fixture.Create<IRequest>();
            return new Endpoint(request, "http://www.example.com/");
        });

        return fixture;
    }
}
