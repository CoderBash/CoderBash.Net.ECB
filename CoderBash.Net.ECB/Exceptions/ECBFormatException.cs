using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CoderBash.Net.ECB.Exceptions
{
	[ExcludeFromCodeCoverage]
	[Serializable]
	public class EcbFormatException : Exception
	{
		public EcbFormatException()
		{
		}

        public EcbFormatException(string? message) : base(message)
        {
        }

        public EcbFormatException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EcbFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

