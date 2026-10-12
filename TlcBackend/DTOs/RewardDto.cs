namespace TlcBackend.DTOs
{
    /// <summary>
    /// Represents a reward returned by the API.
    /// </summary>
    public class RewardDto
    {
        /// <summary>
        /// Gets or sets the reward identifier.
        /// </summary>
        public int RewardId { get; set; }

        /// <summary>
        /// Gets or sets the reward name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the points required to redeem the reward.
        /// </summary>
        public int PointsCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the reward is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}