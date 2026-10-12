namespace TlcBackend.DTOs
{
    /// <summary>
    /// Represents a request to change a redemption status.
    /// </summary>
    public class UpdateRedemptionStatusRequest
    {
        /// <summary>
        /// Gets or sets the new status (Completed or Failed).
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the failure reason (required when Failed).
        /// </summary>
        public string? FailureReason { get; set; }
    }
}