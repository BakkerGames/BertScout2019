CREATE TABLE [dbo].[EventTeamMatch](
	[Id] [int] NOT NULL,
	[EventKey] [varchar](50) NULL,
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
	[ScouterName] [varchar](50) NULL,
	[Comments] [varchar](max) NULL,
 CONSTRAINT [PK_EventTeamMatch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
