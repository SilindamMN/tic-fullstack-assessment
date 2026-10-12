namespace TlcBackend.DTOs
{
    /// <summary>
    /// Represents a request to create a redemption.
    /// </summary>
    public class CreateRedemptionRequest
    {
        /// <summary>
        /// Gets or sets the participant identifier.
        /// </summary>
        public int ParticipantId { get; set; }

        /// <summary>
        /// Gets or sets the reward identifier.
        /// </summary>
        public int RewardId { get; set; }
    }
}