using TlcBackend.DTOs;

namespace TlcBackend.Interface
{
    /// <summary>
    /// Defines operations for redemptions.
    /// </summary>
    public interface IRedemptionService
    {
        /// <summary>
        /// Retrieves all redemptions, optionally filtered by status and country code.
        /// </summary>
        /// <param name="status"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        Task<IEnumerable<RedemptionDto>> GetAllAsync(
            string? status,
            string? countryCode);

        Task<RedemptionDto> CreateAsync(
            CreateRedemptionRequest request);

    }
}