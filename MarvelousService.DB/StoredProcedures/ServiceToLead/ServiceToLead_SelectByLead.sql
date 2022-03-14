create procedure [dbo].[ServiceToLead_SelectByLead]
		@LeadId int
as
begin
	select
		 [Name], 
		 [Type],
		 [Period],
	     [Price],
		 [Description],
	     [Status],
		 [LeadId],
		 [TransactionId]
	from dbo.[ServiceToLead]
	where LeadId = @LeadId
end