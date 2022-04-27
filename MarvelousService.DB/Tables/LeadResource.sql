CREATE TABLE [dbo].[LeadResource]
(
	[Id] int NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Price] decimal(10,0) NOT NULL,
	[Status] int NOT NULL,
	[Period] int NOT NULL,
	[StartDate] date NOT NULL,
	[ResourceId] int NOT NULL,
	[LeadId] int NOT NULL
	CONSTRAINT [FK_LeadResource_Resource] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resource]([Id])
)
