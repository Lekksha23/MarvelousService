create procedure [dbo].[ServiceToLead_SelectByLead]
		@LeadId int
as
begin
	select
	     sl.[Id],
		 sl.[PeriodId],
	     sl.[Price],
	     sl.[Status],
		 sl.[LeadId],
		 sl.[ServiceId],
		 s.[Id],
		 s.[Name],
		 s.[Type],
		 s.[Description],
		 s.[Price]
    from dbo.[ServiceToLead] sl inner join dbo.[Service] s ON sl.ServiceId = s.Id
	where LeadId = @LeadId
end