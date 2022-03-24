CREATE TABLE [dbo].[ServiceToLead]
(
	[Id] int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Price] decimal(6,0) NOT NULL,
	[Status] tinyint NOT NULL,
	[Period] tinyint NOT NULL,
	[ServiceId] int NOT NULL,
	[LeadId] int NOT NULL,  
	CONSTRAINT [FK_ServiceToLead_Service] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service]([Id])
)
