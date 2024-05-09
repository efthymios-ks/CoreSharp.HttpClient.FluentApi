﻿using AutoFixture.NUnit3;
using CoreSharp.Http.FluentApi.Steps.Interfaces.Methods.SafeMethods;
using CoreSharp.Http.FluentApi.Steps.Methods.SafeMethods;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using Tests.Internal.Attributes;
using Tests.Internal.HttpmessageHandlers;
using JsonNet = Newtonsoft.Json;
using TextJson = System.Text.Json;

namespace Tests.Steps.SafeMethods;

[TestFixture]
public sealed class SafeMethodWithResultTests
{
    [Test]
    [AutoNSubstituteData]
    public void Constructor_WhenCalled_ShouldNotThrw(ISafeMethod safeMethod)
    {
        // Act
        Action action = () => _ = new SafeMethodWithResult(safeMethod);

        // Assert 
        action.Should().NotThrow();
    }

    [Test]
    [AutoNSubstituteData]
    public void ToBytes_WhenCalled_ShouldReturnSafeMethodWithResultAsBytes(ISafeMethod safeMethod)
    {
        // Arrange 
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);

        // Act
        var result = safeMethodWithResult.ToBytes();

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsBytes>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public void ToStream_WhenCalled_ShouldReturnSafeMethodWithResultAsStream(ISafeMethod safeMethod)
    {
        // Arrange 
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);

        // Act
        var result = safeMethodWithResult.ToStream();

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsStream>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public void ToString_WhenCalled_ShouldReturnSafeMethodWithResultAsString(ISafeMethod safeMethod)
    {
        // Arrange 
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);

        // Act
        var result = safeMethodWithResult.ToString();

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsString>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public void WithJsonDeserialize_WhenCalled_ShouldReturnSafeMethodWithResultFromJson(ISafeMethod safeMethod)
    {
        // Arrange 
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);

