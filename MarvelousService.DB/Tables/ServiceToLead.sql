CREATE TABLE [dbo].[ServiceToLead]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Name] int UNIQUE NOT NULL,
	[Type] int NOT NULL,
	[Period] int NULL,
	[Price] decimal(10,0) NOT NULL,
	[Description] varchar(300) NOT NULL,
	[Status] int NOT NULL,
	[LeadId] int NOT NULL,
	[TransactionId] int NOT NULL
)
