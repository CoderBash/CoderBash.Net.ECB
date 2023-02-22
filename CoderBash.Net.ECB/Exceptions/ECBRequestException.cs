using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CoderBash.Net.ECB.Exceptions
{
	[ExcludeFromCodeCoverage]
	[Serializable]
	public class ECBRequestException : Exception
	{
		public ECBRequestException()
		{
		}

        public ECBRequestException(string? message) : base(message)
        {
        }

        public ECBRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ECBRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

