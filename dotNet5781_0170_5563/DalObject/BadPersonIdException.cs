using System;
using System.Runtime.Serialization;

namespace Dal
{
    [Serializable]
    internal class BadPersonIdException : Exception
    {
        private object id;
        private string v;

        public BadPersonIdException()
        {
        }

        public BadPersonIdException(string message) : base(message)
        {
        }

        public BadPersonIdException(object id, string v)
        {
            this.id = id;
            this.v = v;
        }

        public BadPersonIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadPersonIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}