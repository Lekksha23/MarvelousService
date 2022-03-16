create procedure [dbo].[ServiceToLead_SelectByLead]
		@LeadId int
as
begin
	select
		 [Type],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId]
	from dbo.[ServiceToLead]
	where LeadId = @LeadId
end