        // Act
        var result = safeMethodWithResult.WithJsonDeserialize<object>();

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsGeneric<object>>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public void WithJsonDeserialize_WhenJsonSerializerSettingsIsNull_ShouldThrowArgumentNullException(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        JsonNet.JsonSerializerSettings jsonSerializerSettings = null;

        // Act
        Action action = () => _ = safeMethodWithResult.WithJsonDeserialize<string>(jsonSerializerSettings);

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Test]
    [AutoNSubstituteData]
    public void WithJsonDeserialize_WhenJsonSerializerSettingsIsNotNull_ShouldReturnSafeMethodWithResultFromJson(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        var jsonSerializerSettings = new JsonNet.JsonSerializerSettings();

        // Act
        var result = safeMethodWithResult.WithJsonDeserialize<string>(jsonSerializerSettings);

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsGeneric<string>>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public async Task WithJsonDeserialize_WhenJsonSerializerSettingsIsNotNull_ShouldDeserializeResponseFromJson(
        [Frozen] MockHttpMessageHandler mockHttpMessageHandler,
        ISafeMethod safeMethod)
    {
        // Arrange
        var jsonSerializerSettings = new JsonNet.JsonSerializerSettings();
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        var expectedResponse = new PublicDummyClass
        {
            Message = "Data"
        };
        mockHttpMessageHandler.ResponseContent = JsonNet.JsonConvert.SerializeObject(expectedResponse);

        // Act
        var response = await safeMethodWithResult
            .WithJsonDeserialize<PublicDummyClass>(jsonSerializerSettings)
            .SendAsync();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Test]
    [AutoNSubstituteData]
    public void WithJsonDeserialize_WhenJsonSerializerOptionsIsNull_ShouldThrowArgumentNullException(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        TextJson.JsonSerializerOptions jsonSerializerOptions = null;

        // Act
        Action action = () => _ = safeMethodWithResult.WithJsonDeserialize<string>(jsonSerializerOptions);

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Test]
    [AutoNSubstituteData]
    public void WithJsonDeserialize_WhenJsonSerializerOptionsIsNotNull_ShouldReturnSafeMethodWithResultFromJson(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        var jsonSerializerOptions = TextJson.JsonSerializerOptions.Default;

        // Act
        var result = safeMethodWithResult.WithJsonDeserialize<string>(jsonSerializerOptions);

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsGeneric<string>>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public async Task WithJsonDeserialize_WhenJsonSerializerOptionsIsNotNull_ShouldDeserializeResponseFromJson(
        [Frozen] MockHttpMessageHandler mockHttpMessageHandler,
        ISafeMethod safeMethod)
    {
        // Arrange
        var jsonSerializerOptions = TextJson.JsonSerializerOptions.Default;
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        var expectedResponse = new PublicDummyClass
        {
            Message = "Data"
        };
        mockHttpMessageHandler.ResponseContent = TextJson.JsonSerializer.Serialize(expectedResponse);

        // Act
        var response = await safeMethodWithResult
            .WithJsonDeserialize<PublicDummyClass>(jsonSerializerOptions)
            .SendAsync();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Test]
    [AutoNSubstituteData]
    public void WithXmlDeserialize_WhenCalled_ShouldReturnSafeMethodWithResultFromXml(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);

        // Act
        var result = safeMethodWithResult.WithXmlDeserialize<object>();

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsGeneric<object>>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public async Task WithXmlDeserialize_WhenCalled_ShouldDeserializeResponseFromXml(
        [Frozen] MockHttpMessageHandler mockHttpMessageHandler,
        ISafeMethod safeMethod)
    {
        // Arrange 
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        var expectedResponse = new PublicDummyClass
        {
            Message = "Data"
        };

        mockHttpMessageHandler.ResponseContent = @"
        <PublicDummyClass xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
            <Message>Data</Message>
        </PublicDummyClass>";

        // Act
        var response = await safeMethodWithResult
            .WithXmlDeserialize<PublicDummyClass>()
            .SendAsync();

        // Assert
        response.Should().BeEquivalentTo(expectedResponse);
    }

    [Test]
    [AutoNSubstituteData]
    public void WithGenericDeserialize_WhenDeserializeStringFunctionIsNull_ShouldThrowArgumentNullException(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        Func<string, string> deserializeStringFunction = null;

        // Act
        Action action = () => _ = safeMethodWithResult.WithGenericDeserialize(deserializeStringFunction);

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Test]
    [AutoNSubstituteData]
    public void WithGenericDeserialize_WhenDeserializeStringFunctionIsNotNull_ShouldReturnSafeMethodWithResultFromJson(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        static string DeserializeStringFunction(string value)
            => null;

        // Act
        var result = safeMethodWithResult.WithGenericDeserialize(DeserializeStringFunction);

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsGeneric<string>>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    [Test]
    [AutoNSubstituteData]
    public void WithGenericDeserialize_WhenDeserializeStreamFunctionIsNull_ShouldThrowArgumentNullException(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        Func<Stream, string> deserializeStringFunction = null;

        // Act
        Action action = () => _ = safeMethodWithResult.WithGenericDeserialize(deserializeStringFunction);

        // Assert
        action.Should().ThrowExactly<ArgumentNullException>();
    }

    [Test]
    [AutoNSubstituteData]
    public void WithGenericDeserialize_WhenDeserializeStreamFunctionIsNotNull_ShouldReturnSafeMethodWithResultFromJson(ISafeMethod safeMethod)
    {
        // Arrange
        var safeMethodWithResult = new SafeMethodWithResult(safeMethod);
        static string DeserializeStreamFunction(Stream stream)
            => null;

        // Act
        var result = safeMethodWithResult.WithGenericDeserialize(DeserializeStreamFunction);

        // Assert
        result.Should().BeOfType<SafeMethodWithResultAsGeneric<string>>();
        result.Endpoint.Should().BeSameAs(safeMethod.Endpoint);
        result.HttpMethod.Should().Be(safeMethod.HttpMethod);
    }

    public sealed class PublicDummyClass
    {
        public string Message { get; set; }
    }
}
