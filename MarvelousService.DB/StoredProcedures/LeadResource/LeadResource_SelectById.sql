create procedure [dbo].[LeadResource_SelectById]
            @Id int
as
begin
    select
        lr.[Id],
		lr.[Period],
		lr.[Price],
		lr.[Status],
		lr.[LeadId],
		r.[Id],
		r.[Name],
		r.[Type],
		r.[Description],
		r.[Price]
    from dbo.[LeadResource] lr inner join dbo.[Resource] r ON lr.ResourceId = r.Id
	where lr.Id = @Id
end