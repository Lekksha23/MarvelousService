create procedure [dbo].[ServiceToLead_SelectById]
            @Id int
as
begin
    select
        sl.[Id],
		sl.[Period],
		sl.[Price],
		sl.[Status],
		sl.[ServiceId],
		sl.[LeadId],
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
	where sl.Id = @Id
end