CREATE TABLE [dbo].[EventTeamMatch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventKey] [nvarchar](50) NULL,
	[TeamNumber] [int] NULL,
	[MatchNumber] [int] NULL,
	[SandstormMoveType] [int] NULL,
	[SandstormOffPlatform] [int] NULL,
	[SandstormHatches] [int] NULL,
	[SandstormCargo] [int] NULL,
	[CargoShipHatches] [int] NULL,
	[CargoShipCargo] [int] NULL,
	[RocketHatches] [int] NULL,
	[RocketCargo] [int] NULL,
	[RocketHighestHatch] [int] NULL,
	[RocketHighestCargo] [int] NULL,
	[EndgamePlatform] [int] NULL,
	[EndgameBuddyClimb] [int] NULL,
	[Defense] [int] NULL,
	[Cooperation] [int] NULL,
	[Fouls] [int] NULL,
	[Broken] [int] NULL,
	[AllianceResult] [int] NULL,
	[RocketRankingPoint] [int] NULL,
	[HabRankingPoint] [int] NULL,
	[ScouterName] [nvarchar](50) NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_EventTeamMatch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_EventTeamMatch_EventKey_TeamNumber_MatchNumber] ON [dbo].[EventTeamMatch]
(
	[EventKey] ASC,
	[TeamNumber] ASC,
	[MatchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
