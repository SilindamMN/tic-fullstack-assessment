using System.ComponentModel.DataAnnotations.Schema;

namespace TlcBackend.Entities
{
    /// <summary>
    /// Represents a reward that can be redeemed.
    /// </summary>
    [Table("Rewards")]
    public class Reward
    {
        /// <summary>
        /// Gets the reward identifier.
        /// </summary>
        public int RewardId { get; private set; }

        /// <summary>
        /// Gets the reward name.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        /// Gets the points required to redeem the reward.
        /// </summary>
        public int PointsCost { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the reward is active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Gets the redemptions for this reward.
        /// </summary>
        public ICollection<Redemption> Redemptions { get; private set; } = new List<Redemption>();
    }
}