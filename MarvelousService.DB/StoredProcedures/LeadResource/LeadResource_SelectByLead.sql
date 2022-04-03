create procedure [dbo].[LeadResource_SelectByLead]
		@LeadId int
as
begin
	select
	     lr.[Id],
		 lr.[Period],
	     lr.[Price],
	     lr.[Status],
		 lr.[LeadId],
		 lr.[ResourceId],
		 r.[Id],
		 r.[Name],
		 r.[Type],
		 r.[Description],
		 r.[Price]
    from dbo.[LeadResource] lr inner join dbo.[Resource] r ON lr.ResourceId = r.Id
	where lr.LeadId = @LeadId
end