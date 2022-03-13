CREATE TABLE [dbo].[Service]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[ServiceName] int UNIQUE NOT NULL,
	[ServiceType] int NOT NULL,
	[Period] int NULL,
	[Price] decimal(10,0) NOT NULL,
	[Description] varchar(300) NOT NULL,
	[Status] int NOT NULL,
	[LeadId] int NOT NULL,
	[TransactionId] int NOT NULL
)
