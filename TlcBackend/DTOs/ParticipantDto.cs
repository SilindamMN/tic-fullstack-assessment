namespace TlcBackend.DTOs
{
    /// <summary>
    /// Represents a participant returned by the API.
    /// </summary>
    public class ParticipantDto
    {
        /// <summary>
        /// Gets or sets the participant identifier.
        /// </summary>
        public int ParticipantId { get; set; }

        /// <summary>
        /// Gets or sets the participant full name.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the participant country code.
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the participant points balance.
        /// </summary>
        public int PointsBalance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the participant is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}