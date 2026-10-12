namespace TlcBackend.Entities.Enums
{
    /// <summary>
    /// Represents the status of a redemption.
    /// </summary>
    public enum RedemptionStatus
    {
        /// <summary>
        /// The redemption is waiting to be processed.
        /// </summary>
        Pending,

        /// <summary>
        /// The redemption was completed successfully.
        /// </summary>
        Completed,

        /// <summary>
        /// The redemption failed.
        /// </summary>
        Failed
    }
}