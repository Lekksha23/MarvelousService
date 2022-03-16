CREATE TABLE [dbo].[ServiceToLead_Transaction]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[ServiceToLeadId] INT NOT NULL,
	[TransactionId] INT NOT NULL, 
    CONSTRAINT [FK_ServiceToLead_Transaction_ServiceToLead] FOREIGN KEY ([ServiceToLeadId]) REFERENCES [dbo].[ServiceToLead]([Id]),
	CONSTRAINT [FK_ServiceToLead_ToTransaction_ServiceToLead] FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[ServiceToLead]([TransactionId])
)
