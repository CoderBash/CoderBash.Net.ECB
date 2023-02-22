using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace CoderBash.Net.ECB.Exceptions
{
    /// <summary>
    /// Exception thrown when an error occurs with any request to the ECB API.
    /// </summary>
	[ExcludeFromCodeCoverage]
	[Serializable]
	public class EcbRequestException : Exception
	{
		public EcbRequestException()
		{
		}

        public EcbRequestException(string? message) : base(message)
        {
        }

        public EcbRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EcbRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

