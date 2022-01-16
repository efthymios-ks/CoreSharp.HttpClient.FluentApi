﻿using CoreSharp.Models.Exceptions;
using System;

namespace CoreSharp.HttpClient.FluentApi.DelegateHandlers
{
    public class HttpResponseErrorHandlerOptions
    {
        //Properties 
        public Action<HttpResponseException> HandleError { get; set; }
    }
}
