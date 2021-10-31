USE [gym]
GO

CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_UserModel] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserModel] ([Id])
GO

ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_UserModel]
GO

IF Not Exists(select count(id) from Payment)
BEGIN
INSERT INTO [dbo].[Payment] ([UserId],[UserName] ,[Amount],[CreatedDate])
     VALUES (2 ,'test' ,10 ,getdate())
END
GO

