-- Script Date: 02/24/2019 13:02  - ErikEJ.SqlCeScripting version 3.5.2.80
-- Database information:
-- Database: D:\Projects\BertScout2019\BertWebApi2019\App_Data\bertscout2019.db3
-- ServerVersion: 3.24.0
-- DatabaseSize: 712 KB
-- Created: 02/19/2019 19:39

-- User Table information:
-- Number of tables: 4
-- EventTeam: -1 row(s)
-- EventTeamMatch: -1 row(s)
-- FRCEvent: -1 row(s)
-- Team: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [Team] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Uuid] nvarchar(2147483647) NULL COLLATE NOCASE
, [Changed] bigint NULL
, [TeamNumber] bigint NULL
, [Name] nvarchar(2147483647) NULL COLLATE NOCASE
, [Location] nvarchar(2147483647) NULL COLLATE NOCASE
);
CREATE TABLE [FRCEvent] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Uuid] nvarchar(2147483647) NULL COLLATE NOCASE
, [Changed] bigint NULL
, [EventKey] nvarchar(2147483647) NULL COLLATE NOCASE
, [Name] nvarchar(2147483647) NULL COLLATE NOCASE
, [Location] nvarchar(2147483647) NULL COLLATE NOCASE
, [StartDate] nvarchar(2147483647) NULL COLLATE NOCASE
, [EndDate] nvarchar(2147483647) NULL COLLATE NOCASE
);
CREATE TABLE [EventTeamMatch] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Uuid] nvarchar(2147483647) NULL COLLATE NOCASE
, [Changed] bigint NULL
, [EventKey] nvarchar(2147483647) NULL COLLATE NOCASE
, [TeamNumber] bigint NULL
, [MatchNumber] bigint NULL
, [SandstormMoveType] bigint NULL
, [SandstormOffPlatform] bigint NULL
, [SandstormHatches] bigint NULL
, [SandstormCargo] bigint NULL
, [CargoShipHatches] bigint NULL
, [CargoShipCargo] bigint NULL
, [RocketHatches] bigint NULL
, [RocketCargo] bigint NULL
, [RocketHighestHatch] bigint NULL
, [RocketHighestCargo] bigint NULL
, [EndgamePlatform] bigint NULL
, [EndgameBuddyClimb] bigint NULL
, [Defense] bigint NULL
, [Cooperation] bigint NULL
, [Fouls] bigint NULL
, [TechFouls] bigint NULL
, [Broken] bigint NULL
, [AllianceResult] bigint NULL
, [RocketRankingPoint] bigint NULL
, [HabRankingPoint] bigint NULL
, [ScouterName] nvarchar(2147483647) NULL COLLATE NOCASE
, [Comments] nvarchar(2147483647) NULL COLLATE NOCASE
);
CREATE TABLE [EventTeam] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Uuid] nvarchar(2147483647) NULL COLLATE NOCASE
, [Changed] bigint NULL
, [EventKey] nvarchar(2147483647) NULL COLLATE NOCASE
, [TeamNumber] bigint NULL
);
CREATE INDEX [Team_Team_TeamNumber] ON [Team] ([TeamNumber] ASC);
CREATE INDEX [FRCEvent_FRCEvent_EventKey] ON [FRCEvent] ([EventKey] ASC);
CREATE INDEX [EventTeamMatch_EventTeamMatch_MatchNumber] ON [EventTeamMatch] ([MatchNumber] ASC);
CREATE INDEX [EventTeamMatch_EventTeamMatch_TeamNumber] ON [EventTeamMatch] ([TeamNumber] ASC);
CREATE INDEX [EventTeamMatch_EventTeamMatch_EventKey] ON [EventTeamMatch] ([EventKey] ASC);
CREATE INDEX [EventTeam_EventTeam_TeamNumber] ON [EventTeam] ([TeamNumber] ASC);
CREATE INDEX [EventTeam_EventTeam_EventKey] ON [EventTeam] ([EventKey] ASC);
COMMIT;

