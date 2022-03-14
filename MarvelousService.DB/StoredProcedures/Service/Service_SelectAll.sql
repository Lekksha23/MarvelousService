create procedure [dbo].[Service_SelectAll]
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
	from dbo.[Service]
end