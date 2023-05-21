﻿using CoreSharp.Http.FluentApi.Steps.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSharp.Http.FluentApi.Steps;

/// <inheritdoc cref="IStreamResponse" />
internal class StreamResponse : Response, IStreamResponse
{
    // Constructors
    public StreamResponse(IMethod method)
        : base(method)
    {
    }

    // Methods
    async Task<Stream> IStreamResponse.SendAsync(CancellationToken cancellationtoken)
    {
        using var response = await SendAsync(cancellationtoken);
        if (response is null)
        {
            return null;
        }

        return await response?.Content.ReadAsStreamAsync(cancellationtoken);
    }
}
