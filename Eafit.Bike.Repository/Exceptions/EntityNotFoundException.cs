using System;

namespace Eafit.Bike.Repository.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }
        public EntityNotFoundException(string message) : base(message) { }
    }
}
