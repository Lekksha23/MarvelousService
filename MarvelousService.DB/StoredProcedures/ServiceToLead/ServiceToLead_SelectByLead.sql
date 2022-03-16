create procedure [dbo].[ServiceToLead_SelectByLead]
		@LeadId int
as
begin
	select
	     sl.[Id],
		 sl.[ServicePeriodId],
	     sl.[Price],
	     sl.[Status],
		 sl.[LeadId],
		 sl.[ServiceId],
		 s.[Id],
		 s.[Name],
		 s.[Type],
		 s.[Description],
		 s.[Price],
		 p.[Id],
		 p.[Period]
    from dbo.[ServiceToLead] sl 
	inner join dbo.[Service] s ON sl.ServiceId = s.Id
	inner join dbo.[ServicePeriod] p ON sl.ServicePeriodId = p.Id
	where LeadId = @LeadId
end