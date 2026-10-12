namespace TlcBackend.Exceptions
{
  /// <summary>
 /// Represents a business rule failure when creating a redemption.
 /// </summary>
    public class RedemptionException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code associated with the error.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedemptionException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        public RedemptionException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}