using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CoderBash.Net.ECB.Exceptions
{
	[ExcludeFromCodeCoverage]
	[Serializable]
	public class ECBFormatException : Exception
	{
		public ECBFormatException()
		{
		}

        public ECBFormatException(string? message) : base(message)
        {
        }

        public ECBFormatException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ECBFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

