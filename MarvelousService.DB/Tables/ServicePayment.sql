CREATE TABLE [dbo].[ServicePayment]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ServiceToLeadId] INT NOT NULL,
	[TransactionId] bigint NOT NULL
	CONSTRAINT [FK_Transaction_ServiceToLead] FOREIGN KEY ([ServiceToLeadId]) REFERENCES [dbo].[ServiceToLead]([Id])
)
