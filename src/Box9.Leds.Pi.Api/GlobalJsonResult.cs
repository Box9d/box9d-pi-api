using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api
{
    public class GlobalJsonResult<T>
    {
        public bool Successful { get; private set; }

        public T Result { get; private set; }

        public string ErrorMessage { get; private set; }

        public HttpStatusCode StatusCode { get; private set; }

        private GlobalJsonResult(bool successful, T result, string errorMessage, HttpStatusCode statusCode)
        {
            Successful = successful;
            Result = result;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public static GlobalJsonResult<EmptyResult> Error(Exception ex)
        {
            return new GlobalJsonResult<EmptyResult>(false, new EmptyResult(), ex.Message, HttpStatusCode.InternalServerError);
        }

        public static GlobalJsonResult<EmptyResult>Success(HttpStatusCode statusCode)
        {
            return new GlobalJsonResult<EmptyResult>(true, new EmptyResult(), null, statusCode);
        }

        public static GlobalJsonResult<T> Success(HttpStatusCode statusCode, T result)
        {
            return new GlobalJsonResult<T>(true, result, null, statusCode);
        }
    }
}