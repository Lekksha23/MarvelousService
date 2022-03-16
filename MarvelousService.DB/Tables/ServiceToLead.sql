CREATE TABLE [dbo].[ServiceToLead]
(
	[Id] int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Period] int NOT NULL,
	[Price] decimal(10,0) NOT NULL,
	[Status] int NOT NULL,
	[ServiceId] int NOT NULL,
	[LeadId] int NOT NULL,
	[TransactionId] int NOT NULL
	CONSTRAINT [FK_ServiceToLead_Service] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service]([Id]),
	CONSTRAINT [FK_ServiceToLead_Transaction] FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[Transaction]([Id]), 
)
