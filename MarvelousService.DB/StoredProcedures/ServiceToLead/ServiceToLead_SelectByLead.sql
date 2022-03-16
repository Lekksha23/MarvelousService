create procedure [dbo].[ServiceToLead_SelectByLead]
		@LeadId int
as
begin
	select
		 [Period],
	     [Price],
	     [Status],
		 [LeadId],
		 [ServiceId],
		 [TransactionId]
	from dbo.[ServiceToLead]
	where LeadId = @LeadId
end