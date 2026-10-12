-- TIC Junior Take-Home — schema + seed data (SQL Server)

-- Table 1: Participants
-- Creates the Participants table for people who can redeem rewards.
-- ParticipantId is the primary key (set manually).
-- PointsBalance is the current balance, after existing deductions.
-- IsActive: 1 = active, 0 = inactive (cannot redeem).
CREATE TABLE Participants (
  ParticipantId INT PRIMARY KEY,
  FullName NVARCHAR(200) NOT NULL,
  Email NVARCHAR(200) NOT NULL,
  CountryCode CHAR(2) NOT NULL,
  PointsBalance INT NOT NULL,
  IsActive BIT NOT NULL
);

-- Table 2: Rewards
-- Creates the Rewards table for redeemable rewards.
-- RewardId is the primary key (set manually).
-- PointsCost is the points needed to redeem it.
-- IsActive: 1 = active, 0 = inactive (cannot be redeemed).
CREATE TABLE Rewards (
  RewardId INT PRIMARY KEY,
  Name NVARCHAR(200) NOT NULL,
  PointsCost INT NOT NULL,
  IsActive BIT NOT NULL
);

-- Table 3: Redemptions
-- Creates the Redemptions table for each redemption request.
-- RedemptionId is the primary key (auto-generated, starts at 100).
-- ParticipantId and RewardId link to Participants and Rewards.
-- Status is Pending, Completed or Failed.
-- CompletedAt and FailureReason are NULL until set.
CREATE TABLE Redemptions (
  RedemptionId INT IDENTITY(100,1) PRIMARY KEY,
  ParticipantId INT NOT NULL REFERENCES Participants(ParticipantId),
  RewardId INT NOT NULL REFERENCES Rewards(RewardId),
  Status NVARCHAR(20) NOT NULL,           -- Pending | Completed | Failed
  RequestedAt DATETIME2 NOT NULL,
  CompletedAt DATETIME2 NULL,
  FailureReason NVARCHAR(500) NULL
);

-- Seed 1: Participants
-- Inserts 4 participants into the Participants table.
-- Amina (ZA, 2000 points), Timo (NA, 600 points),
-- Sam (GB, 1500 points, inactive), Lerato (ZA, 100 points).
INSERT INTO Participants VALUES
 (1, N'Amina Jacobs',   N'amina@example.com',  'ZA', 2000, 1),
 (2, N'Timo Negonga',   N'timo@example.com',   'NA',  600, 1),
 (3, N'Sam Greene',     N'sam@example.com',    'GB', 1500, 0),
 (4, N'Lerato Mokoena', N'lerato@example.com', 'ZA',  100, 1);

 -- Seed 2: Rewards
-- Inserts 4 rewards into the Rewards table.
-- Airtime R50 (500 points), Voucher R100 (1000 points),
-- Legacy Gift (200 points, inactive), Data Bundle 1GB (300 points).
INSERT INTO Rewards VALUES
 (10, N'Airtime R50',     500, 1),
 (11, N'Voucher R100',   1000, 1),
 (12, N'Legacy Gift',     200, 0),
 (13, N'Data Bundle 1GB', 300, 1);

-- Seed 3: Redemptions
-- Inserts 4 redemptions into the Redemptions table.
-- 100 and 103: Amina, Completed. 101: Timo, Pending (8 Sep).
-- 102: Timo, Failed (Provider timeout).
-- IDENTITY_INSERT is ON so the fixed IDs 100-103 can be inserted,
-- then OFF so new rows continue from 104.
SET IDENTITY_INSERT Redemptions ON;
INSERT INTO Redemptions (RedemptionId, ParticipantId, RewardId, Status, RequestedAt, CompletedAt, FailureReason) VALUES
 (100, 1, 10, N'Completed', '2026-09-01T09:00:00', '2026-09-01T09:05:00', NULL),
 (101, 2, 11, N'Pending',   '2026-09-08T14:00:00', NULL, NULL),
 (102, 2, 10, N'Failed',    '2026-09-07T11:00:00', NULL, N'Provider timeout'),
 (103, 1, 11, N'Completed', '2026-09-09T08:00:00', '2026-09-09T08:02:00', NULL);
SET IDENTITY_INSERT Redemptions OFF;