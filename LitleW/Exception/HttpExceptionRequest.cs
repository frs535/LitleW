using System;
using System.Net.Http;

namespace LitleW.Exception
{
    public class HttpExceptionRequest : HttpRequestException
    {
        public System.Net.HttpStatusCode HttpCode { get; }
        public HttpExceptionRequest(System.Net.HttpStatusCode code) : this(code, null, null)
        {
        }

        public HttpExceptionRequest(System.Net.HttpStatusCode code, string message) : this(code, message, null)
        {
        }

        public HttpExceptionRequest(System.Net.HttpStatusCode code, string message, SystemException inner) : base(message, inner)
        {
            HttpCode = code;
        }

    }
}
