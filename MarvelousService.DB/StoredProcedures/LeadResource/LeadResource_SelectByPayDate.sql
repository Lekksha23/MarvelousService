create procedure [dbo].[LeadResource_SelectByPayDate]
   @PayDate date
as
begin
	select 
		lr.Id, 
		lr.LeadId, 
		lr.Price,
		r.[Id],
		r.[Name]
	from dbo.LeadResource lr inner join dbo.[Resource] r ON lr.ResourceId = r.Id
	where datediff(week, StartDate, @PayDate) = 1 and [Status] = 1 and [Period] = 2 or 
		  datediff(month, StartDate, @PayDate) = 1 and [Status] = 1 and [Period] = 3 or 
		  datediff(year, StartDate, @PayDate) = 1 and [Status] = 1 and [Period] = 4
end