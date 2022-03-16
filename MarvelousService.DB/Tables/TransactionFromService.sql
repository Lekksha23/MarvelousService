CREATE TABLE [dbo].[TransactionFromService]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ServiceToLeadId] INT NOT NULL,
	[TransactionId] int NOT NULL,
	[Price] decimal(10,0) NOT NULL
	CONSTRAINT [FK_Transaction_ServiceToLead] FOREIGN KEY ([ServiceToLeadId]) REFERENCES [dbo].[ServiceToLead]([Id])
)
