using System;
namespace Application.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message) { }

        public NotFoundException()
            : base() { }
    }
}
