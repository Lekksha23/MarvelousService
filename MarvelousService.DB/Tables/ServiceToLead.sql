CREATE TABLE [dbo].[ServiceToLead]
(
	[Id] int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Price] decimal(10,0) NOT NULL,
	[Status] int NOT NULL,
	[Period] int NOT NULL,
	[ServiceId] int NOT NULL,
	[LeadId] int NOT NULL,  
	CONSTRAINT [FK_ServiceToLead_Service] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Service]([Id])
)
