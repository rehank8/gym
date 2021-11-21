USE [gym]
GO

/****** Object:  Table [dbo].[Event]    Script Date: 21-11-2021 22:32:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [varchar](250) NOT NULL,
	[Description] [varchar](1024) NOT NULL,
	[EventDateTime] [datetime] NOT NULL,
	[EventDuration] [int] NOT NULL,
	[EventImagePath] [varchar](512) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


