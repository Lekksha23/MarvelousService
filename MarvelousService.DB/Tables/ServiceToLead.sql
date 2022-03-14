CREATE TABLE [dbo].[ServiceToLead]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[ServiceId] int NOT NULL,
	[Type] int NOT NULL,
	[Period] int NULL,
	[Price] decimal(10,0) NOT NULL,
	[Status] int NOT NULL,
	[LeadId] int NOT NULL,
	[TransactionId] int NOT NULL
)
