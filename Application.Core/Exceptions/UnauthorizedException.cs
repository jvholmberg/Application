using System;
namespace Application.Core.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message)
            : base(message) { }

        public UnauthorizedException()
            : base() { }
    }
}
