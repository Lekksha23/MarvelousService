create procedure [dbo].[ServiceToLead_SelectAll]
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
end