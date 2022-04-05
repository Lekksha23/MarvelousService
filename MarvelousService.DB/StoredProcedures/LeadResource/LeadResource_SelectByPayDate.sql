create procedure [dbo].[LeadResource_SelectByPayDate]
   @Date date
as
begin
	create table #ResourceInfo
	(Id int, 
	 LeadId int,
	 ResourceId int,
	 Price decimal (6,0))
insert into #ResourceInfo (Id, LeadId, ResourceId, Price) 
select 
	Id, 
	LeadId, 
	ResourceId, 
	Price 
from dbo.LeadResource
where datediff(week, StartDate, @Date) = 1 and [Status] = 1 and [Period] = 2 or 
	  datediff(month, StartDate, @Date) = 1 and [Status] = 1 and [Period] = 3 or 
	  datediff(year, StartDate, @Date) = 1 and [Status] = 1 and [Period] = 4
end

select * from #ResourceInfo
