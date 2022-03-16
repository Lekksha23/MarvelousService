create procedure [dbo].[ServiceToLead_SelectAll]
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
end