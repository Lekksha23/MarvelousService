create procedure [dbo].[Service_SelectAll]
as
begin
	select
		 [ServiceName], 
		 [ServiceType],
		 [Period],
	     [Price],
		 [Description],
	     [Status],
		 [LeadId],
		 [TransactionId]
	from dbo.[Service]
end