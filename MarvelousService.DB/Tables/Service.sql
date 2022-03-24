CREATE TABLE [dbo].[Service]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(40) UNIQUE NOT NULL,
	[Type] tinyint NOT NULL,
	[Description] VARCHAR(500) NOT NULL,
	[Price] decimal(6,0) NOT NULL,
	[IsDeleted] BIT NOT NULL
)
