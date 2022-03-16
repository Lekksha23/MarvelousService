CREATE TABLE [dbo].[Transaction]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[ServiceToLeadId] INT NOT NULL,
    CONSTRAINT [FK_ServiceToLead_Transaction_ServiceToLead] FOREIGN KEY ([ServiceToLeadId]) REFERENCES [dbo].[ServiceToLead]([Id])
)
