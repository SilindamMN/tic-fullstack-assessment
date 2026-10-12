using System.ComponentModel.DataAnnotations.Schema;

namespace TlcBackend.Entities
{
    /// <summary>
    /// Represents a participant.
    /// </summary>
    [Table("Participants")]
    public class Participant
    {
        /// <summary>
        /// Gets the participant identifier.
        /// </summary>
        public int ParticipantId { get; private set; }

        /// <summary>
        /// Gets the participant full name.
        /// </summary>
        public string FullName { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the participant email address.
        /// </summary>
        public string Email { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the participant country code.
        /// </summary>
        public string CountryCode { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the participant points balance.
        /// </summary>
        public int PointsBalance { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the participant is active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Gets the redemptions made by this participant.
        /// </summary>
        public ICollection<Redemption> Redemptions { get; private set; }
            = new List<Redemption>();

        /// <summary>
        /// Deducts points from the participant's balance.
        /// </summary>
        /// <param name="points">The number of points to deduct.</param>
        public void DeductPoints(int points)
        {
            PointsBalance -= points;
        }

        /// <summary>
        /// Adds points back to the participant's balance.
        /// </summary>
        /// <param name="points">The number of points to refund.</param>
        public void RefundPoints(int points)
        {
            PointsBalance += points;
        }
    }
}