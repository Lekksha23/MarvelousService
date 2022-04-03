CREATE TABLE [dbo].[ResourcePayment]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[LeadResourceId] INT NOT NULL,
	[TransactionId] bigint NOT NULL
	CONSTRAINT [FK_ResourcePayment_LeadResource] FOREIGN KEY ([LeadResourceId]) REFERENCES [dbo].[LeadResource]([Id])
)
