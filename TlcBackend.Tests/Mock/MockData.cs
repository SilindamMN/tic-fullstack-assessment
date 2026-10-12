
using System;
using TlcBackend.DTOs;
using TlcBackend.Entities;

namespace TlcBackend.Tests.Mock;

/// <summary>
/// Provides mock data for unit tests, including participants, redemption requests, and successful redemptions.
/// </summary>
public static class MockData
{
    public static Participant[] Participants = { };

    /// <summary>
    /// Represents a valid redemption request DTO for testing purposes.
    /// </summary>
    public static CreateRedemptionRequest ValidRequest = new()
    {
        ParticipantId = 1,
        RewardId = 10
    };

    /// <summary>
    /// Represents a successful redemption DTO for testing purposes.
    /// </summary>
    public static RedemptionDto SuccessfulRedemption = new()
    {
        RedemptionId = 104,
        ParticipantName = "Amina Jacobs",
        CountryCode = "ZA",
        RewardName = "Airtime R50",
        PointsCost = 500,
        Status = "Pending",
        RequestedAt = DateTime.UtcNow
    };
}
