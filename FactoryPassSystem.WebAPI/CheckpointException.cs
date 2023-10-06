using System;

namespace FactoryPassSystem.WebAPI
{
    public class CheckpointException : Exception
    {
        public CheckpointException(string message) : base(message) { }

        public CheckpointException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
