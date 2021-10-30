USE [gym]
GO

 
CREATE TABLE [dbo].[LoginHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[IPAddress] [varchar](200) NOT NULL,
	[Action] [varchar](512) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_UserModel] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserModel] ([Id])
GO

ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_UserModel]
GO


