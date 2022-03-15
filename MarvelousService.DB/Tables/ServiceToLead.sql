CREATE TABLE [dbo].[ServiceToLead]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Type] int NOT NULL,
	[Period] int NOT NULL,
	[Price] decimal(10,0) NOT NULL,
	[Status] int NOT NULL,
	[ServiceId] int NOT NULL,
	[LeadId] int NOT NULL,
	[TransactionId] int NOT NULL
	CONSTRAINT [FK_ServiceId_ToService] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service]([Id])
)
