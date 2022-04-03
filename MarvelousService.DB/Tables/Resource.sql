CREATE TABLE [dbo].[Resource]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Name] VARCHAR(50) UNIQUE NOT NULL,
	[Type] int NOT NULL,
	[Description] VARCHAR(1000) NOT NULL,
	[Price] decimal(10,0) NOT NULL,
	[IsDeleted] BIT NOT NULL
)
