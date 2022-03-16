CREATE TABLE [dbo].[ServiceToLead]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	[Type] int NOT NULL,
	[PeriodId] int NOT NULL,
	[Price] decimal(10,0) NOT NULL,
	[Status] int NOT NULL,
	[ServiceId] int NOT NULL,
	[LeadId] int NOT NULL, 
    CONSTRAINT [FK_ServiceToLead_Period] FOREIGN KEY ([PeriodId]) REFERENCES [Period]([Id]) 
	
)
