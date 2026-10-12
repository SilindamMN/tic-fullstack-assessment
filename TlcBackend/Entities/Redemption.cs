using System.ComponentModel.DataAnnotations.Schema;
using TlcBackend.Entities.Enums;

namespace TlcBackend.Entities
{
    /// <summary>
    /// Represents a reward redemption made by a participant.
    /// </summary>
    [Table("Redemptions")]
    public class Redemption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Redemption"/> class.
        /// </summary>
        private Redemption()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Redemption"/> class.
        /// </summary>
        /// <param name="participantId">The participant identifier.</param>
        /// <param name="rewardId">The reward identifier.</param>
        /// <param name="status">The redemption status.</param>
        /// <param name="requestedAt">The date and time the redemption was requested.</param>
        public Redemption(
            int participantId,
            int rewardId,
            RedemptionStatus status,
            DateTime requestedAt)
        {
            ParticipantId = participantId;
            RewardId = rewardId;
            Status = status;
            RequestedAt = requestedAt;
        }

        /// <summary>
        /// Gets the redemption identifier.
        /// </summary>
        public int RedemptionId { get; private set; }

        /// <summary>
        /// Gets the participant identifier.
        /// </summary>
        public int ParticipantId { get; private set; }

        /// <summary>
        /// Gets the reward identifier.
        /// </summary>
        public int RewardId { get; private set; }

        /// <summary>
        /// Gets the redemption status.
        /// </summary>
        public RedemptionStatus Status { get; private set; }

        /// <summary>
        /// Gets the date and time the redemption was requested.
        /// </summary>
        public DateTime RequestedAt { get; private set; }

        /// <summary>
        /// Gets the date and time the redemption was completed.
        /// </summary>
        public DateTime? CompletedAt { get; private set; }

        /// <summary>
        /// Gets the reason for a failed redemption.
        /// </summary>
        public string? FailureReason { get; private set; }

        /// <summary>
        /// Gets the participant associated with the redemption.
        /// </summary>
        public Participant Participant { get; private set; } = null!;

        /// <summary>
        /// Gets the reward associated with the redemption.
        /// </summary>
        public Reward Reward { get; private set; } = null!;
  }
}