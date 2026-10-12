using Microsoft.EntityFrameworkCore;
using TlcBackend.Data;
using TlcBackend.DTOs;
using TlcBackend.Entities;
using TlcBackend.Entities.Enums;
using TlcBackend.Exceptions;
using TlcBackend.Interface;

namespace TlcBackend.Services
{
    /// <summary>
    /// Provides operations for redemptions.
    /// </summary>
    public class RedemptionService : IRedemptionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RedemptionService> _logger;

        /// <summary>
        /// Initializes a new instance of the RedemptionService class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="logger">The logger.</param>
        public RedemptionService(
            AppDbContext context,
            ILogger<RedemptionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gets redemptions with optional status and country filters.
        /// </summary>
        public async Task<IEnumerable<RedemptionDto>> GetAllAsync(
            string? status,
            string? countryCode)
        {
            var query = _context.Redemptions
                .AsNoTracking()
                .Include(x => x.Participant)
                .Include(x => x.Reward)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (!Enum.TryParse<RedemptionStatus>(
                    status.Trim(),
                    true,
                    out var redemptionStatus))
                {
                    return Enumerable.Empty<RedemptionDto>();
                }

                query = query.Where(x => x.Status == redemptionStatus);
            }

            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                var normalizedCountryCode =
                    countryCode.Trim().ToUpperInvariant();

                query = query.Where(x =>
                    x.Participant.CountryCode == normalizedCountryCode);
            }

            return await query
                .OrderByDescending(x => x.RequestedAt)
                .Select(x => new RedemptionDto
                {
                    RedemptionId = x.RedemptionId,
                    ParticipantName = x.Participant.FullName,
                    CountryCode = x.Participant.CountryCode,
                    RewardName = x.Reward.Name,
                    PointsCost = x.Reward.PointsCost,
                    Status = x.Status.ToString(),
                    RequestedAt = x.RequestedAt
                })
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new redemption after validating the business rules.
        /// </summary>
        public async Task<RedemptionDto> CreateAsync(
            CreateRedemptionRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            // Rule 1: Validate IDs.
            if (request.ParticipantId <= 0 ||
                request.RewardId <= 0)
            {
                throw new RedemptionException(
                    "participantId and rewardId must be greater than 0.",
                    400);
            }

            // Rule 2: Check that the participant exists.
            var participant = await _context.Participants
                .FirstOrDefaultAsync(x =>
                    x.ParticipantId == request.ParticipantId);

            if (participant is null)
            {
                throw new RedemptionException(
                    "Participant was not found.",
                    404);
            }

            // Rule 3: Check that the participant is active.
            if (!participant.IsActive)
            {
                throw new RedemptionException(
                    "Participant is not active.",
                    400);
            }

            // Rule 4: Check that the reward exists.
            var reward = await _context.Rewards
                .FirstOrDefaultAsync(x =>
                    x.RewardId == request.RewardId);

            if (reward is null)
            {
                throw new RedemptionException(
                    "Reward was not found.",
                    404);
            }

            // Rule 5: Check that the reward is active.
            if (!reward.IsActive)
            {
                throw new RedemptionException(
                    "Reward is not active.",
                    400);
            }

            // Rule 6: Prevent duplicate pending redemptions.
            var existingPendingRedemption =
                await _context.Redemptions.AnyAsync(x =>
                    x.ParticipantId == participant.ParticipantId &&
                    x.RewardId == reward.RewardId &&
                    x.Status == RedemptionStatus.Pending);

            if (existingPendingRedemption)
            {
                throw new RedemptionException(
                    "Participant already has a pending redemption for this reward.",
                    409);
            }

            // Rule 7: Check the points balance.
            if (participant.PointsBalance < reward.PointsCost)
            {
                throw new RedemptionException(
                    "Participant does not have enough points.",
                    400);
            }

            // Rule 8: Create the redemption and deduct points atomically.
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var redemption = new Redemption(
                    participantId: participant.ParticipantId,
                    rewardId: reward.RewardId,
                    status: RedemptionStatus.Pending,
                    requestedAt: DateTime.UtcNow
                );

                _context.Redemptions.Add(redemption);

                participant.DeductPoints(reward.PointsCost);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation(
                    "Redemption {RedemptionId} created for participant {ParticipantId} and reward {RewardId}.",
                    redemption.RedemptionId,
                    participant.ParticipantId,
                    reward.RewardId);

                return new RedemptionDto
                {
                    RedemptionId = redemption.RedemptionId,
                    ParticipantName = participant.FullName,
                    CountryCode = participant.CountryCode,
                    RewardName = reward.Name,
                    PointsCost = reward.PointsCost,
                    Status = redemption.Status.ToString(),
                    RequestedAt = redemption.RequestedAt,
                    Message =
                        $"{participant.FullName} redeemed {reward.Name}: " +
                        $"{reward.PointsCost} points used, " +
                        $"{participant.PointsBalance} points left."
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                _logger.LogError(
                    ex,
                    "Failed to create redemption for participant {ParticipantId} and reward {RewardId}.",
                    request.ParticipantId,
                    request.RewardId);

                throw;
            }
        }
    }
}