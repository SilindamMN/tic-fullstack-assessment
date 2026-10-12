
-- Task 1: Returns all Pending redemptions with participant name,
-- email, country, reward name and points cost.
SELECT
    p.FullName,
    p.Email,
    p.CountryCode,
    rw.Name AS RewardName,
    rw.PointsCost
FROM Redemptions rd
INNER JOIN Participants p
    ON rd.ParticipantId = p.ParticipantId
INNER JOIN Rewards rw
    ON rd.RewardId = rw.RewardId
WHERE rd.Status = 'Pending';


-- Task 2: Returns the number of redemptions per country and status.
-- Seed data: 3 rows (NA Failed 1, NA Pending 1, ZA Completed 2).
SELECT
    p.CountryCode,
    rd.Status,
    COUNT(*) AS TotalRedemptions
FROM Redemptions rd
INNER JOIN Participants p
    ON rd.ParticipantId = p.ParticipantId
GROUP BY
    p.CountryCode,
    rd.Status
ORDER BY
    p.CountryCode,
    rd.Status;


-- Task 3: Returns active participants who have no Completed redemption.
-- Seed data: 2 rows (Timo Negonga, Lerato Mokoena).

SELECT
    p.ParticipantId,
    p.FullName,
    p.Email,
    p.CountryCode
FROM Participants p
WHERE p.IsActive = 1
  AND NOT EXISTS (
      SELECT 1
      FROM Redemptions rd
      WHERE rd.ParticipantId = p.ParticipantId
        AND rd.Status = 'Completed'
  );


-- Task 4: Returns active participants who can afford the active Voucher R100.
-- the active Voucher R100.
-- Seed data: 1 row (Amina Jacobs, 2000 points vs cost 1000)
SELECT
    p.ParticipantId,
    p.FullName,
    p.PointsBalance,
    rw.Name AS RewardName,
    rw.PointsCost
FROM Participants p
INNER JOIN Rewards rw
    ON rw.Name = 'Voucher R100'
WHERE p.IsActive = 1
  AND rw.IsActive = 1
  AND p.PointsBalance >= rw.PointsCost;


-- Task 5: Returns total points spent on Completed redemptions
-- for every participant, including participants with zero points spent.
SELECT
    p.ParticipantId,
    p.FullName,
    COALESCE(SUM(rw.PointsCost), 0) AS TotalPointsSpent
FROM Participants p
LEFT JOIN Redemptions rd
    ON p.ParticipantId = rd.ParticipantId
    AND rd.Status = 'Completed'
LEFT JOIN Rewards rw
    ON rd.RewardId = rw.RewardId
GROUP BY
    p.ParticipantId,
    p.FullName
ORDER BY
    p.ParticipantId;


-- Index: Helps retrieve a participant's redemptions by request date.
CREATE NONCLUSTERED INDEX IX_Redemptions_ParticipantId_RequestedAt
ON Redemptions (ParticipantId, RequestedAt DESC);

-- Example of how the index will be used:
-- Returns Timo's redemptions, newest first.
-- SQL Server picks the index automatically (no need to call it).
SELECT RedemptionId, RewardId, Status, RequestedAt
FROM Redemptions
WHERE ParticipantId = 2
ORDER BY RequestedAt DESC;