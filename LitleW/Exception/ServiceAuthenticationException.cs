using System;
namespace LitleW.Exception
{
    public class ServiceAuthenticationException : SystemException
    {
        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}
