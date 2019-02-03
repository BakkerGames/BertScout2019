CREATE TABLE [dbo].[FRCEvent](
	[Id] [int] NOT NULL,
	[EventKey] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[Location] [varchar](50) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_FRCEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
