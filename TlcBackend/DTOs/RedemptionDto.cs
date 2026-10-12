namespace TlcBackend.DTOs
{
    /// <summary>
    /// Represents a redemption returned by the API.
    /// </summary>
    public class RedemptionDto
    {
        /// <summary>
        /// Gets or sets the redemption identifier.
        /// </summary>
        public int RedemptionId { get; set; }

        /// <summary>
        /// Gets or sets the participant name.
        /// </summary>
        public string ParticipantName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the participant country code.
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reward name.
        /// </summary>
        public string RewardName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reward points cost.
        /// </summary>
        public int PointsCost { get; set; }

        /// <summary>
        /// Gets or sets the redemption status.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time the redemption was requested.
        /// </summary>
        public DateTime RequestedAt { get; set; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public string? Message { get; set; }
    }
}