using System;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name) : base(name) { }
    }
}
